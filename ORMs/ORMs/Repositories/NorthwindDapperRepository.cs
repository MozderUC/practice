using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using AutoMapper;
using Dapper;
using Microsoft.Extensions.Configuration;
using ORMs.Contracts;
using ORMs.Entities;
using ORMs.Utils;

namespace ORMs.Repositories
{
    public class NorthwindDapperRepository : INorthwindRepository
    {
        private const string ResourcesPath = "Resources";

        private readonly IConfigurationRoot _configuration;
        private readonly IDbConnection _dbConnection;

        private readonly string GetCategoriesScript = ScriptLoader.GetEmbeddedResourceByPath(ResourcesPath);
        private readonly string GetEmployeesScript = ScriptLoader.GetEmbeddedResourceByPath(ResourcesPath);
        private readonly string GetProductsScript = ScriptLoader.GetEmbeddedResourceByPath(ResourcesPath);
        private readonly string InsertEmpScript = ScriptLoader.GetEmbeddedResourceByPath(ResourcesPath);
        private readonly string InsertEmployeeTerritoriesScript = ScriptLoader.GetEmbeddedResourceByPath(ResourcesPath);
        private readonly string ChangeProductsCategoryScript = ScriptLoader.GetEmbeddedResourceByPath(ResourcesPath);

        public NorthwindDapperRepository()
        {
            this._configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json").Build();

            this._dbConnection = new SqlConnection(_configuration["connectionString"]);
        }

        public IEnumerable<Category> GetCategories()
        {
            var categoryDictionary = new Dictionary<int, Category>();

            var categories = _dbConnection.Query<Category, Product, Category>(
                    GetCategoriesScript,
                    (category, product) =>
                    {
                        if (!categoryDictionary.TryGetValue(category.CategoryId, out var categoryEntry))
                        {
                            categoryEntry = category;
                            categoryEntry.Products = new List<Product>();
                            categoryDictionary.Add(categoryEntry.CategoryId, categoryEntry);
                        }

                        categoryEntry.Products.Add(product);
                        return categoryEntry;
                    },
                    splitOn: "ProductId"
                )
                .Distinct();

            return categories;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            var employeeDictionary = new Dictionary<int, Employee>();

            var employees = _dbConnection.Query<Employee, Territory, Employee>(
                    GetEmployeesScript,
                    (employee, territory) =>
                    {
                        if (!employeeDictionary.TryGetValue(employee.EmployeeId, out var employeeEntry))
                        {
                            employeeEntry = employee;
                            employeeEntry.Territory = new List<Territory>();
                            employeeDictionary.Add(employeeEntry.EmployeeId, employeeEntry);
                        }

                        employeeEntry.Territory.Add(territory);
                        return employeeEntry;
                    },
                    splitOn: "TerritoryID"
                )
                .Distinct();

            return employees;
        }

        public IEnumerable<Product> GetProducts()
        {
            var queryResult = _dbConnection.Query(
                GetProductsScript);

            var config = new MapperConfiguration(cfg => { });
            var mapper = config.CreateMapper();

            var result = new List<Product>();

            queryResult.ToList().ForEach(q =>
            {
                Product product = mapper.Map<Product>(q);
                product.Supplier = mapper.Map<Supplier>(q);
                product.Category = mapper.Map<Category>(q);

                result.Add(product);
            });

            return result;
        }

        public int InsertEmployee(Employee employee)
        {
            var id = _dbConnection.QuerySingle<int>(
                InsertEmpScript,
                new
                {
                    employee.FirstName,
                    employee.LastName,
                    employee.Title
                });

            _dbConnection.Execute(InsertEmployeeTerritoriesScript,
                employee.Territory.Select(e => new {EmployeeID = id, TerritoryID = e.TerritoryId}));

            return id;
        }

        public int ChangeProductsCategory(int sourceCategoryId, int targetCategoryId)
        {
            return _dbConnection.Execute(
                ChangeProductsCategoryScript,
                new
                {
                    SourceCategoryId = sourceCategoryId,
                    TargetCategoryId = targetCategoryId
                });
        }
    }
}

using System.Collections.Generic;
using ORMs.Entities;

namespace ORMs.Contracts
{
    public interface INorthwindRepository
    {
        IEnumerable<Category> GetCategories();

        IEnumerable<Product> GetProducts();

        IEnumerable<Employee> GetEmployees();

        int InsertEmployee(Employee employee);

        int ChangeProductsCategory(int sourceCategoryId, int targetCategoryId);
    }
}

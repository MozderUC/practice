using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using ORMs.Entities;
using ORMs.Repositories;

namespace ORMs
{
    internal class Program
    {
        static void Main(string[] args)
        {
            #region Dapper

            var northwindDapperRepository = new NorthwindDapperRepository();

            // get
            var categories = northwindDapperRepository.GetCategories();
            //Console.WriteLine(JsonConvert.SerializeObject(categories, Formatting.Indented));

            var employees = northwindDapperRepository.GetEmployees();
            //Console.WriteLine(JsonConvert.SerializeObject(employees, Formatting.Indented));

            var products = northwindDapperRepository.GetProducts();
            //Console.WriteLine(JsonConvert.SerializeObject(products, Formatting.Indented));

            //update
            //var id = northwindDapperRepository.InsertEmployee(new Employee()
            //{
            //    FirstName = "Mark",
            //    LastName = "Kukareku",
            //    Title = "title",
            //    Territory = new List<Territory>()
            //    {
            //        new Territory()
            //        {
            //            TerritoryId = "01581"
            //        },
            //        new Territory()
            //        {
            //            TerritoryId = "01730"
            //        },
            //        new Territory()
            //        {
            //            TerritoryId = "01833"
            //        }
            //    }
            //});
            //Console.WriteLine(id);

            var affectedRows = northwindDapperRepository.ChangeProductsCategory(1, 2);
            Console.WriteLine(affectedRows);


            #endregion
        }
    }
}

using NetCoreMentoring.Core.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Data;

namespace NetCoreMentoring.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;

        public ProductService(NorthwindContext context)
        {
            _context = context;
        }

        public IEnumerable<Products> GetProducts()
        {
            throw new NotImplementedException();
        }
    }
}

using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;
using NetCoreMentoring.Data;
using Microsoft.Extensions.Configuration;

namespace NetCoreMentoring.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly NorthwindContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductService(
            NorthwindContext context,
            IConfiguration configuration,
            IMapper mapper)
        {
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public IEnumerable<Product> GetProducts()
        {
            //TODO: add exception handling
            var maxProductsOnPage = int.Parse(_configuration["MaxProductsOnPage"]);

            var result = _context.Products
                .Take(maxProductsOnPage)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsEnumerable();

            return _mapper.Map<IEnumerable<Product>>(result);
        }
    }
}
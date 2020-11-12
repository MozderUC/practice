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

        public Product GetProduct(int id)
        {
            var result = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.ProductId == id);

            return _mapper.Map<Product>(result);
        }

        public IEnumerable<Product> GetProducts()
        {
            // TODO: add error handling
            var maxProductsOnPage = int.Parse(_configuration["MaxProductsOnPage"]);

            var result = _context.Products
                .Take(maxProductsOnPage == 0 ? int.MaxValue : maxProductsOnPage)
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsEnumerable();

            return _mapper.Map<IEnumerable<Product>>(result);
        }

        public Product Update(Product product)
        {
            var result = _context.Products.Update(_mapper.Map<Data.Models.ProductEntity>(product));
            _context.SaveChanges();

            return _mapper.Map<Product>(result.Entity);
        }

        public Product Create(Product product)
        {
            var result = _context.Products.Add(_mapper.Map<Data.Models.ProductEntity>(product));
            _context.SaveChanges();

            return _mapper.Map<Product>(result.Entity);
        }
    }
}
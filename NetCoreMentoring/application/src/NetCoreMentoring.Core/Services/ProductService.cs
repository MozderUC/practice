using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;
using NetCoreMentoring.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.Core.Utilities.ResultFlow;

namespace NetCoreMentoring.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly ICategoryService _categoryService;
        private readonly NorthwindContext _context;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public ProductService(
            ICategoryService categoryService,
            NorthwindContext context,
            IConfiguration configuration,
            IMapper mapper)
        {
            _categoryService = categoryService;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
        }

        public Result<IEnumerable<Product>> GetProducts()
        {
            var maxProductsOnPage = int.Parse(_configuration["MaxProductsOnPage"]);

            var result = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Supplier)
                .AsEnumerable();
            result = maxProductsOnPage == 0 ? result : result.Take(maxProductsOnPage);

            return Result.Success(_mapper.Map<IEnumerable<Product>>(result));
        }

        public Result<Product> GetProduct(int id)
        {
            var result = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .FirstOrDefault(p => p.ProductId == id);

            return Result.Success(_mapper.Map<Product>(result));
        }

        public Result<ProductAndCategories> GetProductWithCategories(int id)
        {
            var product = GetProduct(id);
            var categories = _categoryService.GetCategories();

            if (!product.IsSuccess)
            {
                return Result.Failure<ProductAndCategories>(product.Error);
            }

            if (!categories.IsSuccess)
            {
                return Result.Failure<ProductAndCategories>(categories.Error);
            }

            return Result.Success(new ProductAndCategories()
            {
                Product = product.Value,
                Categories = categories.Value
            });
        }

        public Result Update(Product product)
        {
            var result = _context.Products.Update(_mapper.Map<Data.Models.ProductEntity>(product));
            _context.SaveChanges();

            return Result.Success();
        }

        public Result Create(Product product)
        {
            var result = _context.Products.Add(_mapper.Map<Data.Models.ProductEntity>(product));
            _context.SaveChanges();

            return Result.Success(_mapper.Map<Product>(result.Entity));
        }
    }
}
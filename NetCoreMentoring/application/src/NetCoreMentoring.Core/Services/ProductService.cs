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
        private readonly ILogger<ProductService> _logger;

        public ProductService(
            ICategoryService categoryService,
            NorthwindContext context,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<ProductService> logger)
        {
            _categoryService = categoryService;
            _context = context;
            _configuration = configuration;
            _mapper = mapper;
            _logger = logger;
        }

        public Result<IEnumerable<Product>> GetProducts()
        {
            try
            {
                var maxProductsOnPage = int.Parse(_configuration["MaxProductsOnPage"]);

                var result = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .AsEnumerable();
                result = maxProductsOnPage == 0 ? result : result.Take(maxProductsOnPage);
                
                return Result.Success(_mapper.Map<IEnumerable<Product>>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(GetProducts));
                return Result.Failure<IEnumerable<Product>>(new Error($"Exception was occurred in {nameof(GetProducts)}.", e));
            }
        }

        public Result<Product> GetProduct(int id)
        {
            try
            {
                var result = _context.Products
                    .Include(p => p.Category)
                    .Include(p => p.Supplier)
                    .FirstOrDefault(p => p.ProductId == id);

                return Result.Success(_mapper.Map<Product>(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(GetProduct));
                return Result.Failure<Product>(new Error($"Exception was occurred in {nameof(GetProducts)}.", e));
            }
        }

        public Result<ProductAndCategories> GetProductWithCategories(int id)
        {
            try
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
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(GetProductWithCategories));
                return Result.Failure<ProductAndCategories>(new Error($"Exception was occurred in {nameof(GetProductWithCategories)}.", e));
            }
        }

        public Result Update(Product product)
        {
            try
            {
                var result = _context.Products.Update(_mapper.Map<Data.Models.ProductEntity>(product));
                _context.SaveChanges();

                return Result.Success();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(Update));
                return Result.Failure(new Error($"Exception was occurred in {nameof(Update)}.", e));
            }
        }

        public Result Create(Product product)
        {
            try
            {
                var result = _context.Products.Add(_mapper.Map<Data.Models.ProductEntity>(product));
                _context.SaveChanges();

                return Result.Success(_mapper.Map<Product>(result.Entity));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(Create));
                return Result.Failure<Product>(new Error($"Exception was occurred in {nameof(Create)}.", e));
            }
        }
    }
}
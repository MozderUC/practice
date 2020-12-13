using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;
using NetCoreMentoring.Core.Utilities;
using NetCoreMentoring.Core.Utilities.ResultFlow;
using NetCoreMentoring.Data;

namespace NetCoreMentoring.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryService> _logger;

        public CategoryService(
            IConfiguration configuration,
            NorthwindContext context,
            IMapper mapper,
            ILogger<CategoryService> logger)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        public Result<IEnumerable<Category>> GetCategories()
        {
            return Result.Success(_mapper.Map<IEnumerable<Category>>(_context.Categories.AsEnumerable()));
        }

        public Result<Category> GetCategory(int categoryId)
        {
            var result = _context.Categories.Find(categoryId);

            return result == null
                ? Result.NotFound<Category>(new Error("Category don't found."))
                : Result.Success(_mapper.Map<Category>(result));
        }

        public Result<byte[]> GetPicture(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            return category == null
                ? Result.NotFound<byte[]>(new Error("Category don't found."))
                : Result.Success(category.Picture);
        }

        public Result UpdatePicture(int categoryId, string pictureName, byte[] picture)
        {
            var category = _context.Categories.Find(categoryId);
            var processFormFileResult = FileHelpers.ProcessFormFile(pictureName, picture);

            if (!processFormFileResult.IsSuccess) return processFormFileResult;

            category.Picture = processFormFileResult.Value;

            _context.Categories.Update(category);
            _context.SaveChanges();

            return Result.Success();
        }
    }
}
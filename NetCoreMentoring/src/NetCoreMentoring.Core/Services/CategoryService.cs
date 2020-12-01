using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;
using NetCoreMentoring.Core.Utilities;
using NetCoreMentoring.Data;

namespace NetCoreMentoring.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IConfiguration _configuration;
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public CategoryService(
            IConfiguration configuration,
            NorthwindContext context,
            IMapper mapper)
        {
            _configuration = configuration;
            _context = context;
            _mapper = mapper;
        }

        public IEnumerable<Category> GetCategories()
        {
            return _mapper.Map<IEnumerable<Category>>(_context.Categories.AsEnumerable());
        }

        public Category GetCategory(int categoryId)
        {
            var category = _context.Categories.Find(categoryId);

            if (category == null)
            {
                //TODO: return error
                throw new Exception();
            }

            return _mapper.Map<Category>(category);
        }

        public byte[] GetPicture(int categoryId)
        {
            return _context.Categories.Find(categoryId).Picture;
        }

        public void UpdatePicture(int categoryId, IFormFile newPicture)
        {
            var category = _context.Categories.Find(categoryId);
            category.Picture = FileHelpers.ProcessFormFile(newPicture);

            _context.Categories.Update(category);
            _context.SaveChanges();

            if (!Directory.Exists(_configuration["CacheImagePath"])) return;

            var cachedFiles = Directory.GetFiles(_configuration["CacheImagePath"]);
            var filePath = cachedFiles.FirstOrDefault(c => FileHelpers.GetImageId(c) == categoryId.ToString());

            if (filePath != null)
            {
                File.Delete(filePath);
            }
        }
    }
}
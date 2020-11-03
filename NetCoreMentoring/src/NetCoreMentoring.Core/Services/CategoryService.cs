using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;
using NetCoreMentoring.Core.Utilities;
using NetCoreMentoring.Data;

namespace NetCoreMentoring.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly NorthwindContext _context;
        private readonly IMapper _mapper;

        public CategoryService(
            NorthwindContext context,
            IMapper mapper)
        {
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

        public void UpdatePicture(int categoryId, IFormFile newPicture)
        {
            var category = _context.Categories.Find(categoryId);
            category.Picture = FileHelpers.ProcessFormFile(newPicture);

            _context.Categories.Update(category);
            _context.SaveChanges();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;
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
    }
}
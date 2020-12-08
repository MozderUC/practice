using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.App.Controllers
{
    public class CategoryController : ControllerMvcBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper,
            ILogger<CategoryController> logger)
            :base(mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            var result = _categoryService.GetCategories();

            return RequestResult<IEnumerable<Category>,IEnumerable<CategoryViewModel>>(result, View().ViewName);
        }

        //[ServiceFilter(typeof(ImageCacheFilter))]
        public IActionResult GetPicture(int categoryId)
        {
            return File(_categoryService.GetPicture(categoryId), "image/jpeg");
        }
        
        public IActionResult UpdatePicture(int categoryId)
        {
            var category = _categoryService.GetCategory(categoryId);

            return RequestResult<Category, CategoryPictureViewModel>(category, View().ViewName);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePicture(CategoryPictureViewModel categoryPictureViewModel)
        {
            var result = _categoryService.UpdatePicture(categoryPictureViewModel.CategoryId, categoryPictureViewModel.FormFile);

            return RedirectToAction(result, nameof(Index));
        }
    }
}
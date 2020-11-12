using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.App.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper,
            ILogger<CategoryController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            try
            {
                return View(_mapper.Map<IEnumerable<CategoryViewModel>>(_categoryService.GetCategories()));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(Index));
                throw;
            }
        }

        [ImageCache]
        public IActionResult GetPicture(int categoryId)
        {
            return File(_categoryService.GetPicture(categoryId), "image/jpeg");
        }

        public IActionResult UpdatePicture(int categoryId)
        {
            return View(_mapper.Map<CategoryPictureViewModel>(_categoryService.GetCategory(categoryId)));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePicture(CategoryPictureViewModel categoryPictureViewModel)
        {
            _categoryService.UpdatePicture(categoryPictureViewModel.CategoryId, categoryPictureViewModel.FormFile);

            return RedirectToAction(nameof(Index));
        }
    }
}
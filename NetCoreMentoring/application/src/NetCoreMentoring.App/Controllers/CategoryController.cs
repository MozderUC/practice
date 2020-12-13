using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;
using NetCoreMentoring.Core.Utilities;
using NuGet.Protocol.Core.v3;

namespace NetCoreMentoring.App.Controllers
{
    public class CategoryController : ControllerMvcBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(
            ICategoryService categoryService,
            IConfiguration configuration,
            IMapper mapper,
            ILogger<CategoryController> logger)
            :base(mapper)
        {
            _configuration = configuration;
            _logger = logger;
            _mapper = mapper;
            _categoryService = categoryService;
        }

        public IActionResult Index()
        {
            try
            {
                var result = _categoryService.GetCategories();

                return RequestResult<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(result, View().ViewName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(Index));
                return View("Error", e.ToJson());
            }
        }

        //[ServiceFilter(typeof(ImageCacheFilter))]
        public IActionResult GetPicture(int categoryId)
        {
            try
            {
                return File(_categoryService.GetPicture(categoryId), "image/jpeg");
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(GetPicture));
                return View("Error", e.ToJson());
            }
        }
        
        public IActionResult UpdatePicture(int categoryId)
        {
            try
            {
                var category = _categoryService.GetCategory(categoryId);

                return RequestResult<Category, CategoryPictureViewModel>(category, View().ViewName);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(UpdatePicture));
                return View("Error", e.ToJson());
            }
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePicture(CategoryPictureViewModel categoryPictureViewModel)
        {
            try
            {
                var newPicture = new MemoryStream();
                categoryPictureViewModel.FormFile.CopyTo(newPicture);

                var result = _categoryService.UpdatePicture(categoryPictureViewModel.CategoryId, categoryPictureViewModel.FormFile.FileName, newPicture.ToArray());

                if (Directory.Exists(_configuration["CacheImagePath"]))
                {
                    var cachedFiles = Directory.GetFiles(_configuration["CacheImagePath"]);
                    var filePath = cachedFiles.FirstOrDefault(c => FileHelpers.GetImageId(c) == categoryPictureViewModel.CategoryId.ToString());

                    if (filePath != null)
                    {
                        System.IO.File.Delete(filePath);
                    }
                }

                return RedirectToAction(result, nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(UpdatePicture));
                return View("Error", e.ToJson());
            }
        }
    }
}
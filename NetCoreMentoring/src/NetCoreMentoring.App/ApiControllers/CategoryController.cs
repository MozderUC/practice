using System.Collections.Generic;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.App.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/categories")]
    [ApiController]
    public class CategoryController : ControllerApiBase
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(
            ICategoryService categoryService,
            IMapper mapper)
            :base(mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetCategories")]
        public IActionResult GetCategories()
        {
            var result = _categoryService.GetCategories();

            return RequestResult<IEnumerable<Category>, IEnumerable<CategoryViewModel>>(result);
        }

        [HttpGet("{id:int}/picture", Name = "GetPicture")]
        [ServiceFilter(typeof(ImageCacheFilter))]
        public IActionResult GetPicture(int id)
        {
            return File(_categoryService.GetPicture(id), "image/jpeg");
        }

        [HttpPut("{id:int}/picture", Name = "UpdatePicture")]
        [ValidateAntiForgeryToken]
        public IActionResult UpdatePicture(CategoryPictureViewModel categoryPictureViewModel)
        {
            var result = _categoryService.UpdatePicture(categoryPictureViewModel.CategoryId, categoryPictureViewModel.FormFile);

            return RequestResult(result, HttpStatusCode.NoContent);
        }
    }
}

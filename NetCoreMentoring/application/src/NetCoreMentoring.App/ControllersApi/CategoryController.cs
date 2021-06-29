using System.Collections.Generic;
using System.IO;
using System.Net;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.App.ControllersApi
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
        public IActionResult GetPicture([FromRoute]int id)
        {
            return File(_categoryService.GetPicture(id), "image/jpeg");
        }

        [HttpPut("{id:int}/picture", Name = "UpdatePicture")]
        public IActionResult UpdatePicture([FromForm]CategoryPictureViewModel categoryPictureViewModel)
        {
            var newPicture = new MemoryStream();
            categoryPictureViewModel.FormFile.CopyTo(newPicture);

            var result = _categoryService.UpdatePicture(categoryPictureViewModel.CategoryId, categoryPictureViewModel.FormFile.FileName, newPicture.ToArray());
            return RequestResult(result, HttpStatusCode.NoContent);
        }
    }
}

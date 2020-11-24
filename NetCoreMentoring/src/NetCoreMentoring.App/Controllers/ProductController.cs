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
    public class ProductController : ControllerMvcBase
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductService productService,
            ICategoryService categoryService,
            IMapper mapper,
            ILogger<ProductController> logger)
            :base(mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _logger = logger;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            var result = _productService.GetProducts();

            return RequestResult<IEnumerable<Product>, IEnumerable<ProductViewModel>>(result, View().ViewName);
        }

        public IActionResult Edit(int id)
        {
            var result = _productService.GetProductWithCategories(id);

            return RequestResult<ProductAndCategories, ProductAndCategoriesViewModel>(result, View().ViewName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductAndCategoriesViewModel productAndCategoriesViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _productService.Update(_mapper.Map<Product>(productAndCategoriesViewModel.Product));

            return RedirectToAction(result, nameof(Index));

        }

        public IActionResult Create()
        {
            var categories = _categoryService.GetCategories();

            return RequestResult<IEnumerable<Category>, ProductAndCategoriesViewModel>(categories, View().ViewName);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductAndCategoriesViewModel productAndCategoriesViewModel)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _productService.Create(_mapper.Map<Product>(productAndCategoriesViewModel.Product));

            return RedirectToAction(result, nameof(Index));

        }
    }
}
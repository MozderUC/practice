using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.App.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(
            IProductService productService,
            IMapper mapper,
            ILogger<ProductController> logger)
        {
            _logger = logger;
            _mapper = mapper;
            _productService = productService;
        }

        public IActionResult Index()
        {
            try
            {
                var products = _productService.GetProducts();

                return View(_mapper.Map<IEnumerable<ProductViewModel>>(products));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}.", nameof(Index));
                throw;
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var product = _productService.GetProduct(id);

                if (product == null)
                {
                    return NotFound();
                }

                return View(_mapper.Map<ProductViewModel>(product));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}. Id: {Id}", nameof(Edit), id);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ProductViewModel productViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(productViewModel);

                _productService.Update(_mapper.Map<Core.Models.Product>(productViewModel));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}. Id: {Id}; Product: {@Product}", nameof(Edit), id, productViewModel);
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return View(productViewModel);

                _productService.Create(_mapper.Map<Core.Models.Product>(productViewModel));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}. Product: {@Product}", nameof(Create), productViewModel);
                throw;
            }
        }
    }
}
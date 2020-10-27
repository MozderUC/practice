using System;
using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NetCoreMentoring.Core.Services.Contracts;
using Product = NetCoreMentoring.App.Models.Product;

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

                return View(_mapper.Map<IEnumerable<Product>>(products));
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

                return View(_mapper.Map<Product>(product));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}. Id: {Id}", nameof(Edit), id);
                throw;
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Product product)
        {
            try
            {
                if (!ModelState.IsValid) return View(product);

                _productService.Update(_mapper.Map<Core.Models.Product>(product));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}. Id: {Id}; Product: {@Product}", nameof(Edit), id, product);
                throw;
            }
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Product product)
        {
            try
            {
                if (!ModelState.IsValid) return View(product);

                _productService.Create(_mapper.Map<Core.Models.Product>(product));

                return RedirectToAction(nameof(Index));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Exception was occurred in {Method}. Product: {@Product}", nameof(Create), product);
                throw;
            }
        }
    }
}
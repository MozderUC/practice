using System.Collections.Generic;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NetCoreMentoring.App.Infrastructure;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;

namespace NetCoreMentoring.App.ApiControllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/products")]
    [ApiController]
    public class ProductController : ControllerApiBase
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(
            IProductService productService,
            IMapper mapper)
            :base(mapper)
        {
            _productService = productService;
            _mapper = mapper;
        }

        [HttpGet(Name = "GetProducts")]
        public IActionResult GetProducts()
        {
            var result = _productService.GetProducts();

            return RequestResult<IEnumerable<Product>, IEnumerable<ProductViewModel>>(result);
        }

        [HttpPut("{id:int}", Name = "EditProduct")]
        public IActionResult EditProduct(int id, ProductViewModel product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _productService.Update(_mapper.Map<Product>(product));

            return RequestResult(result);
        }

        [HttpPost(Name = "Create")]
        public IActionResult Create(ProductViewModel product)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = _productService.Create(_mapper.Map<Product>(product));

            return RequestResult(result);
        }
    }
}

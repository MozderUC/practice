using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NetCoreMentoring.App.Controllers;
using NetCoreMentoring.App.Mapping;
using NetCoreMentoring.App.Models;
using NetCoreMentoring.Core.Models;
using NetCoreMentoring.Core.Services.Contracts;
using Xunit;

namespace NetCoreMentoring.App.Tests
{
    public class ProductControllerTests
    {
        private readonly IMapper mapper;
        private readonly ILogger<ProductController> logger;

        public ProductControllerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = new Mapper(mapperConfig);
            logger = new Mock<ILogger<ProductController>>().Object;
        }

        [Fact]
        public void Index_ReturnViewResult_ViewResult()
        {
            // Arrange
            var stubProductService = new Mock<IProductService>();
            var stubCategoryService = new Mock<ICategoryService>();
            stubProductService.Setup(s => s.GetProducts())
                .Returns(GetTestProducts());

            var controller = new ProductController(
                stubProductService.Object,
                stubCategoryService.Object,
                mapper,
                logger);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<ProductViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        [Fact]
        public void Edit_ProductIdNotExist_NotFound()
        {
            // Arrange
            int testProductId = 1;
            var stubProductService = new Mock<IProductService>();
            var stubCategoryService = new Mock<ICategoryService>();
            stubProductService.Setup(s => s.GetProduct(testProductId))
                .Returns((Product)null);

            var controller = new ProductController(
                stubProductService.Object,
                stubCategoryService.Object,
                mapper,
                logger);

            // Act
            var result = controller.Edit(testProductId);

            // Assert
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public void Edit_ProductIdExist_ViewResult()
        {
            // Arrange
            int testProductId = 1;
            var stubProductService = new Mock<IProductService>();
            var stubCategoryService = new Mock<ICategoryService>();
            stubProductService.Setup(s => s.GetProduct(testProductId))
                .Returns(new Product() {ProductId = 1});
            stubCategoryService.Setup(c => c.GetCategories())
                .Returns(new List<Category>() {new Category() {CategoryId = 12}});

            var controller = new ProductController(
                stubProductService.Object,
                stubCategoryService.Object,
                mapper,
                logger);

            // Act
            var result = controller.Edit(testProductId);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ModifyProductViewModel>(
                viewResult.ViewData.Model);
        }

        [Fact]
        public void EditPost_ModelStateIsInvalid_BadRequestResult()
        {
            // Arrange
            int testProductId = 1;
            var stubProductService = new Mock<IProductService>();
            var stubCategoryService = new Mock<ICategoryService>();
            stubProductService.Setup(s => s.Update(It.IsAny<Product>()));

            var modifyModel = new ModifyProductViewModel()
            {
                Product = new ProductViewModel() {ProductId = testProductId }
            };

            var controller = new ProductController(
                stubProductService.Object,
                stubCategoryService.Object,
                mapper,
                logger);
            controller.ModelState.AddModelError(nameof(ProductViewModel.ProductName), "Required");

            // Act
            var result = controller.Edit(testProductId, modifyModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void EditPost_ModelStateIsValid_RedirectAndAddProduct()
        {
            // Arrange
            int testProductId = 1;
            var stubProductService = new Mock<IProductService>();
            var stubCategoryService = new Mock<ICategoryService>();
            stubProductService.Setup(s => s.Update(It.IsAny<Product>()))
                .Verifiable();

            var modifyModel = new ModifyProductViewModel()
            {
                Product = new ProductViewModel() { ProductId = testProductId }
            };

            var controller = new ProductController(
                stubProductService.Object,
                stubCategoryService.Object,
                mapper,
                logger);

            // Act
            var result = controller.Edit(testProductId, modifyModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal(nameof(ProductController.Index), redirectToActionResult.ActionName);
            stubProductService.Verify();
        }

        [Fact]
        public void Create_ReturnViewResult_ViewResult()
        {
            // Arrange
            var stubProductService = new Mock<IProductService>();
            var stubCategoryService = new Mock<ICategoryService>();
            stubCategoryService.Setup(c => c.GetCategories())
                .Returns(new List<Category>() { new Category() { CategoryId = 1 } });

            var controller = new ProductController(
                stubProductService.Object,
                stubCategoryService.Object,
                mapper,
                logger);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.IsAssignableFrom<ModifyProductViewModel>(
                viewResult.ViewData.Model);
        }

        [Fact]
        public void CreatePost_ModelStateIsInvalid_BadRequestResult()
        {
            // Arrange
            int testProductId = 1;
            var stubProductService = new Mock<IProductService>();
            var stubCategoryService = new Mock<ICategoryService>();
            stubProductService.Setup(s => s.Create(It.IsAny<Product>()));

            var modifyModel = new ModifyProductViewModel()
            {
                Product = new ProductViewModel() { ProductId = testProductId }
            };

            var controller = new ProductController(
                stubProductService.Object,
                stubCategoryService.Object,
                mapper,
                logger);
            controller.ModelState.AddModelError(nameof(ProductViewModel.ProductName), "Required");

            // Act
            var result = controller.Create(modifyModel);

            // Assert
            var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.IsType<SerializableError>(badRequestResult.Value);
        }

        [Fact]
        public void CreatePost_ModelStateIsValid_RedirectAndAddProduct()
        {
            // Arrange
            int testProductId = 1;
            var stubProductService = new Mock<IProductService>();
            var stubCategoryService = new Mock<ICategoryService>();
            stubProductService.Setup(s => s.Create(It.IsAny<Product>()))
                .Verifiable();

            var modifyModel = new ModifyProductViewModel()
            {
                Product = new ProductViewModel() { ProductId = testProductId }
            };

            var controller = new ProductController(
                stubProductService.Object,
                stubCategoryService.Object,
                mapper,
                logger);

            // Act
            var result = controller.Create(modifyModel);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Null(redirectToActionResult.ControllerName);
            Assert.Equal(nameof(ProductController.Index), redirectToActionResult.ActionName);
            stubProductService.Verify();
        }

        private IEnumerable<Product> GetTestProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    ProductId = 1,
                    ProductName = "Product 1"
                },
                new Product()
                {
                    ProductId = 2,
                    ProductName = "Product 2"
                }
            };
        }
    }
}

using System.Collections.Generic;
using System.Linq;
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
    public class CategoryControllerTests
    {
        private readonly IMapper mapper;
        private readonly ILogger<CategoryController> logger;

        public CategoryControllerTests()
        {
            var mapperConfig = new MapperConfiguration(cfg => cfg.AddProfile(new MappingProfile()));
            mapper = new Mapper(mapperConfig);
            logger = new Mock<ILogger<CategoryController>>().Object;
        }

        [Fact]
        public void Index_ReturnViewResult_ViewResult()
        {
            // Arrange
            var stubCategoryService = new Mock<ICategoryService>();
            stubCategoryService.Setup(s => s.GetCategories())
                .Returns(GetTestCategories());

            var controller = new CategoryController(
                stubCategoryService.Object,
                mapper,
                logger);

            // Act
            var result = controller.Index();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsAssignableFrom<IEnumerable<CategoryViewModel>>(
                viewResult.ViewData.Model);
            Assert.Equal(2, model.Count());
        }

        private IEnumerable<Category> GetTestCategories()
        {
            return new List<Category>()
            {
                new Category()
                {
                    CategoryId = 1,
                    CategoryName = "Cars",
                    Description = "New cars"
                },
                new Category() {
                    CategoryId = 2,
                    CategoryName = "Balls",
                    Description = "Great balls"
                }
            };
        }
    }
}

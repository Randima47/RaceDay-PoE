using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Controllers;
using RaceDay.Api.Data;
using RaceDay.Api.Dtos;
using RaceDay.Api.Models;
using Xunit;

namespace RaceDay.Tests
{
    public class CategoriesControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetCategoriesByEvent_ReturnsOkResult()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.EventCategories.Add(new EventCategory { Id = 1, Name = "Pro 10k", EventId = 1 });
            await context.SaveChangesAsync();

            var controller = new CategoriesController(context);

            // Act
            var result = await controller.GetCategoriesByEvent(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }

        [Fact]
        public async Task CreateCategory_ReturnsOkResult_WhenValid()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new CategoriesController(context);
            var categoryDto = new CategoryDto
            {
                Name = "Junior 5k",
                EventId = 1
            };

            // Act
            var result = await controller.CreateCategory(categoryDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }
    }
}

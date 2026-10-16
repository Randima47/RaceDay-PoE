using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Controllers;
using RaceDay.Api.Data;
using RaceDay.Api.Dtos;
using RaceDay.Api.Models;
using Xunit;

namespace RaceDay.Tests
{
    public class ResultsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task SubmitResult_ReturnsOkResult_WhenValid()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new ResultsController(context);
            var resultDto = new ResultDto
            {
                EnrolmentId = 1,
                FinishTime = TimeSpan.FromMinutes(45),
                Position = 1
            };

            // Act
            var result = await controller.SubmitResult(resultDto);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            Assert.NotNull(okResult.Value);
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Controllers;
using RaceDay.Api.Data;
using RaceDay.Api.Dtos;
using RaceDay.Api.Models;
using Xunit;

namespace RaceDay.Tests
{
    public class EventsControllerTests
    {
        private ApplicationDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task GetEvents_ReturnsOkResult_WithListOfEvents()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            context.Events.Add(new Event { Id = 1, Title = "Marathon 2026", Description = "10k Run", Location = "City Park", EventDate = DateTime.UtcNow });
            await context.SaveChangesAsync();

            var controller = new EventsController(context);

            // Act
            var result = await controller.GetEvents();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var events = Assert.IsAssignableFrom<IEnumerable<EventDto>>(okResult.Value);
            Assert.NotEmpty(events);
        }

        [Fact]
        public async Task CreateEvent_ReturnsCreatedAtAction_WhenValid()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var controller = new EventsController(context);
            var newEventDto = new EventDto
            {
                Title = "Trail Run",
                Description = "Mountain Trail",
                Location = "Highlands",
                EventDate = DateTime.UtcNow.AddDays(10)
            };

            // Act
            var result = await controller.CreateEvent(newEventDto);

            // Assert
            var createdAtResult = Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.NotNull(createdAtResult.Value);
        }
    }
}

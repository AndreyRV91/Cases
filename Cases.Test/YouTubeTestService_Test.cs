using Cases.Controllers;
using Cases.Domain.Contracts;
using Cases.Domain.Implementations;
using Cases.Domain.Implementations.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Cases.Test
{
    public class YouTubeTestService_Test
    {
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public async Task CasesController_ReturnsOkObjectResult(int id)
        {
            //Arrange
            var service = new Mock<IYouTubeTestService>();
            var controller = new CasesController(service.Object);
            var model = new CaseModel() { Id = id};

            //Act
            var actionResult = await controller.TestCase(model);

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(actionResult);
        }

        [TestCase(TestType.TestYouTubeLike)]
        public async Task TestYouTubeLike_ReturnsFalse(int id)
        {
            //Arrange
            var service = new Mock<IYouTubeTestService>();

            //Act
            var result = await service.Object.DoTests(id);

            //Assert
            Assert.AreEqual(result, false);

        }

        [TestCase(TestType.TestYouTubeComment)]
        public async Task TestYouTubeComment_ReturnsTrue(int id)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<YouTubeTestService>>();
            var service = new YouTubeTestService(mockLogger.Object);

            //Act
            var result = await service.DoTests(id);

            //Assert
            Assert.AreEqual(result, true);

        }

        [TestCase(TestType.TestYouTubeSearch)]
        public async Task TestYouTubeSearch_ReturnsTrue(int id)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<YouTubeTestService>>();
            var service = new YouTubeTestService(mockLogger.Object);

            //Act
            var result = await service.DoTests(id);

            //Assert
            Assert.AreEqual(result, true);

        }

        [TestCase(4)]
        public async Task DoTests_ReturnsFalse(int id)
        {
            //Arrange
            var mockLogger = new Mock<ILogger<YouTubeTestService>>();
            var service = new YouTubeTestService(mockLogger.Object);

            //Act
            var result = await service.DoTests(id);

            //Assert
            Assert.AreEqual(result, false);
        }

    }
}
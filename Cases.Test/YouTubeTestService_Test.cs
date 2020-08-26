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
        IYouTubeTestService service;
        Mock<ILogger<YouTubeTestService>> mockLogger;

        [SetUp]
        public void SetUp()
        {
            mockLogger = new Mock<ILogger<YouTubeTestService>>();
            service = new YouTubeTestService(mockLogger.Object);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        public async Task CasesController_ReturnsOkObjectResult(int id)
        {
            //Arrange
            var controller = new CasesController(service);
            var model = new CaseModel() { Id = id};

            //Act
            var actionResult = await controller.TestCase(model);

            //Assert
            Assert.IsAssignableFrom<OkObjectResult>(actionResult);
        }

        [TestCase(TestType.TestYouTubeLike)]
        public async Task TestYouTubeLike_ReturnsFalse(int id)
        {
            //Act
            var result = await service.DoTests(id);

            //Assert
            Assert.AreEqual(result, false);

        }

        [TestCase(TestType.TestYouTubeComment)]
        public async Task TestYouTubeComment_ReturnsTrue(int id)
        {
            //Act
            var result = await service.DoTests(id);

            //Assert
            Assert.AreEqual(result, true);

        }

        [TestCase(TestType.TestYouTubeSearch)]
        public async Task TestYouTubeSearch_ReturnsTrue(int id)
        {
            //Act
            var result = await service.DoTests(id);

            //Assert
            Assert.AreEqual(result, true);

        }

        [TestCase(4)]
        public async Task DoTests_ReturnsFalse(int id)
        {
            //Act
            var result = await service.DoTests(id);

            //Assert
            Assert.AreEqual(result, false);
        }

    }
}
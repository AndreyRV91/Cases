using Cases.Domain.Contracts;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.IO;
using System.Threading.Tasks;
using OpenQA.Selenium.Interactions;
using System.Reflection;

namespace Cases.Domain.Implementations
{
    public class YouTubeTestService : IYouTubeTestService
    {
        private readonly ILogger<YouTubeTestService> _logger;

        public YouTubeTestService(ILogger<YouTubeTestService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> DoTests(int id)
        {
            try
            {
                ITest test = TestsFactory.GetTest(id);

                using (var driver = new ChromeDriver(Directory.GetCurrentDirectory()))
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                    driver.Navigate().GoToUrl("https://www.youtube.com/");

                    var result = await test.Execute(driver, wait);
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }

        }

    }

}

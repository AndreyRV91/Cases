using Cases.Domain.Contracts;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using SeleniumExtras.WaitHelpers;
using OpenQA.Selenium.Interactions;

namespace Cases.Domain.Implementations
{
    public class YouTubeLikeService : IYouTubeLikeService
    {
        private readonly ILogger<YouTubeLikeService> _logger;

        public YouTubeLikeService(ILogger<YouTubeLikeService> logger)
        {
            _logger = logger;
        }

        public async Task<bool> DoTests(int id)
        {
            switch (id)
            {
                case 1:
                    TestYouTubeLike();
                    break;
                case 2:
                    var t = await TestYouTubeComment();
                    break;
                case 3:
                    TestYouTubeSearch();
                    break;
            }
            return true;
        }

        private bool TestYouTubeLike()
        {
            try
            {
                using (var driver = new ChromeDriver(Directory.GetCurrentDirectory()))
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                    driver.Navigate().GoToUrl("https://www.youtube.com/");


                    //go to trends
                    wait.Until(d => driver.FindElementByXPath("//a[contains(@href,'/feed/trending')]").Displayed);
                    var trendButton = driver.FindElementByXPath("//a[contains(@href,'/feed/trending')]");
                    trendButton.Click();

                    //go to second video
                    wait.Until(d => driver.FindElementsByXPath("//ytd-video-renderer").Count > 3);
                    var videoButton = driver.FindElementsByXPath("//ytd-video-renderer")[2];
                    videoButton.Click();

                    //like video
                    wait.Until(d => driver.FindElementsByXPath("//div[contains(@id,'top-level-buttons')]//ytd-toggle-button-renderer").Count > 0);
                    var likeButton = driver.FindElementsByXPath("//div[contains(@id,'top-level-buttons')]//ytd-toggle-button-renderer")[0];
                    likeButton.Click();

                    //is like pressed
                    wait.Until(d => driver.FindElementByXPath("//ytd-toggle-button-renderer[1]//a[1]//yt-icon-button[1]//button[1]").Displayed);
                    var pressedLikeButton = driver.FindElementByXPath("//ytd-toggle-button-renderer[1]//a[1]//yt-icon-button[1]//button[1]");

                    return pressedLikeButton.GetAttribute("aria-pressed").Contains("true");
                }
            }
            catch (Exception ex)
            {
                _logger.LogDebug(ex.Message);
                return false;
            }
        }

        private async Task<bool> TestYouTubeComment()
        {
            try
            {
                using (var driver = new ChromeDriver(Directory.GetCurrentDirectory()))
                {
                    WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));

                    driver.Navigate().GoToUrl("https://www.youtube.com/");

                    //go to trends
                    wait.Until(d => driver.FindElementByXPath("//a[contains(@href,'/feed/trending')]").Displayed);
                    var trendButton = driver.FindElementByXPath("//a[contains(@href,'/feed/trending')]");
                    trendButton.Click();

                    //go to seventh video
                    wait.Until(d => driver.FindElementsByXPath("//ytd-video-renderer").Count > 7);
                    var videoButton = driver.FindElementsByXPath("//ytd-video-renderer")[6];
                    videoButton.Click();

                    await Task.Delay(500);

                    //scroll to comment input
                    driver.ExecuteScript("window.scrollTo(0,5000)");
                    await Task.Delay(1000);
                    driver.ExecuteScript("window.scrollTo(0,10000)");
                    await Task.Delay(1000);

                    //press on comment input
                    wait.Until(d => driver.FindElementByXPath("//yt-formatted-string[contains(@id, 'simplebox-placeholder')]").Displayed);
                    var commentInput = driver.FindElementByXPath("//yt-formatted-string[contains(@id, 'simplebox-placeholder')]");

                    Actions builder = new Actions(driver);
                    builder.ClickAndHold(commentInput).Build().Perform();

                    return driver.Url.Contains(@"https://accounts.google.com/signin/");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
        }

        private void TestYouTubeSearch()
        {
            using (var driver = new ChromeDriver())
            {
                driver.Navigate().GoToUrl("https://www.youtube.com/");

                var trendTab = driver.FindElementByXPath("//a[contains(@href,'/feed/trending')]");
            }
        }

    }

}

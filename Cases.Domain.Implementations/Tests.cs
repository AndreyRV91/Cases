using Cases.Domain.Contracts;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System.Threading.Tasks;

namespace Cases.Domain.Implementations.Tests
{
    public class TestYouTubeLike: ITest
    {
        public async Task<bool> Execute(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
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
            catch
            {
                throw;
            }
        }
    }

    public class TestYouTubeComment: ITest
    {
        public async Task<bool> Execute(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                //go to trends
                wait.Until(d => driver.FindElementByXPath("//a[contains(@href,'/feed/trending')]").Displayed);
                var trendButton = driver.FindElementByXPath("//a[contains(@href,'/feed/trending')]");
                trendButton.Click();

                //go to seventh video
                wait.Until(d => driver.FindElementsByXPath("//ytd-video-renderer").Count > 7);
                var videoButton = driver.FindElementsByXPath("//ytd-video-renderer")[6];
                videoButton.Click();

                //scroll to comment input
                await Task.Delay(500);
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
            catch
            {
                throw;
            }
        }
    }

    public class TestYouTubeSearch : ITest
    {
        public async Task<bool> Execute(ChromeDriver driver, WebDriverWait wait)
        {
            try
            {
                //input in search
                wait.Until(d => driver.FindElementByXPath("//input[contains(@id,'search')]").Displayed);
                var searchInput = driver.FindElementByXPath("//input[contains(@id,'search')]");
                searchInput.SendKeys("kaspersky");

                //click search button
                wait.Until(d => driver.FindElementByXPath("//button[contains(@id,'search-icon-legacy')]").Displayed);
                var searchbutton = driver.FindElementByXPath("//button[contains(@id,'search-icon-legacy')]");
                searchbutton.Click();

                //check presence of Kaspersky user on page
                wait.Until(d => driver.FindElementByXPath("//a[contains(@href,'/user/Kaspersky')]").Displayed);
                var kasperskyLink = driver.FindElementByXPath("//a[contains(@href,'/user/Kaspersky')]");

                return !string.IsNullOrEmpty(kasperskyLink.GetAttribute("href"));
            }
            catch
            {
                throw;
            }
        }
    }
}

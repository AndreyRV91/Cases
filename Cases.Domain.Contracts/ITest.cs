using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Threading.Tasks;

namespace Cases.Domain.Contracts
{
    public interface ITest
    {
        Task<bool> Execute(ChromeDriver driver, WebDriverWait wait);
    }
}

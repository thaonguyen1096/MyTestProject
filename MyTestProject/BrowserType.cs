using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;

namespace MyTestProject
{
    public enum BrowserType
    {
        Chrome,
        Edge,
        Firefox
    }
    public class DriverFactory
    {
        public static IWebDriver InitBrowser(BrowserType browser, bool isHeadless)
        {
            switch (browser)
            {
                case BrowserType.Chrome:
                    var options = new ChromeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal
                    };
                    options.AddArguments("incognito");
                    if (isHeadless)
                    {
                        options.AddArguments("--headless");
                    }                    
                    var service = ChromeDriverService.CreateDefaultService();
                    return new ChromeDriver(service, options);
                case BrowserType.Edge:
                    var edgeOptions = new EdgeOptions
                    {
                        PageLoadStrategy = PageLoadStrategy.Normal
                    };
                    edgeOptions.AddArguments("incognito");
                    if (isHeadless)
                    {
                        edgeOptions.AddArguments("--headless");
                    }
                    var edgeService = EdgeDriverService.CreateDefaultService();
                    return new EdgeDriver(edgeService, edgeOptions);
                default:
                    return null;
            }
        }
    }
}

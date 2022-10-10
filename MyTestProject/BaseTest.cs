using Bogus;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;

namespace MyTestProject
{
    [TestClass]
    public abstract class BaseTest
    {
        protected static IWebDriver driver;
        protected static WebDriverWait wait;
        protected Faker fake;

        [AssemblyInitialize]
        public static void Init(TestContext test)
        {
            driver = DriverFactory.InitBrowser(BrowserType.Chrome, false);
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));
        }

        [TestInitialize]
        public void InitTest()
        {
            driver.Navigate().GoToUrl("https://react-redux.realworld.io");
            fake = new Faker();
        }

        [AssemblyCleanup]
        public static void AssemblyCleanUp()
        {
            driver.Close();

        }
    }
}

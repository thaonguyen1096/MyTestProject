using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using FluentAssertions;
using SeleniumExtras.WaitHelpers;
using Bogus;

namespace MyTestProject
{
    [TestClass]
    public class Login
    {
        private IWebDriver driver;
        private WebDriverWait wait;
        private IWebElement Txt_Email;
        private IWebElement Txt_Password;
        private string email;
        private string password;
        private string username;

        [TestMethod]
        public void SignUp()
        {
            IWebElement Lnk_SignUp = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(@href,'#register')]")));
            Lnk_SignUp.Click();

            IWebElement btn_login = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@type='submit']")));
            IWebElement Txt_Username = driver.FindElement(By.XPath("//input[@type='text']"));
            Txt_Password = driver.FindElement(By.XPath("//input[@type='password']"));
            Txt_Email = driver.FindElement(By.XPath("//input[@type='email']"));

            Txt_Username.SendKeys(username);
            Txt_Email.SendKeys(email);
            Txt_Password.SendKeys(password);
            btn_login.Click();

            IWebElement Lbl_UserAccount = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(@href,'#@" + username + "')]")));
            Lbl_UserAccount.Displayed.Should().BeTrue();
        }

        [TestMethod]
        public void FailToLogin()
        {
            IWebElement Lnk_Login = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//a[contains(@href,'#login')]")));
            Lnk_Login.Click();

            IWebElement btn_login = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//button[@type='submit']")));
            Txt_Password = driver.FindElement(By.XPath("//input[@type='password']"));
            Txt_Email = driver.FindElement(By.XPath("//input[@type='email']"));

            Txt_Email.SendKeys(email);
            Txt_Password.SendKeys(password);
            btn_login.Click();

            IWebElement Lbl_ErrorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath("//ul[@class='error-messages']/li")));
            Lbl_ErrorMessage.Text.Should().Be("email or password is invalid");
        }

        [TestInitialize]
        public void Init()
        {
            var options = new ChromeOptions
            {
                PageLoadStrategy = PageLoadStrategy.Normal
            };
            options.AddArguments("incognito");
            options.AddArguments("--headless");
            var service = ChromeDriverService.CreateDefaultService();

            driver = new ChromeDriver(service, options);
            driver.Navigate().GoToUrl("https://react-redux.realworld.io");

            var fake = new Faker();
            wait = new WebDriverWait(driver, new TimeSpan(0, 0, 30));

            email = fake.Person.Email;
            password = fake.Random.String(10);
            username = fake.Name.FirstName() + fake.Random.Number(100, 1000);
        }

        [TestCleanup]
        public void TestClean()
        {
            driver.Close();
        }

    }
}
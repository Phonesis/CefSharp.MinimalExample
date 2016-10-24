namespace CefSharp.Ui.Tests
{
    using System;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using OpenQA.Selenium;
    using OpenQA.Selenium.Chrome;
    using OpenQA.Selenium.Support.UI;

    [TestClass]
    public class SeleniumTests
    {
        private readonly IWebDriver cefDriver;

        public SeleniumTests()
        {
            var service = ChromeDriverService.CreateDefaultService();
            var options = new ChromeOptions { BinaryLocation = @"C:\Users\mpoole\Downloads\cef_binary_3.2704.1434.gec3e9ed_windows64_client\cef_binary_3.2704.1434.gec3e9ed_windows64_client\Release\cefclient.exe" };
            options.AddArgument("url=data:,");
            cefDriver = new ChromeDriver(service, options);
            cefDriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        [TestMethod]
        public void TestCefFrame()
        {
            cefDriver.Navigate().GoToUrl("https://www.google.co.uk");
            cefDriver.FindElement(By.Name("q")).SendKeys("CefSharp");
            cefDriver.FindElement(By.Name("btnG")).Click();
            Assert.IsNotNull(IsElementPresent(By.LinkText("cefsharp · GitHub")));
        }

        private IWebElement IsElementPresent(By identifier)
        {
            var condition = new WebDriverWait(cefDriver, TimeSpan.FromSeconds(5));
            try
            {
                return condition.Until(x => x.FindElement(identifier));
            }
            catch (Exception)
            {
                return null;
            }
        }

        [TestCleanup]
        public void CloseCef()
        {
            cefDriver.Quit();
        }
    }
}

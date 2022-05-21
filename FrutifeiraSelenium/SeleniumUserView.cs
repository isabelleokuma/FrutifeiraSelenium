//Inside SeleniumTest.cs

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;


namespace SeleniumCsharp

{
    public class UserTests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            driver = new ChromeDriver(path + @"\drivers\");
            driver.Navigate().GoToUrl("http://localhost:8081/");
        }

        [Test]
        public async Task selectCondominiumModal()
        {
            //Arrange
            driver.FindElement(By.Id("inputModalCondominios")).SendKeys("Condo");
            var parentDiv = driver.FindElement(By.Id("listCondominios"));
            var lis = parentDiv.FindElements(By.TagName("li"));
            var li = lis[0];

            //Act
            lis[0].Click();
            await Task.Delay(2000);

            //Assert
            driver.FindElement(By.Id("clickQualCondominio")).Click();
            var label = driver.FindElement(By.Id("textCondominuoSelected"));
            Assert.True(!string.IsNullOrEmpty(label.Text));
            GoHome();

        }

        [Test]
        public async Task searchProducts()
        {
            //
            driver.FindElement(By.Id("inputSearchBar")).SendKeys("morango");
            await Task.Delay(2000);
        }

        [Test]
        public void addProductToCart()
        {
            driver.FindElement(By.Id("clickQualCondominio"));
            //driver.FindElement(By.Id("clickFechaCondominio"));
            //verifyCondominiumModal();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public void GoHome() => driver.Navigate().GoToUrl("http://localhost:8081/");


    }

}

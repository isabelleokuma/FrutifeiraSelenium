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
            driver.Navigate().GoToUrl("http://localhost:8080/");
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
            selectCondominiumModal();
            await Task.Delay(6000);

            //Arrange
            driver.FindElement(By.Id("inputSearchBar")).SendKeys("Moran");
            await Task.Delay(2000);
            var parentDiv = driver.FindElement(By.Id("listProdutos"));
            var lis = parentDiv.FindElements(By.TagName("li"));
            var li = lis[0];

            //Act
            li.Click();

            //Assert
            Assert.False(driver.Url.Equals("http://localhost:8080/"));
            Assert.True(driver.Url.Equals("http://localhost:8080/ListAllProducts/62830c2ad8e1850ac3348138?search=Morango"));
        }

        [Test]
        public void addProductToCart()
        {
            driver.FindElement(By.Id("clickQualCondominio"));
            //driver.FindElement(By.Id("clickFechaCondominio"));
            //verifyCondominiumModal();
        }

        [Test]
        public async Task goToMarketVendorStand()
        {
            selectCondominiumModal();
            await Task.Delay(2000);

            //Arrange
            var stand = driver.FindElement(By.Id("stand1"));

            //Act
            stand.Click();
            await Task.Delay(2000);

            //Assert
            Assert.False(driver.Url.Equals("http://localhost:8080/"));
            Assert.True(driver.Url.Equals("http://localhost:8080/ListProduct/627af846b45b280fe8438f51"));
        }

        [Test]
        public async Task selectAllTypesCategory()
        {
            selectCondominiumModal();
            await Task.Delay(3000);

            //Arrange
            var categories = driver.FindElement(By.Id("clickCategorias"));

            //Act
            categories.Click();
            await Task.Delay(3000);

            //Arrange
            var categoryAll = driver.FindElement(By.Id("clickCategorias6"));

            //Act
            categoryAll.Click();
            await Task.Delay(5000);

            //Assert
            Assert.False(driver.Url.Equals("http://localhost:8080/"));
            Assert.True(driver.Url.Equals("http://localhost:8080/ListAllProducts/62830c2ad8e1850ac3348138?type=all"));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public void GoHome() => driver.Navigate().GoToUrl("http://localhost:8080/");


    }

}

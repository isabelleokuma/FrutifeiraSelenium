using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumCsharp
{
    public class MarketVendorTests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            driver = new ChromeDriver(path + @"\drivers\");
            driver.Navigate().GoToUrl("http://localhost:8081/LoginAdm");
        }

        [Test]
        public async Task MarketVendorLogin()
        {
            //Arrange
            driver.FindElement(By.Id("login_feirante"));
            driver.FindElement(By.Id("loginEmail")).SendKeys("feirante1@gmail.com");
            driver.FindElement(By.Id("loginSenha")).SendKeys("feirante");
            var btnLoginEntrar = driver.FindElement(By.Id("btnLoginEntrar"));

            //Act
            btnLoginEntrar.Click();
            await Task.Delay(2000);

            //Assert
            Assert.False(driver.Url.Equals("http://localhost:8081/LoginAdm"));
            Assert.True(driver.Url.Equals("http://localhost:8081/Products"));
        }

        [Test]
        public async Task MarketVendorAddProduct()
        {
            MarketVendorLogin();

            await Task.Delay(2000);
            //Arrange
            var abaCadastroProduto = driver.FindElement(By.Id("marketVendor_cart-add"));

            //Act
            abaCadastroProduto.Click();
            await Task.Delay(2000);

            //Arrange
            driver.FindElement(By.Id("nomeProduto")).SendKeys("melancia");
            var parentDiv = driver.FindElement(By.Id("listaTipoProduto"));
            var lis = parentDiv.FindElements(By.TagName("div"));
            var span = lis[1];

            //Act
            await Task.Delay(2000);
            span.Click();
            await Task.Delay(2000);

            //Arrange
            driver.FindElement(By.Id("descricaoProduto")).SendKeys("melancia meu amor todinho");
            driver.FindElement(By.Id("precoProduto")).SendKeys("14");
            driver.FindElement(By.Id("qtdProduto")).SendKeys("2500");
            driver.FindElement(By.Id("radioUnidadeProduto")).Click();
            driver.FindElement(By.Id("descontoProduto")).SendKeys("5");
            var btnSalvarProduto = driver.FindElement(By.Id("btnSalvarProduto"));

            //Act
            await Task.Delay(5000);
            btnSalvarProduto.Click();
            await Task.Delay(2000);

            //Assert
            var aux = driver.FindElement(By.Id("marketVendor_cart"));
            //Assert.True(driver.FindElement(By.Id("marketVendor_cart")).GetCssValue("")
        }
    }
}

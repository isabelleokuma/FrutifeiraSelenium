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
            driver.Navigate().GoToUrl("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/LoginAdm");
        }

        [Test, Order(1)]
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
            Assert.False(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/LoginAdm"));
            Assert.True(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/Products"));
        }

        [Test, Order(2)]
        public async Task MarketVendorAddProduct()
        {
            MarketVendorLogin();
            await Task.Delay(10000);

            //Arrange
            var abaCadastroProduto = driver.FindElement(By.Id("marketVendor_cart-add"));

            //Act
            abaCadastroProduto.Click();
            await Task.Delay(10000);

            //Arrange
            driver.FindElement(By.Id("nomeProduto")).SendKeys("Rabanete");
            driver.FindElement(By.Id("descricaoProduto")).SendKeys("Se você ainda não conhece a origem do rabanete, " +
                "saiba que ele é uma hortaliça da mesma família da couve, " +
                "do brócolis e da couve-flor.");
            driver.FindElement(By.Id("precoProduto")).SendKeys("7");
            driver.FindElement(By.Id("qtdProduto")).SendKeys("250");
            driver.FindElement(By.Id("radioGramasProduto")).Click();
            driver.FindElement(By.Id("selectImg")).SendKeys("https://www.proativaalimentos.com.br/image/cache/catalog/img_prod/folha-de-rabanete[1]-1000x1000.jpg");
            driver.FindElement(By.Id("descontoProduto")).SendKeys("5");
            var btnSalvarProduto = driver.FindElement(By.Id("btnSalvarProduto"));

            //Act
            await Task.Delay(10000);
            btnSalvarProduto.Click();
            await Task.Delay(2000);

            //Assert
            var aux = driver.FindElement(By.Id("marketVendor_cart"));
            string s = aux.GetAttribute("class");
            Assert.True(s.Equals("active"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}

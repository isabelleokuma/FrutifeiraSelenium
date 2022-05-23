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
            driver.Navigate().GoToUrl("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/");
        }

        [Test, Order(1)]
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

        [Test, Order(3)]
        public async Task Login()
        {
            selectCondominiumModal();
            await Task.Delay(10000);

            //Arrange & Act
            driver.FindElement(By.Id("clickLogin")).Click();
            await Task.Delay(10000);
            driver.FindElement(By.Id("clickLabelLogin")).Click();
            await Task.Delay(2000);
            driver.FindElement(By.Id("modalLoginEmail")).SendKeys("raphaelkonichi@gmail.com");
            driver.FindElement(By.Id("modalLoginSenha")).SendKeys("fruti123");
            await Task.Delay(1000);
            driver.FindElement(By.Id("btnModalEntrar")).Click();
            await Task.Delay(2000);
            driver.FindElement(By.Id("clickLogin")).Click();
            await Task.Delay(10000);

            //Assert
            Assert.True(driver.FindElement(By.Id("labelSair")).Text.ToUpper().Equals("SAIR"));

        }

        [Test, Order(2)]
        public async Task signUp()
        {
            Random rnd = new Random();
            int num = rnd.Next();
            string numString = num.ToString();

            selectCondominiumModal();
            await Task.Delay(10000);

            //Arrange & Act
            driver.FindElement(By.Id("clickLogin")).Click();
            await Task.Delay(10000);
            driver.FindElement(By.Id("clickLabelSignUp")).Click();
            await Task.Delay(1000);
            driver.FindElement(By.Id("inputNomeCadastro")).SendKeys("Jungkook");
            driver.FindElement(By.Id("inputSobrenomeCadastro")).SendKeys("Jeon");
            driver.FindElement(By.Id("inputEmailCadastro")).SendKeys("jungkook" + numString + "@ig.com.br");
            driver.FindElement(By.Id("inputFoneCadastro")).SendKeys("11988283837");
            driver.FindElement(By.Id("inputCPFCadastro")).SendKeys("25844966711");
            driver.FindElement(By.Id("inputSenhaCadastro")).SendKeys("bts123");
            driver.FindElement(By.Id("inputConfirmSenhaCadastro")).SendKeys("bts123");
            await Task.Delay(1000);
            driver.FindElement(By.Id("saveUserButton")).Click();
            await Task.Delay(3000);
            driver.FindElement(By.Id("modalLoginEmail")).SendKeys("jungkook" + numString + "@ig.com.br");
            driver.FindElement(By.Id("modalLoginSenha")).SendKeys("bts123");
            await Task.Delay(1000);
            driver.FindElement(By.Id("btnModalEntrar")).Click();
            await Task.Delay(2000);
            driver.FindElement(By.Id("clickLogin")).Click();
            await Task.Delay(10000);

            //Assert
            Assert.True(driver.FindElement(By.Id("labelSair")).Text.ToUpper().Equals("SAIR"));

            //Act
            driver.FindElement(By.Id("labelSair")).Click();
            await Task.Delay(2000);
        }

        [Test, Order(7)]
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
            Assert.False(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/"));
            Assert.True(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/ListAllProducts/62830c2ad8e1850ac3348138?search=Morango"));
        }

        [Test, Order(5)]
        public async Task addProductToCart()
        {
            GoHome();
            await Task.Delay(2000);

            selectCondominiumModal();
            await Task.Delay(6000);

            //Arrange
            var cardPromo = driver.FindElement(By.Id("cardPromo_627fd1107043f7a5efa51789"));

            //Act
            cardPromo.Click();
            await Task.Delay(1500);
            driver.FindElement(By.Id("clickAumentarProduto")).Click();
            await Task.Delay(1500);
            driver.FindElement(By.Id("clickReduzirProduto")).Click();
            await Task.Delay(2000);
            driver.FindElement(By.Id("btnAdicionarProduto")).Click();
            await Task.Delay(2000);
            driver.FindElement(By.Id("clickFecharProduto")).Click();
            await Task.Delay(2000);

            //Assert
            Assert.False(driver.FindElement(By.Id("totalCarrinho")).Equals("R$ 0,00"));
        }

        [Test, Order(6)]
        public async Task goToMarketVendorStand()
        {
            GoHome();
            await Task.Delay(2000);

            selectCondominiumModal();
            await Task.Delay(6000);

            //Arrange
            var stand = driver.FindElement(By.Id("stand1"));

            //Act
            stand.Click();
            await Task.Delay(2000);

            //Assert
            Assert.False(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/"));
            Assert.True(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/ListProduct/627af846b45b280fe8438f51"));
        }

        [Test, Order(4)]
        public async Task selectAllTypesCategory()
        {
            selectCondominiumModal();
            await Task.Delay(6000);

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
            Assert.False(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/"));
            Assert.True(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/ListAllProducts/62830c2ad8e1850ac3348138?type=all"));

        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        public void GoHome() => driver.Navigate().GoToUrl("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/");


    }

}

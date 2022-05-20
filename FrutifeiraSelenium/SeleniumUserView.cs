//Inside SeleniumTest.cs

using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
 using System;
using System.Collections.ObjectModel;
using System.IO;

namespace SeleniumCsharp

{
    public class Tests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            driver = new ChromeDriver(path + @"\drivers\");
            driver.Navigate().GoToUrl("https://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/");
        }

        [Test]
        public void verifyCondominiumModal()
        {
            driver.FindElement(By.Id("inputModalCondominios")).SendKeys("Condo");
            driver.FindElement(By.Id("modalCondominios")).Submit();
        }

        [Test]
        public void verifyCategories()
        {           
            driver.FindElement(By.Id("clickCategorias"));
            driver.FindElement(By.Id("clickCategorias1"));
            driver.FindElement(By.Id("clickCategorias2"));
            driver.FindElement(By.Id("clickCategorias3"));
            driver.FindElement(By.Id("clickCategorias4"));
            driver.FindElement(By.Id("clickCategorias5"));
            driver.FindElement(By.Id("clickCategorias"));
        }

        [Test]
        public void verifySearchBar()
        {
            driver.FindElement(By.Id("inputSearchBar")).SendKeys("morango");
            driver.FindElement(By.Id("modalCondominios")).Submit();
        }

        [Test]
        public void verifyModalChangeCondominium()
        {
            driver.FindElement(By.Id("clickQualCondominio"));
            driver.FindElement(By.Id("clickFechaCondominio"));
            verifyCondominiumModal();
        }

        [Test]
        public void verifyLateralCart()
        {
            driver.FindElement(By.Id("clickCart"));
            //colocar id no resto desse modal, incluse botão
        }

        [Test]
        public void verifyProfileMenu()
        {
            driver.FindElement(By.Id("clickLogin"));
        }

        [Test]
        public void verifyProductCard()
        {
            driver.FindElement(By.Id("clickProduto"));
            driver.FindElement(By.Id("clickAumentarProduto"));
            driver.FindElement(By.Id("clickDiminuirProduto"));
            driver.FindElement(By.Id("btnAdicionarProduto"));
            driver.FindElement(By.Id("clickFecharProduto"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }

    }

}

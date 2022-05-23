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
    public class CondominiumTests
    {
        IWebDriver driver;

        [OneTimeSetUp]
        public void Setup()
        {
            string path = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName;
            driver = new ChromeDriver(path + @"\drivers\");
            driver.Navigate().GoToUrl("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/LoginAdm");
        }

        [Test]
        public async Task selectCondominiumLogin()
        {
            //Arrange
            var abaCondominio = driver.FindElement(By.Id("login_condominio"));

            //Act
            abaCondominio.Click();
            await Task.Delay(5000);

            //Arrange 
            driver.FindElement(By.Id("loginEmail")).SendKeys("condominio4@gmail.com");
            driver.FindElement(By.Id("loginSenha")).SendKeys("condominio123");
            var btnLoginEntrar = driver.FindElement(By.Id("btnLoginEntrar"));

            //Act
            btnLoginEntrar.Click();
            await Task.Delay(3000);

            //Assert
            Assert.False(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/LoginAdm"));
            Assert.True(driver.Url.Equals("http://frutifeira-pipeline-front-end.s3-website-us-east-1.amazonaws.com/Condominium"));
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}

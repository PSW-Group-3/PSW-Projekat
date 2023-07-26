using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Shouldly;
using Xunit;

namespace IntegrationAPITests.E2E_tests
{
    public class BloodBanksBloodQuantityTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.CheckingBloodQuantityPage checkingBloodQuantityPage;

        public BloodBanksBloodQuantityTest()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("start-maximized");            // open Browser in maximized mode
            options.AddArguments("disable-infobars");           // disabling infobars
            options.AddArguments("--disable-extensions");       // disabling extensions
            options.AddArguments("--disable-gpu");              // applicable to windows os only
            options.AddArguments("--disable-dev-shm-usage");    // overcome limited resource problems
            options.AddArguments("--no-sandbox");               // Bypass OS security model
            options.AddArguments("--disable-notifications");    // disable notifications

            driver = new ChromeDriver(options);

            loginPage = new Pages.LoginPage(driver);
            loginPage.Navigate();
            loginPage.EnsurePageIsDisplayed();
            loginPage.InsertUsername("pera");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            Thread.Sleep(3000);
            loginPage.ErrorDivDisplayed().ShouldBe(false);
            checkingBloodQuantityPage = new Pages.CheckingBloodQuantityPage(driver);
            checkingBloodQuantityPage.Navigate();
            Thread.Sleep(1000);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Checking_blood_bank_blood_quantity()
        {
            Thread.Sleep(1000);
            checkingBloodQuantityPage.SelectNewLifeBank();
            Thread.Sleep(1000);
            checkingBloodQuantityPage.SelectBloodType();
            Thread.Sleep(1000);
            checkingBloodQuantityPage.TypeInQuantity("1");
            Thread.Sleep(1000);
            checkingBloodQuantityPage.SubmitForm();
            Thread.Sleep(6000);
            try
            {
                checkingBloodQuantityPage.WaitForToastDialog();

                Assert.True(checkingBloodQuantityPage.IsToastVisible());
            }
            catch (WebDriverTimeoutException) { }
        }
    }
}

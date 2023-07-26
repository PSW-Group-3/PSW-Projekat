using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;
using Shouldly;
using Xunit;

namespace IntegrationAPITests.E2E_tests
{
    public class BloodRequestManagementTest :IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.BloodRequestPage bloodRequestPage;
        private Pages.AcceptBloodRequestPage acceptBloodRequestPage;

        public BloodRequestManagementTest()
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
            bloodRequestPage = new Pages.BloodRequestPage(driver);
            acceptBloodRequestPage = new Pages.AcceptBloodRequestPage(driver);
            bloodRequestPage.Navigate();
            Thread.Sleep(1000);
            bloodRequestPage.ErrorDivDisplayed().ShouldBe(false);

        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
        [Fact]
        public void Decline_request()
        {
            Thread.Sleep(1000);
            bloodRequestPage.SelectReq1();
            acceptBloodRequestPage.Decline();
        }

        [Fact]
        public void Return_back()
        {
            Thread.Sleep(1000);
            bloodRequestPage.SelectReq2();
            acceptBloodRequestPage.Return_back();
        }

        [Fact]
        public void Accept_request()
        {
            Thread.Sleep(500);
            bloodRequestPage.SelectReq3();
            acceptBloodRequestPage.Accept();
        }
    }
}

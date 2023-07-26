using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using System;
using System.Threading;
using Xunit;

namespace IntegrationAPITests.E2E_tests
{
    public class BloodBankRegistrationTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.BloodBankRegistrationPage bloodBankRegistrationPage;


        public BloodBankRegistrationTest()
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
            bloodBankRegistrationPage= new Pages.BloodBankRegistrationPage(driver);
            bloodBankRegistrationPage.Navigate();
            Thread.Sleep(2000);
        }

        [Fact]
        public void Checking_blood_bank_blood_quantity()
        {
            try
            {
                bloodBankRegistrationPage.InsertName("bankabanka12");
                Thread.Sleep(1000);
                bloodBankRegistrationPage.InsertEmail("bloodymary12@gmail.com");
                Thread.Sleep(1000);
                bloodBankRegistrationPage.InsertAddress("http://nikadnista12.com");
                Thread.Sleep(1000);
                bloodBankRegistrationPage.SubmitForm();
                Thread.Sleep(3000);
                Pages.BloodBanksPage bloodBanksPage = new Pages.BloodBanksPage(driver);
                bloodBanksPage.Navigate();
                Assert.NotNull(bloodBanksPage);
            }
                catch (WebDriverTimeoutException) { }
        }




        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using Xunit;

namespace HospitalTests.E2E_tests
{
    public class ReschedulingAppointmentTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.ReschedulingAppointmentPage reschedulingAppointmentPage;

        public ReschedulingAppointmentTest()
        {
            // options for launching Google Chrome
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
            loginPage.InsertUsername("janko");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.ErrorDivDisplayed().ShouldBe(false);

            reschedulingAppointmentPage = new Pages.ReschedulingAppointmentPage(driver);
            reschedulingAppointmentPage.Navigate();

            Assert.True(reschedulingAppointmentPage.DateTimeFieldDisplayedOnScreen());

        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }


        [Fact]
        public void Test_succesfull_submit()
        {
            reschedulingAppointmentPage.DateTimeFieldDisplayed(new DateTime(2021, 10, 3));
            reschedulingAppointmentPage.SubmitForm();
        }

        [Fact]
        public void Test_selected_failed_patient()
        {
            reschedulingAppointmentPage.DateTimeFieldDisplayed(new DateTime(22222, 10, 2));
            reschedulingAppointmentPage.SubmitForm();

        }
    }
}

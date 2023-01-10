using System;
using System.Threading;
using Microsoft.Extensions.Options;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using Xunit;

namespace HospitalTests.E2E_tests
{
    public class SchedulingAppointmentTest : IDisposable
    {
        private int appointmentsCount = 0;

        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.AppoinmtentsPage appoinmtentsPage;
        private Pages.SchedulingAppointmentPage schedulingAppointmentPage;
        private Pages.ReschedulingAppointmentPage reschedulingAppointmentPage;


        public SchedulingAppointmentTest()
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
            loginPage.InsertUsername("marko");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.ErrorDivDisplayed().ShouldBe(false);

            schedulingAppointmentPage = new Pages.SchedulingAppointmentPage(driver);
            schedulingAppointmentPage.Navigate();
            schedulingAppointmentPage.EnsurePageIsDisplayed();
            
        }
        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Test_succesfull_submit()
        {
            DateTime dateTime = new DateTime(2022, 12, 12);
            schedulingAppointmentPage.PatientSelectButtonDisplayed();
            schedulingAppointmentPage.DateTimeFieldDisplayed(dateTime);
            schedulingAppointmentPage.SubmitForm();

            Assert.True(schedulingAppointmentPage.PatientSelectButtonDisplayedOnScreen());
            Assert.True(schedulingAppointmentPage.DateTimeFieldDisplayedOnScreen());
            Assert.True(schedulingAppointmentPage.SubmitButtonElementDisplayed());

            Assert.Equal("Mikica Mikicovic", schedulingAppointmentPage.GetSelectedPatient());
            Assert.Equal(dateTime, schedulingAppointmentPage.GetDateTime(dateTime));

            Assert.Equal(Pages.SchedulingAppointmentPage.URI, driver.Url);
        }
    }
}


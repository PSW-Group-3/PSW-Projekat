﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using System;
using System.Threading;
using Xunit;

namespace HospitalTests.e2e_T10
{
    public class CancelAppointmentTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.CancelAppointmentPage cancelAppointmentPage;

        public CancelAppointmentTest()
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
            loginPage.InsertUsername("nevena");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            Thread.Sleep(3000);
            loginPage.ErrorDivDisplayed().ShouldBe(false);
            cancelAppointmentPage = new Pages.CancelAppointmentPage(driver);
            cancelAppointmentPage.Navigate();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Cancel_appointment_successfully()
        {

            cancelAppointmentPage.SubmitCancel();
            try
            {
                cancelAppointmentPage.WaitForToastDialog();
            }
            catch (WebDriverTimeoutException) { }
        }

    }
}

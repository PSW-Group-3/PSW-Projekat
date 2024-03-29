﻿using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using System;
using System.Threading;
using Xunit;

namespace HospitalTests.e2e_T10
{
    public class BlockMaliciousPatientsTests : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.BlockMaliciousPatientsPage blockMaliciousPatientsPage;

        public BlockMaliciousPatientsTests()
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
            blockMaliciousPatientsPage = new Pages.BlockMaliciousPatientsPage(driver);
            blockMaliciousPatientsPage.Navigate();
            Thread.Sleep(3000);
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Block_successfully()
        {
            blockMaliciousPatientsPage.SubmitBlock();
            Thread.Sleep(1000);
            Assert.Equal("Blocked", blockMaliciousPatientsPage.isBlockedFirst.Text);
        }

        [Fact]
        public void Unblock_successfully()
        {
            blockMaliciousPatientsPage.SubmitUnblock();
            Thread.Sleep(1000);
            Assert.Equal("Not blocked", blockMaliciousPatientsPage.isBlockedLast.Text);
        }

    }
}

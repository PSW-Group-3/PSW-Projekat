using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Shouldly;
using System;
using Xunit;

namespace HospitalTests.E2E_tests
{
    public class DoctorsCouncilTest : IDisposable
    {
        private readonly IWebDriver driver;
        private Pages.LoginPage loginPage;
        private Pages.DoctorsCouncilPage reportSettingsPage;


        public DoctorsCouncilTest()
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
            loginPage.InsertUsername("marko");
            loginPage.InsertPassword("123");
            loginPage.SubmitForm();
            loginPage.ErrorDivDisplayed().ShouldBe(false);
            reportSettingsPage = new Pages.DoctorsCouncilPage(driver);
            reportSettingsPage.Navigate();
        }

        public void Dispose()
        {
            driver.Quit();
            driver.Dispose();
        }

        [Fact]
        public void Not_all_fields_are_selected_failiure()
        {
            reportSettingsPage.InsertTopic("Tema konzilijuma");
            reportSettingsPage.ClickShowSpecializationsButton();
            var addButton = reportSettingsPage.GetBut();
            var title = reportSettingsPage.GetTitle(); 
            //reportSettingsPage.InsertDoctorSelectButton();
            reportSettingsPage.InsertStartDateField(new DateTime(2022, 12, 12));
            reportSettingsPage.InsertEndDateField(new DateTime(2022, 12, 15));
            reportSettingsPage.InsertDurationFild(20);
            
            reportSettingsPage.SubmitForm();
            reportSettingsPage.WaitForToastDialog();
            Assert.Equal(title.Text, "Schedule a council");
            Assert.Equal(addButton.Text, "Submit");
            Assert.Equal(driver.Url, Pages.DoctorsCouncilPage.URI);      // check if same url - page not submitted

        }

    }
}

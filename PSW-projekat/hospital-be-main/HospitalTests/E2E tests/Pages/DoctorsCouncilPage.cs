using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using SeleniumExtras.WaitHelpers;

namespace HospitalTests.E2E_tests.Pages
{
    public class DoctorsCouncilPage
    {

        private readonly IWebDriver driver;

        public const string URI = "http://localhost:4200/council/add";

     
        private IWebElement TopicField => driver.FindElement(By.Id("topic"));

        private IWebElement StartDateField => driver.FindElement(By.Id("start"));
        private IWebElement EndDateField => driver.FindElement(By.Id("end"));

        private IWebElement DurationField => driver.FindElement(By.Id("duration"));
        private IWebElement ShowDoctorsButton => driver.FindElement(By.Id("showDoctors")); 
        private IWebElement ShowSpecializationsButton => driver.FindElement(By.Id("showSpecializations"));

        private IWebElement SpecializationSelectButton => driver.FindElement(By.Id("specializationSelect"));
        private IWebElement DoctorSelectButton => driver.FindElement(By.Id("doctorSelect"));
        private IWebElement SubmitButton => driver.FindElement(By.Id("submit"));
        private IWebElement toast => driver.FindElement(By.CssSelector(".toast-message"));
        private IWebElement Title => driver.FindElement(By.Id("title"));

        public IWebElement GetTitle()
        {
            return Title;
        }

        public IWebElement GetBut() {
            return SubmitButton;
        }
        public DoctorsCouncilPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        public void InsertDoctorSelectButton()
        {
            ShowDoctorsButton.Click();

        }

        public void InsertSpecializationSelectButton()
        {
            SpecializationSelectButton.Click();
        
            driver.FindElement(By.Id("general")).Click();
            
            driver.FindElement(By.Id("neurologist")).Click();
           
           // ClickShowSpecializationsButton();

            // DoctorSelectButton.getFirstSelectedOptions();
        }


        public void InsertDurationFild(int number)
        {
            DurationField.SendKeys(number.ToString());
        }

        public void ClickShowSpecializationsButton()
        {
            ShowSpecializationsButton.Click();
        }
        public void ClickShowDoctorsButton()
        {
            ShowDoctorsButton.Click();
        }
        public void InsertEndDateField(DateTime date)
        {
            EndDateField.SendKeys(date.ToString());
        }
        public void InsertStartDateField(DateTime date)
        {
            StartDateField.SendKeys(date.ToString());
        }

        public void InsertTopic(string name)
        {
            TopicField.SendKeys(name);
        }


        public bool DoctorSelectButtonDisplayed()
        {
            return DoctorSelectButton.Displayed;
        }
        public bool SubmitButtonDisplayed()
        {
            return SubmitButton.Displayed;
        }

        public bool SpecializationSelectButtonDisplayed()
        {
            return SpecializationSelectButton.Displayed;
        }

        public bool ShowSpecializationsButtonDisplayed()
        {
            return ShowSpecializationsButton.Displayed;
        }
        public bool ShowDoctorsButtonDisplayed()
        {
            return ShowDoctorsButton.Displayed;
        }

        public bool DurationFieldDisplayed()
        {
            return DurationField.Displayed;
        }


        public bool TopicFieldDisplayed() 
        {
            return TopicField.Displayed;
        }

        public bool StartDateFieldDisplayed()
        {
            return StartDateField.Displayed;
        }

        public bool EndDateFieldDisplayed()
        {
            return EndDateField.Displayed;
        }
        public void SubmitForm()
        {
            SubmitButton.Click();
        }

        public void WaitForToastDialog()
        {
            var wait = new WebDriverWait(driver, new TimeSpan(0, 0, 10));
            wait.Until(condition: ExpectedConditions.PresenceOfAllElementsLocatedBy(By.CssSelector(".toast-message")));
        }
        public bool IsToastVisible()
        {
            return toast.Displayed;
        }
        public void Navigate() => driver.Navigate().GoToUrl(URI);
    }
}

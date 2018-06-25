using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;

namespace DemoWebApp.Tests
{
    public class LoanApplicationPage
    {
        private readonly IWebDriver _driver;
        private const string PageUri = @"http://localhost:40077/Home/StartLoanApplication";

        public LoanApplicationPage(IWebDriver driver)
        {
            _driver = driver;
        }

        public static LoanApplicationPage NavigateTo(IWebDriver driver)
        {
            driver.Navigate().GoToUrl(PageUri);

            return new LoanApplicationPage(driver);
        }

        public string FirstName
        {
            set
            {
                _driver.FindElement(By.Id("FirstName")).SendKeys(value);
            }
        }

        public string SecondName
        {
            set
            {
                _driver.FindElement(By.Id("LastName")).SendKeys(value);
            }
        }

        public string ErrorText =>
            _driver.FindElement(By.CssSelector("div.validation-summary-errors ul li")).Text;

        public void SelectExistingLoan()
        {
            _driver.FindElement(By.Id("Loan")).Click();
        }

        public void AcceptTermsAndConditions()
        {
            _driver.FindElement(By.Name("TermsAcceptance")).Click();
        }

        public ApplicationConfirmationPage SubmitApplication()
        {
            _driver.FindElement(By.CssSelector(".btn.btn-primary")).Click();

            return new ApplicationConfirmationPage(_driver);
        }
    }
}

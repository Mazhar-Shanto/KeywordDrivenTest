using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using SAFV.Drivers;
using WebDriverManager.DriverConfigs.Impl;
using WebDriverManager;

namespace KeywordDrivenTest.Drivers
{
    public class DriverSetup
    {
        public IWebDriver _driver;

        [OneTimeSetUp]
        //[SetUp]
        public void InitDriver()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArgument("disable-notifications");  // Disable notification
            //options.AddArgument("--headless");  // Run in headless mode
            //options.AddArgument("--disable-gpu");  // Disable GPU acceleration
            options.AddArgument("--disable-extensions");  // Disable extensions
            options.AddArgument("--disable-popup-blocking");  // Disable popups
            options.AddArgument("--incognito");  // Use incognito mode
            options.AddArgument("--no-sandbox");  // Disable sandbox (Chrome)
            options.AddUserProfilePreference("autofill.profile_enabled", false);

            new DriverManager().SetUpDriver(new ChromeConfig());
            _driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
            _driver.Manage().Window.Maximize();
            // Configure implicit wait
            _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(.3);
        }

        [TearDown]
        public void TearDown()
        {
            if (_driver != null)
            {
                if (TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Failed")
                {
                    //Reporting.SetStepStatusFail(TestContext.Error.ToString(), _driver);
                    //Reporting.SetStepStatusFail(TestContext.CurrentContext.Result.Outcome.ToString(), _driver);
                    Reporting.SetTestStatusFail();
                }
                else if (TestContext.CurrentContext.Result.Outcome.Status.ToString() == "Passed")
                {
                    Reporting.SetTestStatusPass();
                }
                Reporting.Close();
            }
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            // Dispose of the driver after each test
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
                _driver = null;
            }
        }
    }
}

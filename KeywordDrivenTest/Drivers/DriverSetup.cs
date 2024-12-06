using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace KeywordDrivenTest.Drivers
{
    public class DriverSetup
    {
        private static IWebDriver driver;

        public static IWebDriver InitDriver()
        {
            if (driver == null)
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

                // new DriverManager().SetUpDriver(new ChromeConfig());
                driver = new ChromeDriver(ChromeDriverService.CreateDefaultService(), options, TimeSpan.FromMinutes(3));
                driver.Manage().Window.Maximize();
                // Configure implicit wait
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(.3);
            }

            return driver;
        }

        /*public static void QuitDriver()
        {
            if (driver != null)
            {
                driver.Quit();
                driver = null;
            }
        }*/
    }
}

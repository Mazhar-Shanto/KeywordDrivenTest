/*using KeywordDrivenTest.Keywords;
using KeywordDrivenTest.Utils;
using OpenQA.Selenium;
using KeywordDrivenTest.Drivers;

namespace KeywordDrivenTest.TestCases
{
    public class TestExecutor
    {
        private IWebDriver _driver;
        private KeywordExecutor _keywordLibrary;

        *//*public TestExecutor()
        {
            _driver = DriverManager.GetDriver(); // Use the centralized driver
            _keywordLibrary = new KeywordLibrary(_driver);
        }*//*

        public void ExecuteTest(string filePath)
        {
            var testSteps = ExcelUtils.ReadExcel(filePath);

            foreach (var step in testSteps)
            {
                string action = step["Action"];
                string locatorType = step["Locator Type"];
                string locatorValue = step["Locator Value"];
                string data = step["Data"];

                Console.WriteLine(step["Action"]);
                Console.WriteLine(step["Locator Type"]);
                Console.WriteLine(step["Locator Value"]);
                Console.WriteLine(step["Data"]);

                switch (action.ToLower())
                {
                    case "openbrowser":
                        _keywordLibrary.OpenBrowser(data);
                        break;

                    case "navigateto":
                        _keywordLibrary.NavigateToUrl(data);
                        break;

                    case "entertext":
                        _keywordLibrary.EnterText(locatorType, locatorValue, data);
                        break;

                    case "click":
                        _keywordLibrary.Click(locatorType, locatorValue);
                        break;

                    case "closebrowser":
                        _keywordLibrary.CloseBrowser();
                        break;

                    default:
                        Console.WriteLine($"Unknown action: {action}");
                        break;
                }
            }
        }

        public void Cleanup()
        {
            DriverSetup.QuitDriver(); // Quit the centralized driver
        }
    }
}*/
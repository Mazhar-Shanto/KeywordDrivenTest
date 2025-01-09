using OpenQA.Selenium;

namespace KeywordDrivenTest.Keywords
{
    public class KeywordExecutor : KeywordBase
    {
        //private IWebDriver _driver;
        private Dictionary<string, (string ElementType, string LocatorType, string LocatorValue)> _locators;

        public KeywordExecutor(IWebDriver driver, Dictionary<string, (string ElementType, string LocatorType, string LocatorValue)> locators) : base(driver)
        {
            //base._driver = driver;
            _locators = locators;
        }

        public struct Result
        {
            public string status;
            public string message;
        }

        public Result Execute(string actionKeyword, string elementName, string testData)
        {
            Result result;
            IWebElement element = null;
            IList<IWebElement> elements = null;

            foreach (var item in _locators)
            {
                Console.WriteLine(item);
            }

            try
            {
                if (!string.IsNullOrEmpty(elementName) && _locators.ContainsKey(elementName))
                {
                    var (elementType, locatorType, locatorValue) = _locators[elementName];

                    if (elementType == "list")
                    {
                        // Use WaitAndFindElements for lists
                        elements = locatorType switch
                        {
                            "id" => WaitAndFindElements(By.Id(locatorValue)),
                            "css" => WaitAndFindElements(By.CssSelector(locatorValue)),
                            "xpath" => WaitAndFindElements(By.XPath(locatorValue)),
                            _ => throw new Exception($"Invalid locator type: {locatorType}")
                        };
                    }
                    else
                    {
                        // Use WaitAndFindElement for single element
                        element = locatorType switch
                        {
                            "id" => WaitAndFindElement(By.Id(locatorValue)),
                            "css" => WaitAndFindElement(By.CssSelector(locatorValue)),
                            "xpath" => WaitAndFindElement(By.XPath(locatorValue)),
                            _ => throw new Exception($"Invalid locator type: {locatorType}")
                        };
                    }
                }

                switch (actionKeyword)
                {
                    case "NavigateToURL":
                        _driver.Navigate().GoToUrl(testData);
                        break;
                    case "VerifyPageTitle":
                        VerifyPageTitle(testData);
                        break;
                    case "InputText":
                        SendKeys(element, testData);
                        break;
                    case "Click":
                        Click(element);
                        break;
                    case "SelectOption":
                        SelectOption(elements, testData);
                        break;
                    case "Toggle":
                        Toggle(element, testData);
                        break;
                    case "RadioOption":
                        Radio(elements, testData);
                        break;
                    case "VerifyText":
                        if (element == null || !element.Text.Contains(testData))
                            throw new Exception($"Verification failed for text: {testData}");
                        break;
                    default:
                        throw new Exception($"Invalid keyword: {actionKeyword}");
                }

                result.status = "Pass";
                result.message = "";

                return result; // Return "pass" if no exception occurs
            }
            catch (Exception ex)
            {
                // Catch any exception and return the message
                result.status = "Fail";
                result.message = ex.Message;
                return result;
            }
        }
    }
}
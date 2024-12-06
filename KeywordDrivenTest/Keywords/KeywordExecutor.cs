using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace KeywordDrivenTest.Keywords
{
    public class KeywordExecutor
    {
        private IWebDriver _driver;
        private Dictionary<string, (string LocatorType, string LocatorValue)> _locators;

        public KeywordExecutor(IWebDriver driver, Dictionary<string, (string LocatorType, string LocatorValue)> locators)
        {
            _driver = driver;
            _locators = locators;
        }

        public void Execute(string keyword, string locatorKey, string testData)
        {
            IWebElement element = null;

            if (!string.IsNullOrEmpty(locatorKey) && _locators.ContainsKey(locatorKey))
            {
                var (locatorType, locatorValue) = _locators[locatorKey];

                element = locatorType switch
                {
                    "id" => _driver.FindElement(By.Id(locatorValue)),
                    "css" => _driver.FindElement(By.CssSelector(locatorValue)),
                    "xpath" => _driver.FindElement(By.XPath(locatorValue)),
                    _ => throw new Exception($"Invalid locator type: {locatorType}")
                };
            }

            switch (keyword)
            {
                case "NavigateToURL":
                    _driver.Navigate().GoToUrl(testData);
                    break;
                case "InputText":
                    element?.SendKeys(testData);
                    break;
                case "Click":
                    element?.Click();
                    break;
                case "VerifyText":
                    if (element == null || !element.Text.Contains(testData))
                        throw new Exception($"Verification failed for text: {testData}");
                    break;
                default:
                    throw new Exception($"Invalid keyword: {keyword}");
            }
        }
    }
}

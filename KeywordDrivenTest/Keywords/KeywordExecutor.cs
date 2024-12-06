using OpenQA.Selenium;
using System;
using System.Collections.Generic;

namespace KeywordDrivenTest.Keywords
{
    public class KeywordExecutor
    {
        private IWebDriver _driver;
        private Dictionary<string, string> _locators;

        public KeywordExecutor(IWebDriver driver, Dictionary<string, string> locators)
        {
            _driver = driver;
            _locators = locators;
        }

        public void Execute(string keyword, string locatorKey, string testData)
        {
            IWebElement element = null;

            if (!string.IsNullOrEmpty(locatorKey) && _locators.ContainsKey(locatorKey))
            {
                var locatorParts = _locators[locatorKey].Split('=');
                var byType = locatorParts[0];
                var byValue = locatorParts[1];

                element = byType switch
                {
                    "id" => _driver.FindElement(By.Id(byValue)),
                    "css" => _driver.FindElement(By.CssSelector(byValue)),
                    "xpath" => _driver.FindElement(By.XPath(byValue)),
                    _ => throw new Exception($"Invalid locator type: {byType}")
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

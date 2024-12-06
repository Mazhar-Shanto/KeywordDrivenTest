using KeywordDrivenTest.Drivers;
using KeywordDrivenTest.Keywords;
using KeywordDrivenTest.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;

namespace KeywordDrivenTest.Tests
{
    public class KeywordDrivenTest
    {
        private static IWebDriver _driver;
        private KeywordExecutor _executor;

        [SetUp]
        public void Setup()
        {
            _driver = DriverSetup.InitDriver();
            var _locators = LocatorReader.ReadLocators("../../../TestData/Locators.xlsx");
            _executor = new KeywordExecutor(_driver, _locators);
        }

        [Test]
        public void RunKeywordDrivenTest()
        {
            var testCases = TestCaseReader.ReadTestCases("../../../TestData/TestCases.xlsx");

            foreach (var testStep in testCases)
            {
                string keyword = testStep["Keyword"];
                string locatorKey = testStep["LocatorKey"];
                string testData = testStep["TestData"];

                _executor.Execute(keyword, locatorKey, testData);
            }
        }

        [TearDown]
        public void TearDown()
        {
            _driver.Quit();
        }
    }
}

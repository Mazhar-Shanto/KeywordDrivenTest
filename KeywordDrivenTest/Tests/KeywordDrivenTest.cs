using KeywordDrivenTest.Drivers;
using KeywordDrivenTest.Keywords;
using KeywordDrivenTest.Utils;
using NUnit.Framework;
using OpenQA.Selenium;
using SAFV.Drivers;
using System.Collections.Generic;

namespace KeywordDrivenTest.Tests
{
    public class KeywordDrivenTest : DriverSetup
    {
        //private static IWebDriver _driver;
        private KeywordExecutor _executor;

        [Test]
        public void RunKeywordDrivenTest()
        {
            Reporting.CreateTest("RunKeywordDrivenTest");
            var _locators = LocatorReader.ReadLocators("../../../TestData/Locators.xlsx");
            _executor = new KeywordExecutor(_driver, _locators);

            var testCases = TestCaseReader.ReadTestCases("../../../TestData/TestCases.xlsx");

            foreach (var testStep in testCases)
            {
                string actionKeyword = testStep["ActionKeyword"];
                string elementName = testStep["ElementName"];
                string testData = testStep["TestData"];

                _executor.Execute(actionKeyword, elementName, testData);
            }
        }
    }
}

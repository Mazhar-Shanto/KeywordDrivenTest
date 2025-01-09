using KeywordDrivenTest.Drivers;
using KeywordDrivenTest.Keywords;
using KeywordDrivenTest.Utils;
using SAFV.Drivers;

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

            var projectRoot = Helper.GetProjectRoot();
            var locatorsFilePath = Path.Combine(projectRoot, "TestData/Locators.xlsx");
            var testCaseFilePath = Path.Combine(projectRoot, "TestData/TestCases.xlsx");
            var excelReportFilePath = Path.Combine(projectRoot, "Report/ExcelReport.xlsx");

            var _locators = LocatorReader.ReadLocators(locatorsFilePath);
            _executor = new KeywordExecutor(_driver, _locators);

            var testCases = TestCaseReader.ReadTestCases(testCaseFilePath);
            var testResult = new List<Dictionary<string, string>>();

            foreach (var testStep in testCases)
            {
                string actionKeyword = testStep["ActionKeyword"];
                string elementName = testStep["ElementName"];
                string testData = testStep["TestData"];
                //string result = "";

                var step = new Dictionary<string, string>();
                var result = _executor.Execute(actionKeyword, elementName, testData);
                testStep["Status"] = result.status;
                testStep["Message"] = result.message;
                testResult.Add(testStep);
                Console.WriteLine(".....");
            }
            WriteExcelReport.WriteTestResults(excelReportFilePath, testResult);
            Console.WriteLine("end");
        }
    }
}

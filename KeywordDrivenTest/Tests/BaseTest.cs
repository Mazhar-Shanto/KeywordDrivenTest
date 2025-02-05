using KeywordDrivenTest.KeywordRunner;
using KeywordDrivenTest.Utils;
using SAFV.Drivers;

namespace KeywordDrivenTest.Tests
{
    public class BaseTest : DriverSetup
    {
        //private static IWebDriver _driver;
        private KeywordExecutor _executor;

        public void RunKeywordDrivenTest(string locatorsFilePath, string testCaseFilePath, string excelReportFilePath)
        {

            var projectRoot = Helper.GetProjectRoot();

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

        public void Login()
        {

            var projectRoot = Helper.GetProjectRoot();
            var locatorsFilePath = Path.Combine(projectRoot, "_TestData/Locators_Login.xlsx");
            var testCaseFilePath = Path.Combine(projectRoot, "_TestData/TestCase_1_Login.xlsx");
            var excelReportFilePath = Path.Combine(projectRoot, "_Report/ExcelReport_Login.xlsx");

            RunKeywordDrivenTest(locatorsFilePath, testCaseFilePath, excelReportFilePath);
        }

        public bool VerifyPageTitle(string pageTitle)
        {
            string getPageTitle = _driver.Title;

            if (getPageTitle != null && getPageTitle == pageTitle)
                return true;
            else 
                return false;
        }
    }
}

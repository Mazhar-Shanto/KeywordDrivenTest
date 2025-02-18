using KeywordDrivenTest.Source.Pages;
using KeywordDrivenTest.Utils;
using SAFV.Drivers;

namespace KeywordDrivenTest.Tests
{
    public class DetectiveModeTest : BaseTest
    {
        [Test]
        public void CreateCaseFromIncidentsTest()
        {
            Reporting.CreateTest("CreateCaseFromIncidentsTest");

            var projectRoot = Helper.GetProjectRoot();
            var locatorsFilePath = Path.Combine(projectRoot, "_TestData/CreateCaseFromIncidents/CreateCaseFromIncidents_Locators.xlsx");
            var testCaseFilePath = Path.Combine(projectRoot, "_TestData/CreateCaseFromIncidents/CreateCaseFromIncidents_TestCase.xlsx");
            var excelReportFilePath = Path.Combine(projectRoot, "_Report/CreateCaseFromIncidents/CreateCaseFromIncidents_ExcelReport.xlsx");

            NavMenu navMenu = new NavMenu(_driver);

            if(!VerifyPageTitle("Home Page - SAFV_Site"))
            {
                Login();
            }

            if(!VerifyPageTitle("Create Case - SAFV_Site"))
            {
                navMenu.GoToCaseCreateFromIncidents();
            }
            
            RunKeywordDrivenTest(locatorsFilePath, testCaseFilePath, excelReportFilePath);
        }
    }
}

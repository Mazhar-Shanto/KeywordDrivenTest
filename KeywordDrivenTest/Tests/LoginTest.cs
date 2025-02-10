using KeywordDrivenTest.Utils;
using OfficeOpenXml;
using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using SAFV.Drivers;
using System.Text.RegularExpressions;

namespace KeywordDrivenTest.Tests
{
    public class LoginTest : BaseTest
    {
        [Test]
        public void Login()
        {
            Reporting.CreateTest("Login");

            var projectRoot = Helper.GetProjectRoot();
            var locatorsFilePath = Path.Combine(projectRoot, "_TestData/Locators_Login.xlsx");
            var testCaseFilePath = Path.Combine(projectRoot, "_TestData/TestCase_1_Login.xlsx");
            var excelReportFilePath = Path.Combine(projectRoot, "_Report/ExcelReport_Login.xlsx");

            RunKeywordDrivenTest(locatorsFilePath, testCaseFilePath, excelReportFilePath);
        }


        [Test]
        public void A()
        {
            string input = "public static IWebElement ReportDate => WaitAndFindElement(By.Id(\"IncidentDate\"));\r\n        public static IWebElement DetectiveCaseNumber => WaitAndFindElement(By.Id(\"AgencyIdentifier\"));\r\n        public static IWebElement IncidentType => WaitAndFindElement(By.Id(\"IncidentTypeId\"));\r\n        public static IList<IWebElement> LstIncidentType => WaitAndFindElements(By.XPath(\"//*[@id=\\\"IncidentTypeId\\\"]/option\"));\r\n        public static IWebElement ConfidentialMode => WaitAndFindElement(By.XPath(\"//*[@id=\\\"incident-creation-form\\\"]/div[5]/span[1]\"));\r\n        public static IWebElement CaseType => WaitAndFindElement(By.XPath(\"//*[@id=\\\"incident-creation-form\\\"]/div[6]/span[1]\"));\r\n        public static IList<IWebElement> LstCaseType => WaitAndFindElements(By.XPath(\"//*[@id=\\\"CaseTypeId_listbox\\\"]/li\"));\r\n        public static IWebElement MainCase => WaitAndFindElement(By.XPath(\"//*[@id=\\\"masterIncidentField\\\"]/span[1]\"));\r\n        public static IWebElement SearchMainCase => WaitAndFindElement(By.XPath(\"//*[@id=\\\"MasterIncidentId-list\\\"]/span/input\"));\r\n        public static IList<IWebElement> LstMainCase => WaitAndFindElements(By.XPath(\"//*[@id=\\\"MasterIncidentId_listbox\\\"]/li\"));\r\n\r\n        public static IWebElement Incidents => WaitAndFindElement(By.XPath(\"//*[@id=\\\"incident-creation-form\\\"]/div[9]/div/div[1]/span\"));\r\n        public static IWebElement SearchIncidents => WaitAndFindElement(By.XPath(\"//*[@id=\\\"OriginIncidentId-list\\\"]/span/input\"));\r\n        public static IList<IWebElement> LstIncidents => WaitAndFindElements(By.XPath(\"//*[@id=\\\"OriginIncidentId_listbox\\\"]/li\"));\r\n        public static IWebElement AddToList => WaitAndFindElement(By.XPath(\"//*[@id=\\\"incident-creation-form\\\"]/div[9]/div/div[2]/button\"));\r\n\r\n        public static IWebElement CreateButton => WaitAndFindElement(By.XPath(\"//*[@id=\\\"incident-creation-form\\\"]/div[8]/input\"));\r\n        public static IWebElement CreateCaseFromIncidents => WaitAndFindElement(By.XPath(\"//*[@id=\\\"incident-creation-form\\\"]/div[11]/button\"));";

            List<(string LocatorName, string ElementType, string LocatorType, string Locator)> locators = new();

            foreach (Match match in Regex.Matches(input, @"public static (?:IList<IWebElement>|IWebElement) (?<name>\w+) => WaitAndFind(?:Element|Elements)\(By\.(?<type>Id|XPath)\((?<locator>.*?)\)\)"))
            {
                string elementType = match.Groups[0].Value.Contains("IList<IWebElement>") ? "list" : ""; // Correctly classify lists
                locators.Add((match.Groups["name"].Value, elementType, match.Groups["type"].Value.ToLower(), match.Groups["locator"].Value.Trim('"')));
            }

            string filePath = "D:/Locators.xlsx";
            FileInfo file = new FileInfo(filePath);
            if (file.Exists) file.Delete();

            using (ExcelPackage package = new ExcelPackage(file))
            {
                ExcelWorksheet sheet = package.Workbook.Worksheets.Add("Locators");
                sheet.Cells[1, 1].Value = "LocatorName";
                sheet.Cells[1, 2].Value = "ElementType";
                sheet.Cells[1, 3].Value = "LocatorType";
                sheet.Cells[1, 4].Value = "Locator";

                for (int i = 0; i < locators.Count; i++)
                {
                    sheet.Cells[i + 2, 1].Value = locators[i].LocatorName;
                    sheet.Cells[i + 2, 2].Value = locators[i].ElementType;
                    sheet.Cells[i + 2, 3].Value = locators[i].LocatorType;
                    sheet.Cells[i + 2, 4].Value = locators[i].Locator.Trim('"'); // Trim double quotes
                }

                package.Save();
            }
            Console.WriteLine("Excel file created: " + filePath);
        }
    }
}

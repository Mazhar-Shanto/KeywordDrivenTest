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
            var locatorsFilePath = Path.Combine(projectRoot, "_TestData/Login_Locators.xlsx");
            var testCaseFilePath = Path.Combine(projectRoot, "_TestData/Login_TestCase.xlsx");
            var excelReportFilePath = Path.Combine(projectRoot, "_Report/Login_ExcelReport.xlsx");

            RunKeywordDrivenTest(locatorsFilePath, testCaseFilePath, excelReportFilePath);
        }


        [Test]
        public void A()
        {
            string input = "public static IWebElement AddSuspectEvidence => WaitAndFindElement(By.XPath(\"//*[@id=\\\"master-container\\\"]/div[1]/div/a\"));\r\n        public static IWebElement EvidenceType => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[1]/div/span[1]\"));\r\n        public static IList<IWebElement> LstEvidenceType => WaitAndFindElements(By.XPath(\"//*[@id=\\\"EvidenceTypeId_listbox\\\"]/li\"));\r\n        public static IWebElement EvidenceDisposition => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[2]/div/span[1]\"));\r\n        public static IList<IWebElement> LstEvidenceDisposition => WaitAndFindElements(By.XPath(\"//*[@id=\\\"EvidenceDispositionId_listbox\\\"]/li\"));\r\n        public static IWebElement CollectedFromPerson => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[3]/div[1]/div/span[1]\"));\r\n        public static IWebElement Person => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[3]/div[2]/div/span[1]\"));\r\n        public static IList<IWebElement> LstPerson => WaitAndFindElements(By.XPath(\"//*[@id=\\\"IncidentPersonId_listbox\\\"]/li\"));\r\n        public static IWebElement PersonNotListed => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[3]/div[3]/div/span[1]\"));\r\n        public static IWebElement PersonName => WaitAndFindElement(By.Id(\"NameofEntity\"));\r\n        public static IWebElement WhereFound => WaitAndFindElement(By.Id(\"Location\"));\r\n        public static IWebElement EvidenceDescription => WaitAndFindElement(By.Id(\"PhysicalDesc\"));\r\n        public static IWebElement WasSeized => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[6]/div/span[1]\"));\r\n        public static IWebElement TypeOfWeapon => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[7]/div[1]/div/span[1]\"));\r\n        public static IList<IWebElement> LstTypeOfWeapon => WaitAndFindElements(By.XPath(\"//*[@id=\\\"IncidentWeaponTypeId_listbox\\\"]/li\"));\r\n        public static IWebElement EvidenceSerialNumber => WaitAndFindElement(By.Id(\"SerialNumber\"));\r\n        public static IWebElement WeaponManufacturer => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[7]/div[3]/div[1]/div/span[1]\"));\r\n        public static IList<IWebElement> LstWeaponManufacturer => WaitAndFindElements(By.XPath(\"//*[@id=\\\"WeaponManufacturerId_listbox\\\"]/li\"));\r\n        public static IWebElement WeaponCaliber => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[7]/div[3]/div[3]/div/span[1]\"));\r\n        public static IList<IWebElement> LstWeaponCaliber => WaitAndFindElements(By.XPath(\"//*[@id=\\\"WeaponCaliberId_listbox\\\"]/li\"));\r\n        public static IWebElement EvidenceCollectedBy => WaitAndFindElement(By.Id(\"CollectedBy\"));\r\n        public static IWebElement SaveEvidence => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[9]/div/div/button\"));\r\n        public static IWebElement CancelEvidence => WaitAndFindElement(By.XPath(\"//*[@id=\\\"person-evidance-form\\\"]/div/div[9]/div/div/a\"));";

            List<(string LocatorName, string ElementType, string LocatorType, string Locator)> locators = new();

            foreach (Match match in Regex.Matches(input, @"public static (?:IList<IWebElement>|IWebElement) (?<name>\w+) => WaitAndFind(?:Element|Elements)\(By\.(?<type>Id|XPath)\((?<locator>.*?)\)\)"))
            {
                string elementType = match.Groups[0].Value.Contains("IList<IWebElement>") ? "list" : ""; // Correctly classify lists
                locators.Add((match.Groups["name"].Value, elementType, match.Groups["type"].Value.ToLower(), match.Groups["locator"].Value.Trim('"')));
            }

            string filePath = "D:/Locators/CreateIncident/People/Suspect/Locators.xlsx";
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

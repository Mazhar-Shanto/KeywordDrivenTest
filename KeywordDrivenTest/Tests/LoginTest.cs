using KeywordDrivenTest.Utils;
using SAFV.Drivers;

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
        public void a()
        {
            string input = "public static IWebElement SubmitForReview => WaitAndFindElement(By.Id(\"submitforReview\"));\r\n        public static IWebElement ConfirmSubmitForReview => WaitAndFindElement(By.XPath(\"//*[@id=\\\"review-dialog\\\"]/div/div/div[3]/button[1]\"));\r\n        public static IWebElement CancelSubmitForReview => WaitAndFindElement(By.XPath(\"//*[@id=\\\"review-dialog\\\"]/div/div/div[3]/button[2]\"));\r\n        public static IWebElement ReportGenerateYes => WaitAndFindElement(By.XPath(\"/html/body/div[6]/div[3]/button[1]\"));\r\n        public static IWebElement ReportGenerateNo => WaitAndFindElement(By.XPath(\"/html/body/div[6]/div[3]/button[2]\"));\r\n        public static IWebElement LockIncident => WaitAndFindElement(By.XPath(\"/html/body/div[1]/div[4]/div[2]/div/div[2]/div/div/div[3]/div/div/div[1]/div/div[2]/div/div[1]/button[1]\"));\r\n        public static IWebElement UnLockIncident => WaitAndFindElement(By.XPath(\"/html/body/div[1]/div[4]/div[2]/div/div[2]/div/div/div[3]/div/div/div[1]/div/div[2]/div/div[1]/button\"));\r\n        public static IWebElement RejectIncident => WaitAndFindElement(By.XPath(\"/html/body/div[1]/div[4]/div[2]/div/div[2]/div/div/div[3]/div/div/div[1]/div/div[2]/div/div[1]/button[2]\"));\r\n        public static IWebElement ConfirmReject => WaitAndFindElement(By.XPath(\"//*[@id=\\\"reject-dialog\\\"]/div/div/div[3]/button[1]\"));\r\n        public static IWebElement CancelReject => WaitAndFindElement(By.XPath(\"//*[@id=\\\"reject-dialog\\\"]/div/div/div[3]/button[2]\"));\r\n        public static IWebElement ConfirmLock => WaitAndFindElement(By.XPath(\"//*[@id=\\\"lock-dialog\\\"]/div/div/div[3]/button[1]\"));\r\n        public static IWebElement CancelLock => WaitAndFindElement(By.XPath(\"//*[@id=\\\"lock-dialog\\\"]/div/div/div[3]/button[2]\"));\r\n        public static IWebElement ConfirmUnLock => WaitAndFindElement(By.XPath(\"//*[@id=\\\"unlock-dialog\\\"]/div/div/div[3]/button[1]\"));\r\n        public static IWebElement CancelUnLock => WaitAndFindElement(By.XPath(\"//*[@id=\\\"unlock-dialog\\\"]/div/div/div[3]/button[2]\"));\r\n        public static IWebElement Status => WaitAndFindElement(By.XPath(\"//*[@id=\\\"StatusHisotrGrid\\\"]/table/tbody/tr[1]/td[2]\"));\r\n\r\n        public static IList<IWebElement> LstIncidents => WaitAndFindElements(By.XPath(\"//*[@id=\\\"linked-incident-grid\\\"]/table/tbody/tr[1]/td[1]/a\"));";

            // Split the string into words, ignoring multiple spaces
            string[] lines = input
                .Split(["\r\n"], StringSplitOptions.RemoveEmptyEntries)
                .Select(incident => incident.Trim())
                .ToArray();
            int count = 0;

            // Print each word on a new line
            foreach (string line in lines)
            {
                Console.WriteLine(line);
                string[] words = line
                .Split(["\r\n"], StringSplitOptions.RemoveEmptyEntries)
                .Select(incident => incident.Trim())
                .ToArray();

                foreach

                count++;
            }
        }
    }
}

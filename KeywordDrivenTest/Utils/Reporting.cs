using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;
using KeywordDrivenTest.Utils;
using OpenQA.Selenium;
using System.Reflection;
using System.Xml.Linq;

namespace SAFV.Drivers
{
    public class Reporting
    {
        public static ExtentReports Extent { get; set; }
        public static ExtentSparkReporter Reporter { get; set; }
        public static ExtentTest Test { get; set; }

        static Reporting()
        {
            Extent = new ExtentReports();
            var projectRoot = Helper.GetProjectRoot();
            var dateTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm");
            var fileName = "TestReport_" + dateTime + ".html";
            var filePath = Path.Combine(projectRoot, "Report", fileName);
            Reporter = new ExtentSparkReporter(filePath);
            Reporter.Config.DocumentTitle = "Automation Testing Report";
            Reporter.Config.ReportName = "Regression Testing";
            Reporter.Config.Theme = AventStack.ExtentReports.Reporter.Config.Theme.Standard;
            Extent.AttachReporter(Reporter);
            Extent.AddSystemInfo("Application Under Test", "DRE");
            Extent.AddSystemInfo("Environment", "QA");
            Extent.AddSystemInfo("Machine", Environment.MachineName);
            Extent.AddSystemInfo("OS", Environment.OSVersion.VersionString);
        }

        public static void CreateTest(string testName)
        {
            Test = Extent.CreateTest(testName);
        }

        public static void SetStepStatusPass(string stepDescription, IWebDriver driver)
        {
            Test.Log(Status.Pass, $"<p style=\"color:green;\"><b>{stepDescription}</b></p>", MediaEntityBuilder.CreateScreenCaptureFromBase64String(AddTestScreenshotBase64(driver)).Build());
        }

        public static void SetStepStatusInfo(string stepDescription, IWebDriver driver)
        {
            Test.Log(Status.Info, $"<p style=\"color:blue;\"><b>{stepDescription}</b></p>", MediaEntityBuilder.CreateScreenCaptureFromBase64String(AddTestScreenshotBase64(driver)).Build());
        }

        public static void SetStepStatusWarning(string stepDescription, IWebDriver driver)
        {
            Test.Log(Status.Warning, $"<p style=\"color:gold;\"><b>{stepDescription}</b></p>", MediaEntityBuilder.CreateScreenCaptureFromBase64String(AddTestScreenshotBase64(driver)).Build());
        }

        public static void SetStepStatusFail(string stepDescription, IWebDriver driver)
        {
            Test.Log(Status.Fail, $"<p style=\"color:red;\"><b>{stepDescription}</b></p>", MediaEntityBuilder.CreateScreenCaptureFromBase64String(AddTestScreenshotBase64(driver)).Build());
        }

        public static void SetTestStatusPass()
        {
            Test.Pass("<p style=\"color:green;\"><b>Test Executed Successfully!</b></p>");
        }

        public static void SetTestStatusFail(string message = null)
        {
            var printMessage = "<p style=\"color:red;\"><b>Test FAILED!</b></p>";
            if (!string.IsNullOrEmpty(message))
            {
                printMessage += $"Message: <br>{message}<br>";
            }
            Test.Fail(printMessage);
        }

        public static void AddTestScreenshot(IWebDriver driver, string testName)
        {
            // Take screenshot
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            // Save screenshot
            var projectRoot = Helper.GetProjectRoot();
            var dateTime = DateTime.Now.ToString("dd-MM-yyyy_HH-mm-ss");
            var filePath = Path.Combine(projectRoot, "Report", testName, dateTime + ".png");
            screenshot.SaveAsFile(filePath);

            // Log screenshot
            Test.Info(MediaEntityBuilder.CreateScreenCaptureFromPath(filePath).Build());
        }

        public static string AddTestScreenshotBase64(IWebDriver driver)
        {
            var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
            return screenshot.AsBase64EncodedString;
        }

        public static void SetTestStatusSkipped()
        {
            Test.Skip("Test skipped!");
        }

        public static void Close()
        {
            Extent.Flush();
        }
    }
}

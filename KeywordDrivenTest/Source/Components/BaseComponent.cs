using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace KeywordDrivenTest.Source.Components
{
    public class BaseComponent
    {
        private static IWebDriver _driver;
        private static WebDriverWait _wait;

        public static IWebElement WaitAndFindElement(By by)
        {
            IWebElement element = null;
            int maxTry = 10;
            int trying = 11;

            // This will try 3 times to find an element
            while (maxTry > 0)
            {
                try
                {
                    //element = _wait.Until(driver => driver.FindElement(by));
                    element = _driver.FindElement(by);

                    //((IJavaScriptExecutor)_driver).ExecuteScript("arguments[0].scrollIntoView(true);", element);

                    /*Actions actions = new Actions(_driver);
                    actions.MoveToElement(element).Perform();*/
                }
                catch (Exception)
                {
                    Console.WriteLine("Element not found on try " + (trying - maxTry));
                    Thread.Sleep(1000);
                }

                //Thread.Sleep(100);

                if (element != null && element.Enabled)
                {
                    break;
                }
                else
                {
                    maxTry--;
                }
            }
            return element;
        }

        public static IList<IWebElement> WaitAndFindElements(By by)
        {
            IList<IWebElement> elements = null;
            int maxTry = 10;
            int trying = 11;

            // This will try 3 times to find an element list
            while (maxTry > 0)
            {
                try
                {
                    //elements = _wait.Until(driver => driver.FindElements(by));
                    elements = _driver.FindElements(by);
                }
                catch (Exception)
                {
                    Console.WriteLine("Element not found on try " + (trying - maxTry));
                    Thread.Sleep(1000);
                }

                //Thread.Sleep(100);

                if (elements != null)
                {
                    break;
                }
                else
                {
                    maxTry--;
                }
            }
            return elements;
        }

        public BaseComponent(IWebDriver driver)
        {
            _driver = driver;

            // Use fluent wait to find the element
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }
    }
}

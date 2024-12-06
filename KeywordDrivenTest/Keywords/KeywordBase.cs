using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;
using SAFV.Drivers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KeywordDrivenTest.Utils;
using OpenQA.Selenium.BiDi.Modules.BrowsingContext;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;

namespace KeywordDrivenTest.Keywords
{
    public class KeywordBase
    {
        public static IWebDriver _driver;
        public static WebDriverWait _wait;

        public KeywordBase(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }


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


        public void Click(IWebElement element)
        {
            int maxTry = 5;
            string label = "";

            // This will try 5 times to click an element
            while (maxTry > 0)
            {
                if (label.Length == 0)
                {
                    label = Helper.GetLabelName(element);
                }


                try
                {
                    element.Click();
                    Console.WriteLine("Try click: " + label);
                    Reporting.SetStepStatusPass($"Click <b style=\"color:blue;\">{label}</b>", _driver);
                    return; // Click was successful, exit the method
                }
                catch (ElementClickInterceptedException e)
                {
                    // Log or handle ElementClickInterceptedException
                    Console.WriteLine("ElementClickInterceptedException caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Click <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (StaleElementReferenceException e)
                {
                    // Log or handle StaleElementReferenceException
                    Console.WriteLine("StaleElementReferenceException caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Click <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (Exception e)
                {
                    // Log or handle other exceptions
                    Console.WriteLine("Exception caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Click <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }

                maxTry--;
            }

            // If we exit the loop without clicking, throw an exception
            Reporting.SetStepStatusFail($"Click <b style=\"color:blue;\">{label}</b> failed", _driver);
            throw new Exception("Failed to click the element after multiple attempts");
        }

        public void Toggle(IWebElement element, string s)
        {
            int maxTry = 5;
            string label = "";
            string previousState = element.GetAttribute("aria-checked");

            if (s.ToLower() == "yes" || s == "true" || s == "active" || s == "1")
            {
                s = "ON";
            }
            else
            {
                s = "OFF";
            }

            if (previousState.ToLower() == "true")
            {
                previousState = "ON";
            }
            else
            {
                previousState = "OFF";
            }


            if (s != previousState)
            {
                // This will try 5 times to click an element
                while (maxTry > 0)
                {
                    if (label.Length == 0)
                    {
                        label = Helper.GetLabelName(element);
                    }


                    try
                    {
                        element.Click();
                        Reporting.SetStepStatusPass($"Toggle <b style=\"color:blue;\">{label}</b> from <b>{previousState}</b> to <b>{s}</b>", _driver);
                        return; // Click was successful, exit the method
                    }
                    catch (ElementClickInterceptedException e)
                    {
                        // Log or handle ElementClickInterceptedException
                        Console.WriteLine("ElementClickInterceptedException caught: " + e.Message);
                        Reporting.SetStepStatusFail($"Toggle <b style=\"color:blue;\">{label}</b> from <b>{previousState}</b> to <b>{s}</b>failed", _driver);
                        Thread.Sleep(2000); // Wait before retrying
                    }
                    catch (StaleElementReferenceException e)
                    {
                        // Log or handle StaleElementReferenceException
                        Console.WriteLine("StaleElementReferenceException caught: " + e.Message);
                        Reporting.SetStepStatusFail($"Toggle <b style=\"color:blue;\">{label}</b> from <b>{previousState}</b> to <b>{s}</b>failed", _driver);
                        Thread.Sleep(2000); // Wait before retrying
                    }
                    catch (Exception e)
                    {
                        // Log or handle other exceptions
                        Console.WriteLine("Exception caught: " + e.Message);
                        Reporting.SetStepStatusFail($"Toggle <b style=\"color:blue;\">{label}</b> from <b>{previousState}</b> to <b>{s}</b>failed", _driver);
                        Thread.Sleep(2000); // Wait before retrying
                    }

                    maxTry--;
                }

                // If we exit the loop without clicking, throw an exception
                Reporting.SetStepStatusFail($"Toggle <b style=\"color:blue;\">{label}</b> from <b>{previousState}</b> to <b>{s}</b>failed", _driver);
                throw new Exception("Failed to toggle the element after multiple attempts");
            }
            else
            {
                return;
            }
        }

        public void Radio(IList<IWebElement> elements, string s)
        {
            int maxTry = 5;
            string label = "";
            string selectedOption = "";


            // This will try 5 times to click an element
            while (maxTry > 0)
            {
                /*if (label.Length == 0)
                {
                    label = Helper.GetLabelName(elements.ElementAt(0));
                }*/


                try
                {
                    if (s.ToLower() == "yes" || s == "true" || s == "1")
                    {
                        selectedOption = elements.ElementAt(0).Text;
                        elements.ElementAt(0).Click();
                    }
                    else if (s.ToLower() == "no" || s == "false" || s == "0")
                    {
                        selectedOption = elements.ElementAt(1).Text;
                        elements.ElementAt(1).Click();
                    }
                    else
                    {
                        selectedOption = elements.ElementAt(2).Text;
                        elements.ElementAt(2).Click();
                    }
                    Reporting.SetStepStatusPass($"Select radio option <b>{selectedOption}</b>", _driver);/* from <b style=\"color:blue;\">{label}</b>*/
                    return; // Click was successful, exit the method
                }
                catch (ElementClickInterceptedException e)
                {
                    // Log or handle ElementClickInterceptedException
                    Console.WriteLine("ElementClickInterceptedException caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Select radio option <b>{selectedOption}</b> from <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (StaleElementReferenceException e)
                {
                    // Log or handle StaleElementReferenceException
                    Console.WriteLine("StaleElementReferenceException caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Select radio option <b>{selectedOption}</b> from <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (Exception e)
                {
                    // Log or handle other exceptions
                    Console.WriteLine("Exception caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Select radio option <b>{selectedOption}</b> from <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }

                maxTry--;
            }

            // If we exit the loop without clicking, throw an exception
            Reporting.SetStepStatusFail($"Select radio option <b>{selectedOption}</b> from <b style=\"color:blue;\">{label}</b> failed", _driver);
            throw new Exception("Failed to select the radio element after multiple attempts");

        }

        public void SendKeys(IWebElement element, string value)
        {
            int maxTry = 5;
            Console.WriteLine(value);
            string label = "";

            // This will try 5 times to input a value into an element
            while (maxTry > 0)
            {
                if (label.Length == 0)
                {
                    label = Helper.GetLabelName(element);
                }


                try
                {
                    element.Clear();
                    element.SendKeys(value);
                    Reporting.SetStepStatusPass($"Input value <b>{value}</b> to <b style=\"color:blue;\">{label}</b>", _driver);
                    return; // SendKeys was successful, exit the method
                }
                catch (ElementNotInteractableException e)
                {
                    // Log or handle ElementNotInteractableException
                    Console.WriteLine("ElementNotInteractableException caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Input value <b>{value}</b> to <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (StaleElementReferenceException e)
                {
                    // Log or handle StaleElementReferenceException
                    Console.WriteLine("StaleElementReferenceException caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Input value <b>{value}</b> to <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (Exception e)
                {
                    // Log or handle other exceptions
                    Console.WriteLine("Exception caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Input value <b>{value}</b> to <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }

                maxTry--;
            }

            // If we exit the loop without writing values, throw an exception
            Reporting.SetStepStatusFail($"Input value <b>{value}</b> to <b style=\"color:blue;\">{label}</b> failed", _driver);
            throw new Exception("Failed to send values to the element after multiple attempts");
        }

        public void Draw(IWebElement element, int fromX, int fromY, int lenX, int lenY)
        {
            int maxTry = 5;

            // This will try 5 times to perform a draw action on an element
            while (maxTry > 0)
            {
                try
                {
                    Actions actions = new Actions(_driver);
                    actions.MoveToElement(element);
                    actions.MoveByOffset(fromX, fromY);
                    actions.ClickAndHold();
                    int x = fromX + lenX;
                    int y = fromY + lenY;
                    actions.MoveByOffset(x, y);
                    actions.Release();
                    actions.Perform();
                    Thread.Sleep(2000);
                    return; // Draw was successful, exit the method
                }
                catch (ElementClickInterceptedException e)
                {
                    // Log or handle ElementClickInterceptedException
                    Console.WriteLine("ElementClickInterceptedException caught: " + e.Message);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (StaleElementReferenceException e)
                {
                    // Log or handle StaleElementReferenceException
                    Console.WriteLine("StaleElementReferenceException caught: " + e.Message);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (Exception e)
                {
                    // Log or handle other exceptions
                    Console.WriteLine("Exception caught: " + e.Message);
                    Thread.Sleep(2000); // Wait before retrying
                }

                maxTry--;
            }

            // If we exit the loop without clicking, throw an exception
            throw new Exception("Failed to perform the draw action on the element after multiple attempts");
        }

        public void SelectOption(IList<IWebElement> elementList, string searchValue)
        {
            int count = elementList.Count;

            Thread.Sleep(500);

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(elementList.ElementAt(i).Text.ToLower());
                if (elementList.ElementAt(i).Text.ToLower().Equals(searchValue.ToLower()))
                {
                    Actions actions = new Actions(_driver);
                    actions.ScrollToElement(elementList.ElementAt(i));
                    actions.Perform();
                    Thread.Sleep(250);
                    elementList.ElementAt(i).Click();
                    Reporting.SetStepStatusPass($"Select option <b>{searchValue}</b>", _driver);
                }
                else if (elementList.ElementAt(i).Text.ToLower().Contains(searchValue.ToLower()))
                {
                    Actions actions = new Actions(_driver);
                    actions.ScrollToElement(elementList.ElementAt(i));
                    actions.Perform();
                    Thread.Sleep(250);
                    elementList.ElementAt(i).Click();
                    Reporting.SetStepStatusPass($"Select option <b>{searchValue}</b>", _driver);
                }
            }
            Thread.Sleep(250);
        }

        public void SelectOptionChild(IList<IWebElement> elementList, string childXpath, string searchValue)
        {
            int count = elementList.Count;

            Thread.Sleep(500);

            for (int i = 0; i < count; i++)
            {
                Console.WriteLine(elementList.ElementAt(i).Text.ToLower());
                if (elementList.ElementAt(i).Text.ToLower().Equals(searchValue.ToLower()))
                {
                    Actions actions = new Actions(_driver);
                    actions.ScrollToElement(elementList.ElementAt(i));
                    actions.Perform();
                    Thread.Sleep(250);
                    elementList.ElementAt(i).FindElement(By.XPath(childXpath)).Click();
                    Reporting.SetStepStatusPass($"Select option <b>{searchValue}</b>", _driver);
                }
            }
            Thread.Sleep(250);
        }

        public void MultiSelectChild(IList<IWebElement> elementList, string childXpath, List<string> searchValue)
        {
            int countEL = elementList.Count;
            int countValue = searchValue.Count;

            Thread.Sleep(500);

            for (int i = 0; i < countValue; i++)
            {
                for (int j = 0; j < countEL; j++)
                {
                    Console.WriteLine(elementList.ElementAt(j).Text.ToLower());
                    if (elementList.ElementAt(j).Text.ToLower().Equals(searchValue.ElementAt(i).ToLower()))
                    {
                        Actions actions = new Actions(_driver);
                        actions.ScrollToElement(elementList.ElementAt(j));
                        actions.Perform();
                        Thread.Sleep(250);
                        elementList.ElementAt(j).FindElement(By.XPath(childXpath)).Click();
                        Reporting.SetStepStatusPass($"Select option <b>{searchValue.ElementAt(i)}</b>", _driver);
                    }
                }
            }
            Thread.Sleep(50);
        }

        public void SelectMultipleByValue(IList<IWebElement> elementList, List<string> searchValue)
        {
            int countEL = elementList.Count;
            int countValue = searchValue.Count;

            Thread.Sleep(500);

            for (int i = 0; i < countValue; i++)
            {
                for (int j = 0; j < countEL; j++)
                {
                    string value = elementList.ElementAt(j).GetAttribute("value");
                    if (value.Contains(searchValue.ElementAt(i)))
                    {
                        Actions actions = new Actions(_driver);
                        actions.ScrollToElement(elementList.ElementAt(j));
                        actions.Perform();
                        Thread.Sleep(250);
                        elementList.ElementAt(j).Click();
                        Reporting.SetStepStatusPass($"Select option <b>{searchValue.ElementAt(i)}</b>", _driver);
                    }
                }
            }
            Thread.Sleep(250);
        }

        public void Select(IWebElement selectElement, string value)
        {
            int maxTry = 5;
            string label = "";

            // This will try 5 times to perform a select action on an element
            while (maxTry > 0)
            {
                if (label.Length == 0)
                {
                    label = Helper.GetLabelName(selectElement);
                }


                try
                {
                    SelectElement select = new SelectElement(selectElement);

                    select.SelectByText(value);

                    Reporting.SetStepStatusPass($"Select option <b>{value}</b> from <b style=\"color:blue;\">{label}</b>", _driver);

                    return; // Select was successful, exit the method
                }
                catch (ElementNotInteractableException e)
                {
                    // Log or handle ElementNotInteractableException
                    Console.WriteLine("ElementNotInteractableException caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Select option <b>{value}</b> from <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (StaleElementReferenceException e)
                {
                    // Log or handle StaleElementReferenceException
                    Console.WriteLine("StaleElementReferenceException caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Select option <b>{value}</b> from <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }
                catch (Exception e)
                {
                    // Log or handle other exceptions
                    Console.WriteLine("Exception caught: " + e.Message);
                    Reporting.SetStepStatusFail($"Select option <b>{value}</b> from <b style=\"color:blue;\">{label}</b> failed", _driver);
                    Thread.Sleep(2000); // Wait before retrying
                }

                maxTry--;
            }

            // If we exit the loop without clicking, throw an exception
            Reporting.SetStepStatusFail($"Select option <b>{value}</b> from <b style=\"color:blue;\">{label}</b> failed", _driver);
            throw new Exception("Failed to perform the draw action on the element after multiple attempts");
        }

        public void SelectChild(IList<IWebElement> elementList, string childLocator)
        {
            Thread.Sleep(500);

            foreach (IWebElement element in elementList)
            {
                element.FindElement(By.XPath(childLocator)).Click();
                string searchValue = Helper.GetLabelName(element.FindElement(By.XPath(childLocator)));
                Reporting.SetStepStatusPass($"Select option <b>{searchValue}</b>", _driver);
            }
        }

        public void VerifyPageLabel(IWebElement element, string headerText, string searchText)
        {
            int maxTry = 5;
            while (maxTry > 0)
            {
                try
                {
                    if (headerText.ToLower().Contains(searchText))
                    {
                        Console.WriteLine(headerText);
                        Thread.Sleep(3000);
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Label not found");
                        Thread.Sleep(2000);
                        element.Click();
                    }
                }
                catch (Exception ex)
                {

                }

                maxTry--;
            }
        }

        public void ScrollToTop()
        {
            // Scroll to the top of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, 0);");
        }

        public void ScrollDwon()
        {
            // Scroll to the top of the page
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            js.ExecuteScript("window.scrollTo(0, 500);");
        }

        public IWebElement GetShadowRootElement(IWebElement shadowHost, string element)
        {
            // Use JavaScript to access the shadow root and find the target element
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            IWebElement signatureActionElement = (IWebElement)js.ExecuteScript(
                $"return arguments[0].shadowRoot.querySelector('{element}')", shadowHost);

            // Perform the click action on the element
            return signatureActionElement;
        }
/*
        public BasePage(IWebDriver driver) : base(driver)
        {
            _driver = driver;

            // Use fluent wait to find the element
            Wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }*/
    }
}

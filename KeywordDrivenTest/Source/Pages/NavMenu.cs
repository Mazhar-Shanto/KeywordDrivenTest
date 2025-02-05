using KeywordDrivenTest.KeywordRunner;
using KeywordDrivenTest.Source.Components;
using OpenQA.Selenium;

namespace KeywordDrivenTest.Source.Pages
{
    internal class NavMenu : KeywordLibrary
    {
        public NavMenu(IWebDriver driver) : base(driver)
        {
        }

        public void GoToCaseCreateFromIncidents()
        {
            Click(MenuComponent.Cases);
            Click(MenuComponent.CreateCaseFromIncidentButton);
        }
    }
}

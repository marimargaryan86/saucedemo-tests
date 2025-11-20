using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using SauceDemo.UiTests.Configuration;

namespace SauceDemo.UiTests.Pages;

public abstract class BasePage
{
    protected readonly IWebDriver Driver;
    protected readonly WebDriverWait Wait;
    protected readonly string BaseUrl;

    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        var settings = TestConfiguration.SeleniumSettings;
        BaseUrl = settings.BaseUrl;
        Wait = new WebDriverWait(driver, TimeSpan.FromSeconds(settings.ImplicitWaitSeconds));
    }

    protected IWebElement WaitAndFind(By by)
    {
        return Wait.Until(ExpectedConditions.ElementIsVisible(by));
    }

    public abstract bool IsAt();
}
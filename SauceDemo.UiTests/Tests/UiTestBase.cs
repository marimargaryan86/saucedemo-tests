using NUnit.Framework;
using OpenQA.Selenium;
using SauceDemo.UiTests.Configuration;
using SauceDemo.UiTests.Drivers;

namespace SauceDemo.UiTests.Tests;

public abstract class UiTestBase
{
    protected IWebDriver Driver = null!;
    private readonly BrowserType _browser;

    // Each fixture passes BrowserType into base
    protected UiTestBase(BrowserType browser)
    {
        _browser = browser;
    }

    [SetUp]
    public void SetUp()
    {
        Driver = DriverFactory.CreateDriver(_browser);

        var baseUrl = TestConfiguration.SeleniumSettings.BaseUrl.TrimEnd('/');
        Driver.Navigate().GoToUrl(baseUrl);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            Driver?.Quit();
            Driver?.Dispose();
        }
        catch
        {
            // ignore cleanup issues
        }
    }
}
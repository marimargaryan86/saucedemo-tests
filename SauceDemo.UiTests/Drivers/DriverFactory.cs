using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using WebDriverManager;
using WebDriverManager.DriverConfigs.Impl;
using SauceDemo.UiTests.Configuration;

namespace SauceDemo.UiTests.Drivers;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(BrowserType? overrideBrowser = null)
    {
        var settings = TestConfiguration.SeleniumSettings;

        // 1) If test passed a browser explicitly → use it
        // 2) Else allow TEST_BROWSER env var
        // 3) Else use Selenium:Browser from config
        var browserNameFromEnv = Environment.GetEnvironmentVariable("TEST_BROWSER");
        var browserName = overrideBrowser?.ToString() ?? browserNameFromEnv ?? settings.Browser;

        if (!Enum.TryParse<BrowserType>(browserName, ignoreCase: true, out var browserType))
        {
            browserType = BrowserType.Chrome;
        }

        IWebDriver driver = browserType switch
        {
            BrowserType.Chrome  => CreateChromeDriver(),
            BrowserType.Firefox => CreateFirefoxDriver(),
            _ => throw new NotSupportedException($"Browser '{browserType}' is not supported.")
        };

        // Common settings
        driver.Manage().Timeouts().ImplicitWait =
            TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);

        driver.Manage().Timeouts().PageLoad =
            TimeSpan.FromSeconds(settings.PageLoadTimeoutSeconds);

        driver.Manage().Window.Maximize();

        return driver;
    }

    private static IWebDriver CreateChromeDriver()
    {
        new DriverManager().SetUpDriver(new ChromeConfig());

        var options = new ChromeOptions();

        // Disable Chrome password manager & leak detection
        options.AddUserProfilePreference("credentials_enable_service", false);
        options.AddUserProfilePreference("profile.password_manager_enabled", false);
        options.AddArgument("--disable-save-password-bubble");
        options.AddArgument("--disable-notifications");
        options.AddArgument("--disable-infobars");
        options.AddArgument("--disable-features=PasswordManagerOnboarding,PasswordLeakDetection");

        // options.AddArgument("--headless=new"); // for CI if needed

        return new ChromeDriver(options);
    }

    private static IWebDriver CreateFirefoxDriver()
    {
        new DriverManager().SetUpDriver(new FirefoxConfig());

        var options = new FirefoxOptions();
        // options.AddArgument("-headless"); // for CI if needed

        return new FirefoxDriver(options);
    }
}

using OpenQA.Selenium;
using SauceDemo.UiTests.Configuration;

namespace SauceDemo.UiTests.Pages;

public class LoginPage : BasePage
{
    private readonly By _usernameInput = By.Id("user-name");
    private readonly By _passwordInput = By.Id("password");
    private readonly By _loginButton = By.Id("login-button");

    public LoginPage(IWebDriver driver) : base(driver) { }

    public LoginPage GoTo()
    {
        Driver.Navigate().GoToUrl(BaseUrl);
        return this;
    }

    public ProductsPage LoginAsStandardUser()
    {
        var settings = TestConfiguration.SeleniumSettings;

        WaitAndFind(_usernameInput).Clear();
        WaitAndFind(_usernameInput).SendKeys(settings.Username);

        WaitAndFind(_passwordInput).Clear();
        WaitAndFind(_passwordInput).SendKeys(settings.Password);

        WaitAndFind(_loginButton).Click();

        return new ProductsPage(Driver);
    }

    public override bool IsAt()
    {
        return Driver.Url.Contains("saucedemo.com", StringComparison.OrdinalIgnoreCase);
    }
}
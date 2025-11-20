using OpenQA.Selenium;

namespace SauceDemo.UiTests.Pages;

public class CheckoutCompletePage : BasePage
{
    private readonly By _completeHeader = By.CssSelector("[data-test=\"complete-header\"]");

    public CheckoutCompletePage(IWebDriver driver) : base(driver) { }

    public override bool IsAt()
    {
        return Driver.Url.Contains("checkout-complete.html", StringComparison.OrdinalIgnoreCase);
    }

    public string GetThankYouMessage()
    {
        return WaitAndFind(_completeHeader).Text;
    }
}
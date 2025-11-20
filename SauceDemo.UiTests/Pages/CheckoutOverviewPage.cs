using OpenQA.Selenium;

namespace SauceDemo.UiTests.Pages;

public class CheckoutOverviewPage : BasePage
{
    private readonly By _finishButton = By.CssSelector("[data-test=\"finish\"]");

    public CheckoutOverviewPage(IWebDriver driver) : base(driver) { }

    public override bool IsAt()
    {
        return Driver.Url.Contains("checkout-step-two.html", StringComparison.OrdinalIgnoreCase);
    }

    public CheckoutCompletePage ClickFinish()
    {
        WaitAndFind(_finishButton).Click();
        return new CheckoutCompletePage(Driver);
    }
}
using OpenQA.Selenium;
using SeleniumExtras.WaitHelpers;

namespace SauceDemo.UiTests.Pages;

public class CheckoutInformationPage : BasePage
{
    private readonly By _firstName   = By.CssSelector("[data-test=\"firstName\"]");
    private readonly By _lastName    = By.CssSelector("[data-test=\"lastName\"]");
    private readonly By _postalCode  = By.CssSelector("[data-test=\"postalCode\"]");
    private readonly By _continueButton = By.CssSelector("[data-test=\"continue\"]");

    public CheckoutInformationPage(IWebDriver driver) : base(driver) { }

    public override bool IsAt()
    {
        return Driver.Url.Contains("checkout-step-one.html", StringComparison.OrdinalIgnoreCase);
    }

    public CheckoutOverviewPage FillCustomerInfo(string firstName, string lastName, string postalCode)
    {
        // First name
        var firstNameInput = Wait.Until(ExpectedConditions.ElementExists(_firstName));
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", firstNameInput);
        firstNameInput.Clear();
        firstNameInput.SendKeys(firstName);

        // Last name
        var lastNameInput = Wait.Until(ExpectedConditions.ElementExists(_lastName));
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", lastNameInput);
        lastNameInput.Clear();
        lastNameInput.SendKeys(lastName);

        // Postal code (the one that was failing)
        var postalCodeInput = Wait.Until(ExpectedConditions.ElementExists(_postalCode));
        ((IJavaScriptExecutor)Driver).ExecuteScript("arguments[0].scrollIntoView(true);", postalCodeInput);
        postalCodeInput.Clear();
        postalCodeInput.SendKeys(postalCode);

        // Continue
        var continueButton = Wait.Until(ExpectedConditions.ElementToBeClickable(_continueButton));
        continueButton.Click();

        return new CheckoutOverviewPage(Driver);
    }
}
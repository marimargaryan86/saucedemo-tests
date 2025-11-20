using OpenQA.Selenium;

namespace SauceDemo.UiTests.Pages;

public class CartPage : BasePage
{
    private readonly By _cartItems = By.ClassName("cart_item");
    private readonly By _checkoutButton = By.Id("checkout");

    public CartPage(IWebDriver driver) : base(driver) { }

    public override bool IsAt()
    {
        return Driver.Url.Contains("cart.html", StringComparison.OrdinalIgnoreCase);
    }

    public int GetCartItemsCount()
    {
        return Driver.FindElements(_cartItems).Count;
    }

    public CheckoutInformationPage ClickCheckout()
    {
        WaitAndFind(_checkoutButton).Click();
        return new CheckoutInformationPage(Driver);
    }
}
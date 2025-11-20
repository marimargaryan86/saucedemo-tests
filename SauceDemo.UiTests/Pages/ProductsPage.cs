using OpenQA.Selenium;
using OpenQA.Selenium.BiDi.BrowsingContext;
using OpenQA.Selenium.Support.UI;

namespace SauceDemo.UiTests.Pages;

public class ProductsPage : BasePage
{
    private readonly By _title = By.ClassName("title");
    private readonly By _sortDropdown = By.CssSelector("[data-test=\"product-sort-container\"]");
    private readonly By _inventoryItems =  By.CssSelector("[data-test=\"inventory-item\"]");
    private readonly By _cartIcon = By.CssSelector("[data-test=\"shopping-cart-link\"]");

    public ProductsPage(IWebDriver driver) : base(driver) { }

    public override bool IsAt()
    {
        return WaitAndFind(_title).Text.Equals("Products", StringComparison.OrdinalIgnoreCase);
    }

    public ProductsPage SortByPriceLowToHigh()
    {
        var dropdown = new SelectElement(WaitAndFind(_sortDropdown));
        dropdown.SelectByValue("lohi");
        return this;
    }

    public ProductsPage AddTwoCheapestItemsToCart()
    {
        var items = Driver.FindElements(_inventoryItems);

        // after sort lo->hi, first 2 are cheapest
        foreach (var item in items.Take(2))
        {
            var addButton = item.FindElement(By.CssSelector("button.btn_inventory"));
            addButton.Click();
        }

        return this;
    }

    public CartPage GoToCart()
    {
        WaitAndFind(_cartIcon).Click();
        return new CartPage(Driver);
    }
}
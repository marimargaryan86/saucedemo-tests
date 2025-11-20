using NUnit.Framework;
using SauceDemo.UiTests.Configuration;
using SauceDemo.UiTests.Pages;
using Allure.NUnit;
using Allure.NUnit.Attributes;
using Allure.Net.Commons;
namespace SauceDemo.UiTests.Tests;

[TestFixture(BrowserType.Chrome)]
[TestFixture(BrowserType.Firefox)]
[Parallelizable(ParallelScope.Self)]
[AllureNUnit]
[AllureParentSuite("Swag Labs UI")]
[AllureSuite("E2E Purchase Flow")]
[AllureFeature("Standard user checkout")]
public class PurchaseTests : UiTestBase
{
    public PurchaseTests(BrowserType browser) : base(browser) { }

    [Test]
    [AllureName("Standard user can complete successful purchase")]
    [AllureTag("UI", "Regression")]
    [AllureSeverity(SeverityLevel.critical)]
    public void Standard_user_can_complete_successful_purchase()
    {
        // 1–3: login
        var loginPage = new LoginPage(Driver).GoTo();
        var productsPage = loginPage.LoginAsStandardUser();

        Assert.That(
            productsPage.IsAt(),
            Is.True,
            "User should be on Products page after login."
        );

        // 4: sort by price low to high
        productsPage.SortByPriceLowToHigh();

        // 5: add two cheapest items
        productsPage.AddTwoCheapestItemsToCart();

        // 6: go to cart
        var cartPage = productsPage.GoToCart();
        Assert.That(
            cartPage.IsAt(),
            Is.True,
            "User should be on Cart page."
        );

        // 7: verify 2 items in cart
        Assert.That(
            cartPage.GetCartItemsCount(),
            Is.EqualTo(2),
            "Cart should contain 2 items."
        );

        // 8: checkout
        var infoPage = cartPage.ClickCheckout();
        Assert.That(
            infoPage.IsAt(),
            Is.True,
            "User should be on Checkout: Your Information page."
        );

        // 9–10: fill info and continue
        var overviewPage = infoPage.FillCustomerInfo("Test", "User", "12345");
        Assert.That(
            overviewPage.IsAt(),
            Is.True,
            "User should be on Checkout: Overview page."
        );

        // 11: finish
        var completePage = overviewPage.ClickFinish();
        Assert.That(
            completePage.IsAt(),
            Is.True,
            "User should be on Checkout: Complete page."
        );

        // 12: verify thank-you message
        var thankYou = completePage.GetThankYouMessage();
        Assert.That(
            thankYou,
            Does.Contain("thank you for your order").IgnoreCase,
            "Thank-you message text should be shown."
        );
    }
}

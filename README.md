# SauceDemo UI Tests (C# / Selenium / NUnit)

This repository contains a small but production-style UI test automation framework
for [Sauce Labs Swag Labs](https://www.saucedemo.com/).

It was created as an interview assignment to demonstrate:
- C# + Selenium WebDriver + NUnit
- Page Object Model (POM)
- Multi-browser support (Chrome & Firefox)
- Multi-environment config (QA / Prod)
- Allure reporting
- Parallel test execution

## Tech Stack

- .NET 8
- C#, NUnit 4
- Selenium WebDriver
- WebDriverManager
- Allure.NUnit
- JetBrains Rider IDE

## Project Structure

- `SauceDemo.UiTests/Configuration`  
  - `SeleniumSettings.cs` – strongly-typed config  
  - `TestConfiguration.cs` – loads appsettings + env  
  - `BrowserType.cs` – supported browsers enum  

- `SauceDemo.UiTests/Drivers`  
  - `DriverFactory.cs` – creates Chrome/Firefox drivers with common options  

- `SauceDemo.UiTests/Pages`  
  - `BasePage.cs` – shared WebDriver + waits  
  - `LoginPage.cs`, `ProductsPage.cs`, `CartPage.cs`,  
    `CheckoutInformationPage.cs`, `CheckoutOverviewPage.cs`, `CheckoutCompletePage.cs`  

- `SauceDemo.UiTests/Tests`  
  - `UiTestBase.cs` – base class (driver lifecycle, navigation to BaseUrl)  
  - `PurchaseTests.cs` – end-to-end “successful purchase” scenario  
    - runs for both Chrome and Firefox using `[TestFixture(BrowserType.Chrome)]` and `[TestFixture(BrowserType.Firefox)]`  
    - decorated with Allure attributes and step methods  

## Configuration

Config files are in `SauceDemo.UiTests`:

- `appsettings.json` – common defaults  
- `appsettings.Qa.json` – QA environment (base URL, username)  
- `appsettings.Prod.json` – Prod environment (example)  
- `appsettings.Local.json` – local overrides (passwords etc.), excluded from git  

Environment is chosen via `TEST_ENVIRONMENT`:

```bash
# QA (default)
dotnet test

# Explicit QA
TEST_ENVIRONMENT=Qa dotnet test

# Prod
TEST_ENVIRONMENT=Prod dotnet test

namespace SauceDemo.UiTests.Configuration;

public class SeleniumSettings
{
    public string Browser { get; set; } = "Chrome";
    public string BaseUrl { get; set; } = "";
    public int ImplicitWaitSeconds { get; set; } = 5;
    public int PageLoadTimeoutSeconds { get; set; } = 30;
    public string Username { get; set; } = "";
    public string Password { get; set; } = "";
}
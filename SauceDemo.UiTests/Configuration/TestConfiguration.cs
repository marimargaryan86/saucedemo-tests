using Microsoft.Extensions.Configuration;
namespace SauceDemo.UiTests.Configuration;

public class TestConfiguration
{
    private static IConfigurationRoot Configuration()
    {
        var envName = Environment.GetEnvironmentVariable("TEST_ENVIRONMENT") ?? "Qa";

        var builder = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{envName}.json", optional: true, reloadOnChange: true)
            .AddJsonFile("appsettings.Local.json", optional: true, reloadOnChange: true);


        return builder.Build();
    }

    public static SeleniumSettings SeleniumSettings =>
        Configuration().GetSection("Selenium").Get<SeleniumSettings>()!;
}

using Microsoft.AspNetCore.Mvc.Testing;
using PuppeteerSharp;
using System.Diagnostics;

namespace MealPlanner.Tests;

// NOTE: These Puppeteer tests require Chrome/Chromium system dependencies
// In WSL environments, you may need to install: sudo apt install -y libasound2-dev libatk-bridge2.0-dev libdrm2 libxss1 libgtk-3-dev libxrandr2 libasound2 libpangocairo-1.0-0 libatk1.0-dev libcairo-gobject2 libgtk-3-0 libgdk-pixbuf2.0-dev

public class IntegrationTests : IClassFixture<WebApplicationFactory<Program>>, IAsyncLifetime
{
    private readonly WebApplicationFactory<Program> _factory;
    private IBrowser? _browser;
    private string _serverUrl = string.Empty;

    public IntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    public async Task InitializeAsync()
    {
        // Download Chromium if needed
        await new BrowserFetcher().DownloadAsync();
        
        // Launch browser with additional arguments for WSL/Linux environments
        _browser = await Puppeteer.LaunchAsync(new LaunchOptions
        {
            Headless = true,
            Args = new[] { 
                "--no-sandbox", 
                "--disable-setuid-sandbox",
                "--disable-dev-shm-usage",
                "--disable-gpu",
                "--no-first-run",
                "--no-zygote",
                "--single-process",
                "--disable-background-timer-throttling",
                "--disable-backgrounding-occluded-windows",
                "--disable-renderer-backgrounding"
            }
        });

        // Start the test server
        var client = _factory.CreateClient();
        _serverUrl = client.BaseAddress?.ToString().TrimEnd('/') ?? "http://localhost:5000";
    }

    public async Task DisposeAsync()
    {
        if (_browser != null)
        {
            await _browser.CloseAsync();
        }
    }

    [Fact]
    public async Task FoodPreferences_RequiresLogin_AndShowsPreferencesAfterLogin()
    {
        if (_browser == null)
            throw new InvalidOperationException("Browser not initialized");

        var page = await _browser.NewPageAsync();
        
        try
        {
            // Navigate to the home page first
            await page.GoToAsync(_serverUrl);
            await page.WaitForSelectorAsync("body", new WaitForSelectorOptions { Timeout = 5000 });

            // Navigate to Food Preferences page
            await page.GoToAsync($"{_serverUrl}/meal-preferences");
            await page.WaitForSelectorAsync("body", new WaitForSelectorOptions { Timeout = 5000 });

            // Verify that a login required message is present
            var loginRequiredElement = await page.QuerySelectorAsync(".login-required");
            Assert.NotNull(loginRequiredElement);
            
            // Verify the specific login required text
            var loginRequiredText = await page.QuerySelectorAsync("text/Login Required");
            Assert.NotNull(loginRequiredText);

            // Click the Login button
            var loginButton = await page.QuerySelectorAsync("button:has-text('Login')");
            Assert.NotNull(loginButton);
            await loginButton!.ClickAsync();

            // Wait for modal to appear and input field to be visible
            await page.WaitForSelectorAsync("#loginModal input[type='text']", new WaitForSelectorOptions { Timeout = 3000 });
            
            // Enter the test username
            await page.TypeAsync("#loginModal input[type='text']", "Testy Tester");
            
            // Click Save button
            var saveButton = await page.QuerySelectorAsync("#loginModal .btn-primary");
            Assert.NotNull(saveButton);
            await saveButton!.ClickAsync();

            // Wait for modal to close and page to update
            await Task.Delay(1000);

            // Navigate to Food Preferences again
            await page.GoToAsync($"{_serverUrl}/meal-preferences");
            await page.WaitForSelectorAsync("body", new WaitForSelectorOptions { Timeout = 5000 });

            // Verify that food preferences show up (look for the meal rating container)
            var mealRatingContainer = await page.QuerySelectorAsync(".meal-rating-container");
            Assert.NotNull(mealRatingContainer);

            // Verify that we can see some food rating items
            var foodRatingItems = await page.QuerySelectorAllAsync(".food-rating-item");
            Assert.True(foodRatingItems.Length > 0, "Expected to find food rating items on the page");

            // Verify that the user greeting is displayed (using the actual format from the page)
            var userGreeting = await page.QuerySelectorAsync("text/Welcome Testy Tester!");
            Assert.NotNull(userGreeting);
        }
        finally
        {
            await page.CloseAsync();
        }
    }

    [Fact]
    public async Task HomePage_LoadsSuccessfully()
    {
        if (_browser == null)
            throw new InvalidOperationException("Browser not initialized");

        var page = await _browser.NewPageAsync();

        try
        {
            await page.GoToAsync(_serverUrl);
            await page.WaitForSelectorAsync("body", new WaitForSelectorOptions { Timeout = 5000 });

            var title = await page.GetTitleAsync();
            Assert.Contains("MealPlanner", title);

            // Check that the navigation menu is present
            var navMenu = await page.QuerySelectorAsync(".navbar");
            Assert.NotNull(navMenu);
        }
        finally
        {
            await page.CloseAsync();
        }
    }
}
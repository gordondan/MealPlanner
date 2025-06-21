using Microsoft.AspNetCore.Mvc.Testing;

namespace MealPlanner.Tests;

public class LoginIntegrationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public LoginIntegrationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task MealPreferencesPage_ShowsLoginRequiredMessage_WhenNotLoggedIn()
    {
        // Act
        var response = await _client.GetAsync("/meal-preferences");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        // Assert - Check that login required elements are present
        Assert.Contains("Login Required", content);
        Assert.Contains("Please login to rate your food preferences", content);
        Assert.Contains("login-required", content); // CSS class
        
        // Assert - Check that food rating functionality is not shown when not logged in
        Assert.DoesNotContain("meal-rating-container", content);
        Assert.DoesNotContain("Welcome", content); // User greeting should not be present
    }

    [Fact]
    public async Task MealPreferencesPage_ContainsLoginButton()
    {
        // Act
        var response = await _client.GetAsync("/meal-preferences");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        // Assert - Check that login button is present
        Assert.Contains("Login", content);
        Assert.Contains("btn", content); // Bootstrap button classes
    }

    [Fact]
    public async Task HomePage_ContainsLoginComponent()
    {
        // Act
        var response = await _client.GetAsync("/");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        // Assert - Check that the login component is rendered
        Assert.Contains("login-component", content);
    }

    [Fact]
    public async Task MealPreferencesPage_ContainsExpectedFoodCategories()
    {
        // Act
        var response = await _client.GetAsync("/meal-preferences");
        response.EnsureSuccessStatusCode();

        var content = await response.Content.ReadAsStringAsync();

        // The page should contain references to food categories even when login is required
        // This tests that the page structure is correct
        Assert.Contains("Meal Preferences", content);
        Assert.Contains("food preferences", content);
    }

    [Fact] 
    public async Task StaticAssets_AreAccessible()
    {
        // Test rating images that should be used in the food preferences
        var imageFiles = new[] { "good.png", "great.png", "meh.png", "ok.png", "yuck.png" };
        
        foreach (var imageFile in imageFiles)
        {
            var response = await _client.GetAsync($"/images/{imageFile}");
            Assert.True(response.IsSuccessStatusCode, $"Image {imageFile} should be accessible");
        }
    }
}
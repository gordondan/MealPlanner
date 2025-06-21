using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace MealPlanner.Tests;

public class WebApplicationTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly HttpClient _client;

    public WebApplicationTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
        _client = _factory.CreateClient();
    }

    [Fact]
    public async Task HomePage_ReturnsSuccessAndCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("MealPlanner", content);
    }

    [Fact]
    public async Task MealPreferencesPage_ReturnsSuccessAndCorrectContentType()
    {
        // Act
        var response = await _client.GetAsync("/meal-preferences");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal("text/html; charset=utf-8", response.Content.Headers.ContentType?.ToString());
        
        var content = await response.Content.ReadAsStringAsync();
        Assert.Contains("Meal Preferences", content);
        Assert.Contains("Login Required", content); // Should show login required message
    }

    [Fact]
    public async Task StaticFiles_AreServedCorrectly()
    {
        // Test that CSS files are served
        var cssResponse = await _client.GetAsync("/app.css");
        cssResponse.EnsureSuccessStatusCode();

        // Test that favicon is served
        var faviconResponse = await _client.GetAsync("/favicon.png");
        faviconResponse.EnsureSuccessStatusCode();
    }

    [Theory]
    [InlineData("/")]
    [InlineData("/meal-preferences")]
    public async Task Pages_ReturnSuccessStatusCodes(string url)
    {
        // Act
        var response = await _client.GetAsync(url);

        // Assert
        response.EnsureSuccessStatusCode();
    }
}
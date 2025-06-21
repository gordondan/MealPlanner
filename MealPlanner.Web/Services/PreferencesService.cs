using System.Text.Json;
using MealPlanner.Models;
using Microsoft.AspNetCore.Components;

namespace MealPlanner.Services
{
    public class PreferencesService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;
        private PreferencesData _preferencesData = new();
        private readonly string _preferencesUrl = "repo/preferences.json";

        public PreferencesService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task LoadPreferencesAsync()
        {
            try
            {
                var json = await _httpClient.GetStringAsync(_preferencesUrl);
                if (!string.IsNullOrWhiteSpace(json) && json != "{}")
                {
                    _preferencesData = JsonSerializer.Deserialize<PreferencesData>(json) ?? new PreferencesData();
                }
            }
            catch (Exception)
            {
                _preferencesData = new PreferencesData();
            }
        }

        public UserPreferences GetUserPreferences(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return new UserPreferences();

            if (!_preferencesData.Users.ContainsKey(userName))
            {
                _preferencesData.Users[userName] = new UserPreferences();
            }

            return _preferencesData.Users[userName];
        }

        public int GetFoodRating(string userName, string category, int foodId)
        {
            var userPrefs = GetUserPreferences(userName);
            
            if (!userPrefs.Categories.ContainsKey(category))
                return 0;

            if (!userPrefs.Categories[category].FoodRatings.ContainsKey(foodId))
                return 0;

            return userPrefs.Categories[category].FoodRatings[foodId];
        }

        public async Task SetFoodRatingAsync(string userName, string category, int foodId, int rating)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return;

            var userPrefs = GetUserPreferences(userName);

            if (!userPrefs.Categories.ContainsKey(category))
            {
                userPrefs.Categories[category] = new CategoryPreferences();
            }

            userPrefs.Categories[category].FoodRatings[foodId] = rating;

            await SavePreferencesAsync();
        }

        public async Task ClearUserPreferencesAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return;

            if (_preferencesData.Users.ContainsKey(userName))
            {
                _preferencesData.Users[userName] = new UserPreferences();
                await SavePreferencesAsync();
            }
        }

        private Task SavePreferencesAsync()
        {
            try
            {
                var json = JsonSerializer.Serialize(_preferencesData, new JsonSerializerOptions 
                { 
                    WriteIndented = true 
                });

                // Note: In a real application, you would need a server-side API to save files
                // For now, this simulates the save operation
                Console.WriteLine("Preferences would be saved: " + json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving preferences: {ex.Message}");
            }
            
            return Task.CompletedTask;
        }
    }
}
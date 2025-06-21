using System.Text.Json;
using MealPlanner.Models;
using Microsoft.AspNetCore.Components;

namespace MealPlanner.Services
{
    public class FoodService
    {
        private readonly HttpClient _httpClient;
        private readonly NavigationManager _navigationManager;

        public FoodService(HttpClient httpClient, NavigationManager navigationManager)
        {
            _httpClient = httpClient;
            _navigationManager = navigationManager;
            _httpClient.BaseAddress = new Uri(_navigationManager.BaseUri);
        }

        public async Task<List<FoodItem>> GetFoodItemsAsync()
        {
            try
            {
                var json = await _httpClient.GetStringAsync("repo/foods.json");
                var foods = JsonSerializer.Deserialize<List<FoodItem>>(json) ?? new List<FoodItem>();
                return foods;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading food items: {ex.Message}");
                return new List<FoodItem>();
            }
        }

        public async Task<List<FoodItem>> GetFoodItemsByCategoryAsync(string category)
        {
            var allFoods = await GetFoodItemsAsync();
            return allFoods.Where(f => f.Category.Equals(category, StringComparison.OrdinalIgnoreCase)).ToList();
        }
    }
}
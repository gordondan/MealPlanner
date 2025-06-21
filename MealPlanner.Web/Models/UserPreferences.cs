namespace MealPlanner.Models
{
    public class UserPreferences
    {
        public Dictionary<string, CategoryPreferences> Categories { get; set; } = new();
    }

    public class CategoryPreferences
    {
        public Dictionary<int, int> FoodRatings { get; set; } = new();
    }

    public class PreferencesData
    {
        public Dictionary<string, UserPreferences> Users { get; set; } = new();
    }
}
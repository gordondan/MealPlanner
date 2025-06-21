namespace MealPlanner.Models
{
    public class FoodItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool NeedsFreezer { get; set; }
        public bool NeedsRefrigeration { get; set; }
    }
}
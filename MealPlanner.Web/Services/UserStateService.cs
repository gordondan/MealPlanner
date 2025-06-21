namespace MealPlanner.Services
{
    public class UserStateService
    {
        private string _currentUser = string.Empty;
        
        public string CurrentUser 
        { 
            get => _currentUser; 
            set 
            { 
                _currentUser = value;
                OnUserChanged?.Invoke(value);
            } 
        }

        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(_currentUser);

        public event Action<string>? OnUserChanged;
    }
}
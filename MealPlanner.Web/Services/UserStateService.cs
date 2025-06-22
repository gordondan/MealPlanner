using Microsoft.Extensions.Logging;

namespace MealPlanner.Services
{
    public class UserStateService
    {
        private readonly ILogger<UserStateService> _logger;
        private string _currentUser = string.Empty;
        
        public UserStateService(ILogger<UserStateService> logger)
        {
            _logger = logger;
        }
        
        public string CurrentUser 
        { 
            get => _currentUser; 
            set 
            { 
                _logger.LogInformation("CurrentUser changing from '{OldUser}' to '{NewUser}'", _currentUser, value);
                try
                {
                    _currentUser = value;
                    _logger.LogInformation("Invoking OnUserChanged event with value '{Value}'", value);
                    OnUserChanged?.Invoke(value);
                    _logger.LogInformation("OnUserChanged event completed successfully");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error in UserStateService.CurrentUser setter");
                    throw;
                }
            } 
        }

        public bool IsLoggedIn => !string.IsNullOrWhiteSpace(_currentUser);

        public event Action<string>? OnUserChanged;
    }
}
using WorkweekChecker.Services;
namespace WorkweekChecker.ViewModels
{
    public class ViewModelLocator
    {
        private readonly WorkweekService _service = new();
        public MainViewModel Main => new MainViewModel(_service);
    }
}

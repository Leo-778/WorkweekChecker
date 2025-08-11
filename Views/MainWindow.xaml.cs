using System.Windows;
namespace WorkweekChecker.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            WorkweekChecker.ViewModels.ThemeManager.Apply(WorkweekChecker.ViewModels.AppTheme.Light);
        }
    }
}

using System.Windows;
using System.Windows.Controls;

namespace WorkweekChecker.Controls
{
    public partial class Badge : UserControl
    {
        public Badge() { InitializeComponent(); }
        public static readonly DependencyProperty TextProperty =
            DependencyProperty.Register(nameof(Text), typeof(string), typeof(Badge), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty EmphasizedTextProperty =
            DependencyProperty.Register(nameof(EmphasizedText), typeof(string), typeof(Badge), new PropertyMetadata(string.Empty));
        public static readonly DependencyProperty IsRestProperty =
            DependencyProperty.Register(nameof(IsRest), typeof(bool), typeof(Badge), new PropertyMetadata(false));
        public string Text { get => (string)GetValue(TextProperty); set => SetValue(TextProperty, value); }
        public string EmphasizedText { get => (string)GetValue(EmphasizedTextProperty); set => SetValue(EmphasizedTextProperty, value); }
        public bool IsRest { get => (bool)GetValue(IsRestProperty); set => SetValue(IsRestProperty, value); }
    }
}

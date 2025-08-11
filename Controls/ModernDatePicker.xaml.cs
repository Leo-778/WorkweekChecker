using System;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace WorkweekChecker.Controls
{
    public partial class ModernDatePicker : UserControl
    {
        public ModernDatePicker() { InitializeComponent(); }

        public static readonly DependencyProperty SelectedDateProperty =
            DependencyProperty.Register(nameof(SelectedDate), typeof(DateTime?), typeof(ModernDatePicker),
                new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnSelectedDateChanged));

        public DateTime? SelectedDate
        {
            get => (DateTime?)GetValue(SelectedDateProperty);
            set => SetValue(SelectedDateProperty, value);
        }

        public static readonly DependencyProperty DateTextProperty =
            DependencyProperty.Register(nameof(DateText), typeof(string), typeof(ModernDatePicker),
                new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDateTextChanged));

        public string DateText
        {
            get => (string)GetValue(DateTextProperty);
            set => SetValue(DateTextProperty, value);
        }

        private static readonly Regex DateRegex = new Regex(@"^\d{4}-\d{1,2}-\d{1,2}$");

        private static void OnSelectedDateChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ModernDatePicker mdp)
            {
                mdp.DateText = mdp.SelectedDate?.ToString("yyyy-MM-dd") ?? string.Empty;
            }
        }

        private static void OnDateTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is ModernDatePicker mdp)
            {
                var s = (string)e.NewValue ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(s) && DateRegex.IsMatch(s))
                {
                    if (DateTime.TryParseExact(s, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out var dt))
                        mdp.SelectedDate = dt;
                }
                else if (string.IsNullOrWhiteSpace(s))
                {
                    mdp.SelectedDate = null;
                }
            }
        }

        private void OpenCalendar_Click(object sender, RoutedEventArgs e) => CalendarPopup.IsOpen = true;
    }
}

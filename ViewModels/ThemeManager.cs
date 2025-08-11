using System;
using System.Windows;

namespace WorkweekChecker.ViewModels
{
    public enum AppTheme { Light, Dark }
    public static class ThemeManager
    {
        public static AppTheme Current { get; private set; } = AppTheme.Light;
        public static void Apply(AppTheme theme)
        {
            var app = Application.Current;
            if (app == null) return;
            var merged = app.Resources.MergedDictionaries;
            if (merged.Count == 0) return;
            while (merged.Count > 1) merged.RemoveAt(1);
            var uri = new Uri(theme == AppTheme.Light ? "Themes/Light.xaml" : "Themes/Dark.xaml", UriKind.Relative);
            merged.Add(new ResourceDictionary { Source = uri });
            Current = theme;
        }
    }
}

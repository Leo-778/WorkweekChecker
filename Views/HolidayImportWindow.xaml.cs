using System;
using System.IO;
using System.Text.Json;
using System.Windows;
using Microsoft.Win32;
using WorkweekChecker.Services;

namespace WorkweekChecker.Views
{
    public partial class HolidayImportWindow : Window
    {
        private readonly HolidayService _holidays;
        public HolidayImportWindow(HolidayService holidays)
        {
            InitializeComponent();
            _holidays = holidays;
        }
        private void LoadFile_Click(object sender, RoutedEventArgs e)
        {
            var dlg = new OpenFileDialog { Filter = "JSON 文件 (*.json)|*.json|所有文件 (*.*)|*.*" };
            if (dlg.ShowDialog(this) == true) PreviewBox.Text = File.ReadAllText(dlg.FileName);
        }
        private void LoadClipboard_Click(object sender, RoutedEventArgs e)
        {
            if (Clipboard.ContainsText()) PreviewBox.Text = Clipboard.GetText();
        }
        private void Import_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var doc = JsonSerializer.Deserialize<HolidayDoc>(PreviewBox.Text);
                if (doc == null) { MessageBox.Show("解析失败。", "错误", MessageBoxButton.OK, MessageBoxImage.Error); return; }
                foreach (var s in doc.specialHolidays) _holidays.SpecialHolidays.Add(DateTime.Parse(s).Date);
                foreach (var s in doc.specialWorkdays) _holidays.SpecialWorkdays.Add(DateTime.Parse(s).Date);
                _holidays.Save();
                MessageBox.Show("已导入并保存。", "成功", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("导入失败：" + ex.Message, "错误", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Close_Click(object sender, RoutedEventArgs e) => Close();
        private sealed class HolidayDoc
        {
            public string[] specialHolidays { get; set; } = Array.Empty<string>();
            public string[] specialWorkdays { get; set; } = Array.Empty<string>();
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using WorkweekChecker.Services;

namespace WorkweekChecker.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly WorkweekService _service;
        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged(string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public MainViewModel(WorkweekService service)
        {
            _service = service;
            _service.Load();
            Today = DateTime.Today;
            QueryDate = Today;
            UpdateToday();
            UpdateWeekRestMap();
            Query();
            ThemeIndex = 0;
        }

        public int ThemeIndex
        {
            get => _themeIndex;
            set { if (_themeIndex != value) { _themeIndex = value; OnPropertyChanged(nameof(ThemeIndex)); ThemeManager.Apply(value == 0 ? AppTheme.Light : AppTheme.Dark); } }
        }
        private int _themeIndex;

        public DateTime Today { get; private set; }
        public string TodayString => Today.ToString("yyyy-MM-dd dddd");
        public bool TodayIsDoubleRest => _service.IsDoubleRestWeek(Today);
        public string TodayRestText => TodayIsDoubleRest ? "本周：双休" : "本周：单休";

        private Dictionary<DayOfWeek, bool> _weekRestMap = new();
        public Dictionary<DayOfWeek, bool> WeekRestMap { get => _weekRestMap; private set { _weekRestMap = value; OnPropertyChanged(nameof(WeekRestMap)); } }

        private Dictionary<DayOfWeek, bool> _queryWeekRestMap = new();
        public Dictionary<DayOfWeek, bool> QueryWeekRestMap { get => _queryWeekRestMap; private set { _queryWeekRestMap = value; OnPropertyChanged(nameof(QueryWeekRestMap)); } }

        public DateTime QueryDate { get => _queryDate; set { if (_queryDate != value) { _queryDate = value; OnPropertyChanged(nameof(QueryDate)); OnPropertyChanged(nameof(QueryIsDoubleRest)); } } }
        private DateTime _queryDate;
        public bool QueryIsDoubleRest => _service.IsDoubleRestWeek(QueryDate);
        public string QueryResultText { get => _queryResultText; private set { if (_queryResultText != value) { _queryResultText = value; OnPropertyChanged(nameof(QueryResultText)); } } }
        private string _queryResultText = string.Empty;

        public ICommand QueryCommand => new RelayCommand(_ => Query());
        public ICommand TodayCommand => new RelayCommand(_ => { QueryDate = Today; Query(); });
        public ICommand OpenSettingsCommand => new RelayCommand(_ => OpenSettings());

        private void UpdateToday()
        {
            OnPropertyChanged(nameof(Today));
            OnPropertyChanged(nameof(TodayString));
            OnPropertyChanged(nameof(TodayIsDoubleRest));
            OnPropertyChanged(nameof(TodayRestText));
            UpdateWeekRestMap();
        }
        private void UpdateWeekRestMap() => WeekRestMap = _service.GetWeekRestMap(Today);

        private void Query()
        {
            var isDouble = _service.IsDoubleRestWeek(QueryDate);
            var monday = WorkweekService.GetWeekMonday(QueryDate);
            var sunday = monday.AddDays(6);
            QueryResultText = $"{QueryDate:yyyy-MM-dd dddd} → {(isDouble ? "双休周" : "单休周")}（周区间：{monday:yyyy-MM-dd} ~ {sunday:yyyy-MM-dd}）";
            QueryWeekRestMap = _service.GetWeekRestMap(QueryDate);
            OnPropertyChanged(nameof(QueryIsDoubleRest));
        }

        private void OpenSettings()
        {
            var win = new Views.SettingsWindow { Owner = Application.Current?.MainWindow, DataContext = new SettingsViewModel(_service) };
            win.ShowDialog();
            UpdateToday();
            Query();
        }
    }
}

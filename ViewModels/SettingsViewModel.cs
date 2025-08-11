using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;
using WorkweekChecker.Services;

namespace WorkweekChecker.ViewModels
{
    public class SettingsViewModel : INotifyPropertyChanged
    {
        private readonly WorkweekService _service;
        public SettingsViewModel(WorkweekService service)
        {
            _service = service;
            BaseWeekAnyDate = _service.Config.BaseMonday;
            SpecialHolidays = new ObservableCollection<DateTime>(_service.Holidays.SpecialHolidays.OrderBy(d => d));
            SpecialWorkdays = new ObservableCollection<DateTime>(_service.Holidays.SpecialWorkdays.OrderBy(d => d));
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        private void OnPropertyChanged([CallerMemberName] string? name = null) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        public DateTime BaseWeekAnyDate { get => _baseWeekAnyDate; set { if (_baseWeekAnyDate != value) { _baseWeekAnyDate = value; OnPropertyChanged(); } } }
        private DateTime _baseWeekAnyDate;
        public ObservableCollection<DateTime> SpecialHolidays { get; }
        public ObservableCollection<DateTime> SpecialWorkdays { get; }
        public DateTime? NewHoliday { get => _newHoliday; set { if (_newHoliday != value) { _newHoliday = value; OnPropertyChanged(); } } }
        private DateTime? _newHoliday;
        public DateTime? NewWorkday { get => _newWorkday; set { if (_newWorkday != value) { _newWorkday = value; OnPropertyChanged(); } } }
        private DateTime? _newWorkday;
        public DateTime? SelectedHoliday { get => _selectedHoliday; set { if (_selectedHoliday != value) { _selectedHoliday = value; OnPropertyChanged(); } } }
        private DateTime? _selectedHoliday;
        public DateTime? SelectedWorkday { get => _selectedWorkday; set { if (_selectedWorkday != value) { _selectedWorkday = value; OnPropertyChanged(); } } }
        private DateTime? _selectedWorkday;

        public ICommand SaveBaseCommand => new RelayCommand(_ =>
        {
            _service.SetBaseByAnyDate(BaseWeekAnyDate);
            _service.Save();
            MessageBox.Show($"已保存基准周：{_service.Config.BaseMonday:yyyy-MM-dd}（周一）", "保存成功", MessageBoxButton.OK, MessageBoxImage.Information);
        });
        public ICommand ResetBaseCommand => new RelayCommand(_ =>
        {
            _service.ResetDefaultBase();
            _service.Save();
            BaseWeekAnyDate = _service.Config.BaseMonday;
        });
        public ICommand AddHolidayCommand => new RelayCommand(_ =>
        {
            if (NewHoliday.HasValue)
            {
                var d = NewHoliday.Value.Date;
                if (!SpecialHolidays.Contains(d)) SpecialHolidays.Add(d);
                Persist();
            }
        });
        public ICommand RemoveHolidayCommand => new RelayCommand(_ =>
        {
            if (SelectedHoliday.HasValue)
            {
                SpecialHolidays.Remove(SelectedHoliday.Value);
                Persist();
            }
        });
        public ICommand AddWorkdayCommand => new RelayCommand(_ =>
        {
            if (NewWorkday.HasValue)
            {
                var d = NewWorkday.Value.Date;
                if (!SpecialWorkdays.Contains(d)) SpecialWorkdays.Add(d);
                Persist();
            }
        });
        public ICommand RemoveWorkdayCommand => new RelayCommand(_ =>
        {
            if (SelectedWorkday.HasValue)
            {
                SpecialWorkdays.Remove(SelectedWorkday.Value);
                Persist();
            }
        });
        public ICommand ImportOfficialCommand => new RelayCommand(_ =>
        {
            var win = new Views.HolidayImportWindow(_service.Holidays) { Owner = Application.Current?.MainWindow };
            win.ShowDialog();
            ReloadLists();
        });
        public ICommand Apply2025Command => new RelayCommand(_ =>
        {
            OfficialHolidayProvider.Apply2025(_service.Holidays);
            ReloadLists();
            MessageBox.Show("已写入 2025 国务院官方放假与调休安排。", "完成", MessageBoxButton.OK, MessageBoxImage.Information);
        });

        private void ReloadLists()
        {
            SpecialHolidays.Clear();
            foreach (var d in _service.Holidays.SpecialHolidays.OrderBy(x => x)) SpecialHolidays.Add(d);
            SpecialWorkdays.Clear();
            foreach (var d in _service.Holidays.SpecialWorkdays.OrderBy(x => x)) SpecialWorkdays.Add(d);
        }
        private void Persist()
        {
            _service.Holidays.SpecialHolidays = new System.Collections.Generic.HashSet<DateTime>(SpecialHolidays.Select(x => x.Date));
            _service.Holidays.SpecialWorkdays = new System.Collections.Generic.HashSet<DateTime>(SpecialWorkdays.Select(x => x.Date));
            _service.Save();
        }
    }
}

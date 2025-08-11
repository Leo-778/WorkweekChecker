using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace WorkweekChecker.Services
{
    public sealed class HolidayService
    {
        public HashSet<DateTime> SpecialHolidays { get; set; } = new();
        public HashSet<DateTime> SpecialWorkdays { get; set; } = new();

        public string ConfigDir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WorkweekChecker");
        public string ConfigPath => Path.Combine(ConfigDir, "holidays.json");

        public void Load()
        {
            try
            {
                if (!File.Exists(ConfigPath))
                {
                    Directory.CreateDirectory(ConfigDir);
                    Save();
                    return;
                }
                var json = File.ReadAllText(ConfigPath);
                var doc = JsonSerializer.Deserialize<HolidayConfig>(json);
                if (doc != null)
                {
                    SpecialHolidays = new HashSet<DateTime>(doc.specialHolidays.Select(ParseDate));
                    SpecialWorkdays = new HashSet<DateTime>(doc.specialWorkdays.Select(ParseDate));
                }
            }
            catch
            {
                SpecialHolidays.Clear();
                SpecialWorkdays.Clear();
            }
        }

        public void Save()
        {
            var doc = new HolidayConfig
            {
                specialHolidays = SpecialHolidays.Select(d => d.ToString("yyyy-MM-dd")).ToList(),
                specialWorkdays = SpecialWorkdays.Select(d => d.ToString("yyyy-MM-dd")).ToList()
            };
            Directory.CreateDirectory(ConfigDir);
            var json = JsonSerializer.Serialize(doc, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(ConfigPath, json);
        }

        public bool IsSpecialHoliday(DateTime date) => SpecialHolidays.Contains(date.Date);
        public bool IsSpecialWorkday(DateTime date) => SpecialWorkdays.Contains(date.Date);

        private static DateTime ParseDate(string s) => DateTime.Parse(s).Date;

        private sealed class HolidayConfig
        {
            public List<string> specialHolidays { get; set; } = new();
            public List<string> specialWorkdays { get; set; } = new();
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace WorkweekChecker.Services
{
    public sealed class WorkweekService
    {
        private const string ConfigFileName = "Workweek.config.json";
        public WorkweekConfig Config { get; set; } = new();
        public HolidayService Holidays { get; } = new HolidayService();

        public void Load()
        {
            Holidays.Load();
            try
            {
                if (File.Exists(ConfigPath))
                {
                    var json = File.ReadAllText(ConfigPath);
                    var cfg = JsonSerializer.Deserialize<WorkweekConfig>(json);
                    if (cfg != null) Config = cfg;
                }
                else
                {
                    ResetDefaultBase();
                    Save();
                }
            }
            catch
            {
                ResetDefaultBase();
            }
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(Config, new JsonSerializerOptions { WriteIndented = true });
            Directory.CreateDirectory(AppDir);
            File.WriteAllText(ConfigPath, json);
            Holidays.Save();
        }

        public void ResetDefaultBase() => Config.BaseMonday = new DateTime(2025, 8, 4);
        public void SetBaseByAnyDate(DateTime anyDateInDoubleRestWeek) => Config.BaseMonday = GetWeekMonday(anyDateInDoubleRestWeek);

        public static DateTime GetWeekMonday(DateTime date)
        {
            int day = ((int)date.DayOfWeek + 6) % 7;
            return date.Date.AddDays(-day);
        }

        public bool IsDoubleRestWeek(DateTime date)
        {
            var monday = GetWeekMonday(date);
            var baseMon = Config.BaseMonday.Date;
            var weeksDiff = (int)Math.Floor((monday - baseMon).TotalDays / 7.0);
            return (weeksDiff % 2) == 0;
        }

        public Dictionary<DayOfWeek, bool> GetWeekRestMap(DateTime anyDateInWeek)
        {
            var map = new Dictionary<DayOfWeek, bool>
            {
                [DayOfWeek.Monday] = false, [DayOfWeek.Tuesday] = false, [DayOfWeek.Wednesday] = false,
                [DayOfWeek.Thursday] = false, [DayOfWeek.Friday] = false, [DayOfWeek.Saturday] = false, [DayOfWeek.Sunday] = false
            };
            var monday = GetWeekMonday(anyDateInWeek);
            var isDouble = IsDoubleRestWeek(anyDateInWeek);
            if (isDouble) map[DayOfWeek.Saturday] = true;
            map[DayOfWeek.Sunday] = true;

            for (int i = 0; i < 7; i++)
            {
                var day = monday.AddDays(i);
                if (Holidays.IsSpecialHoliday(day)) map[day.DayOfWeek] = true;
                if (Holidays.IsSpecialWorkday(day)) map[day.DayOfWeek] = false;
            }
            return map;
        }

        private static string AppDir => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "WorkweekChecker");
        private static string ConfigPath => Path.Combine(AppDir, ConfigFileName);
    }

    public sealed class WorkweekConfig
    {
        public DateTime BaseMonday { get; set; } = new DateTime(2025, 8, 4);
    }
}

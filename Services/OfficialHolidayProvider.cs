using System;
using System.Collections.Generic;

namespace WorkweekChecker.Services
{
    public static class OfficialHolidayProvider
    {
        public static void Apply2025(HolidayService svc)
        {
            var H = new HashSet<DateTime>();
            var W = new HashSet<DateTime>();

            H.Add(new DateTime(2025, 1, 1));
            for (var d = new DateTime(2025, 1, 28); d <= new DateTime(2025, 2, 4); d = d.AddDays(1)) H.Add(d);
            W.Add(new DateTime(2025, 1, 26));
            W.Add(new DateTime(2025, 2, 8));
            for (var d = new DateTime(2025, 4, 4); d <= new DateTime(2025, 4, 6); d = d.AddDays(1)) H.Add(d);
            for (var d = new DateTime(2025, 5, 1); d <= new DateTime(2025, 5, 5); d = d.AddDays(1)) H.Add(d);
            W.Add(new DateTime(2025, 4, 27));
            for (var d = new DateTime(2025, 5, 31); d <= new DateTime(2025, 6, 2); d = d.AddDays(1)) H.Add(d);
            for (var d = new DateTime(2025, 10, 1); d <= new DateTime(2025, 10, 8); d = d.AddDays(1)) H.Add(d);
            W.Add(new DateTime(2025, 9, 28));
            W.Add(new DateTime(2025, 10, 11));

            svc.SpecialHolidays = H;
            svc.SpecialWorkdays = W;
            svc.Save();
        }
        public static void Apply2026(HolidayService svc)
        {
            throw new InvalidOperationException("2026 年节假日官方安排尚未发布。");
        }
    }
}

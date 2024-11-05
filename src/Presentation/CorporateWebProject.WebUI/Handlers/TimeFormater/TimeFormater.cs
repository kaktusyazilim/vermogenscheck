namespace CorporateWebProject.WebUI.Handlers.TimeFormater
{
    public static class TimeFormatter
    {
        public static string TimeAgo(DateTime createDate)
        {
            TimeSpan timeSpan = DateTime.Now - createDate;

            if (timeSpan.TotalMinutes < 1)
                return "birkaç saniye önce";
            if (timeSpan.TotalMinutes < 60)
                return $"{(int)timeSpan.TotalMinutes} dakika önce";
            if (timeSpan.TotalHours < 24)
                return $"{(int)timeSpan.TotalHours} saat önce";
            if (timeSpan.TotalDays < 7)
                return $"{(int)timeSpan.TotalDays} gün önce";
            if (timeSpan.TotalDays < 30)
                return $"{(int)(timeSpan.TotalDays / 7)} hafta önce";
            if (timeSpan.TotalDays < 365)
                return $"{(int)(timeSpan.TotalDays / 30)} ay önce";

            return $"{(int)(timeSpan.TotalDays / 365)} yıl önce";
        }

        public static void Main()
        {
            // Test tarihleri
            DateTime[] testDates = {
            DateTime.Now.AddMinutes(-10),
            DateTime.Now.AddHours(-2),
            DateTime.Now.AddDays(-1),
            DateTime.Now.AddDays(-3),
            DateTime.Now.AddDays(-10),
            DateTime.Now.AddMonths(-1),
            DateTime.Now.AddMonths(-6),
            DateTime.Now.AddYears(-1),
            DateTime.Now.AddYears(-2)
        };

            foreach (var date in testDates)
            {
                Console.WriteLine($"Tarih: {date}, Geçen Süre: {TimeAgo(date)}");
            }
        }
    }
}

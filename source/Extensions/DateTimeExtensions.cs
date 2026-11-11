namespace DotNetCore.Extensions;

public static class DateTimeExtensions
{
    public static List<(DateTime, DateTime)> Chunks(this DateTime startDate, DateTime endDate, int days)
    {
        endDate = endDate.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

        var chunks = new List<(DateTime, DateTime)>();

        var currentDate = startDate;

        while (currentDate <= endDate)
        {
            var chunkStartDate = currentDate;

            var chunkEndDate = currentDate.AddDays(days).AddHours(23).AddMinutes(59).AddSeconds(59);

            if (chunkEndDate > endDate) chunkEndDate = endDate;

            chunks.Add((chunkStartDate, chunkEndDate));

            currentDate = chunkEndDate.AddSeconds(1);
        }

        return chunks;
    }
}

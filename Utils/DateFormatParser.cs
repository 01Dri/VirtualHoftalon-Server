using System.Globalization;

namespace VirtualHoftalon_Server.Utils;

public class DateFormatParser
{
    public static DateTime ToTimestamp(int? Day, int? Month, int? Year, string? Hour)
    {
        string dateStr =
            $"{Day.Value.ToString("D2")}/{Month.Value.ToString("D2")}/{Year} {Hour}";
        return DateTime.ParseExact(dateStr, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
    }
}
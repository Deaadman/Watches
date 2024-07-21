namespace Watches.Utilities;

internal static class TimeUtilities
{
    internal static string ConvertTo12HourFormat(int hour24, int minutes)
    {
        var hour12 = hour24 % 12;
        hour12 = hour12 == 0 ? 12 : hour12;
        var amPm = hour24 < 12 ? "AM" : "PM";
        return $"{hour12}:{minutes:D2} {amPm}";
    }
}
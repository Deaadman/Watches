using System.Text;

namespace Watches.Utilities;

internal static class TimeDisplayUtilities
{
    private const string GlitchDigits = "0123456789?";
    private const float GlitchInterval = 0.1f;
    private static string CurrentGlitchDisplay = "00:00";
    private static float GlitchTimer;
    
    internal static string ConvertTo12HourFormat(int hour24, int minutes)
    {
        var hour12 = hour24 % 12;
        hour12 = hour12 == 0 ? 12 : hour12;
        var amPm = hour24 < 12 ? "AM" : "PM";
        
        return hour24 == -1 ? $"??:{minutes:D2} {amPm}" : $"{hour12}:{minutes:D2} {amPm}";
    }

    internal static string GetTimeDisplay(int hour, int minutes, bool is12HourFormat, bool isAuroraActive)
    {
        if (isAuroraActive) return GetGlitchyTimeDisplay(is12HourFormat);
        return is12HourFormat ? ConvertTo12HourFormat(hour, minutes) : $"{hour:D2}:{minutes:D2}";
    }

    private static string GetGlitchyTimeDisplay(bool is12HourFormat)
    {
        GlitchTimer += Time.deltaTime;
        if (GlitchTimer >= GlitchInterval)
        {
            GlitchTimer = 0f;
            CurrentGlitchDisplay = GenerateGlitchyTime(is12HourFormat);
        }
        return CurrentGlitchDisplay;
    }

    private static string GenerateGlitchyTime(bool is12HourFormat)
    {
        var sb = new StringBuilder();
        
        for (var i = 0; i < 5; i++)
        {
            sb.Append(i == 2 ? ':' : GlitchDigits[UnityEngine.Random.Range(0, GlitchDigits.Length)]);
        }

        if (is12HourFormat) sb.Append(UnityEngine.Random.value > 0.5f ? " AM" : " PM");

        return sb.ToString();
    }
}
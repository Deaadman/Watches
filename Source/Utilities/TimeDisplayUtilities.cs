using System.Text;
using Random = UnityEngine.Random;

namespace Watches.Utilities;

internal static class TimeDisplayUtilities
{
    private const string GlitchDigits = "0123456789?";
    private static float s_GlitchTimer = 0f;
    private const float GlitchInterval = 0.1f;
    private static string s_CurrentGlitchDisplay = "00:00";
    
    internal static string ConvertTo12HourFormat(int hour24, int minutes)
    {
        var hour12 = hour24 % 12;
        hour12 = hour12 == 0 ? 12 : hour12;
        var amPm = hour24 < 12 ? "AM" : "PM";
        
        return minutes == -1 ? $"{hour12}:?? {amPm}" : $"{hour12}:{minutes:D2} {amPm}";
    }

    internal static string GetTimeDisplay(int hour, int minutes, bool is12HourFormat, bool isAuroraActive)
    {
        if (isAuroraActive) return GetGlitchyTimeDisplay(is12HourFormat);
        return is12HourFormat ? ConvertTo12HourFormat(hour, minutes) : $"{hour:D2}:{minutes:D2}";
    }

    private static string GetGlitchyTimeDisplay(bool is12HourFormat)
    {
        s_GlitchTimer += Time.deltaTime;
        if (s_GlitchTimer >= GlitchInterval)
        {
            s_GlitchTimer = 0f;
            s_CurrentGlitchDisplay = GenerateGlitchyTime(is12HourFormat);
        }
        return s_CurrentGlitchDisplay;
    }

    private static string GenerateGlitchyTime(bool is12HourFormat)
    {
        var sb = new StringBuilder();
        
        for (var i = 0; i < 5; i++)
        {
            sb.Append(i == 2 ? ':' : GlitchDigits[Random.Range(0, GlitchDigits.Length)]);
        }

        if (is12HourFormat) sb.Append(Random.value > 0.5f ? " AM" : " PM");

        return sb.ToString();
    }
}
namespace Watches.Utilities;

internal static class GlobalVariables
{
    internal const string m_SundialGearName = "GEAR_Sundial";
    internal const string m_StopwatchGearName = "GEAR_Stopwatch";
    internal const string m_AnalogWatchGearName = "GEAR_AnalogWatch";
    internal const string m_DigitalWatchGearName = "GEAR_DigitalWatch";
}

#region Enums
internal enum TimeDisplayState
{
    Full,
    Partial
}

internal enum WatchType
{
    Sundial,
    Stopwatch,
    Analog,
    Digital
}
#endregion
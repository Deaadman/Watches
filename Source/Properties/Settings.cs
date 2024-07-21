using ModSettings;

namespace Watches.Properties;

internal class Settings : JsonModSettings
{
    internal static Settings Instance { get; } = new();

    [Name("12-Hour Time")] [Description("Change the default time format from 24 to 12 hour time.")]
    public bool TwelveHourTime = false;
    
    internal static void OnLoad()
    {
        Instance.AddToModSettings(BuildInfo.GUIName);
        Instance.RefreshGUI();
    }
}
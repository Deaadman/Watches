using ModSettings;

namespace Watches.Properties;

internal class Settings : JsonModSettings
{
    internal static Settings Instance { get; } = new();

    [Name("Digital Time Format")] [Description("Placeholder.")]
    public bool DigitalTimeFormat = false;
    
    internal static void OnLoad()
    {
        Instance.AddToModSettings(BuildInfo.GUIName);
        Instance.RefreshGUI();
    }
}
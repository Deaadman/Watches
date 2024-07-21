using LocalizationUtilities;
using Watches.Properties;

namespace Watches;

internal sealed class Mod : MelonMod
{
    public override void OnInitializeMelon()
    {
        RegisterLocalizationKeys("Watches.Resources.Localization.json");
        Settings.OnLoad();
    }
    
    private static void RegisterLocalizationKeys(string jsonFilePath)
    {
        if (string.IsNullOrWhiteSpace(jsonFilePath)) throw new ArgumentNullException(nameof(jsonFilePath));

        using var stream = Assembly.GetExecutingAssembly().GetManifestResourceStream(jsonFilePath);
        if (stream == null) throw new FileNotFoundException($"Resource not found: {jsonFilePath}");

        using var reader = new StreamReader(stream);
        var jsonText = reader.ReadToEnd();

        if (string.IsNullOrWhiteSpace(jsonText)) throw new InvalidDataException("JSON content is empty or whitespace.");

        LocalizationManager.LoadJsonLocalization(jsonText);
    }
}
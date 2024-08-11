using Watches.Properties;

namespace Watches;

internal sealed class Mod : MelonMod
{
    public override void OnInitializeMelon() => Settings.OnLoad();
}
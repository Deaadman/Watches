using Watches.Components;

namespace Watches.Patches;

internal static class TimeWidgetPatches
{
    [HarmonyPatch(nameof(TimeWidget), nameof(TimeWidget.Update))]
    private static class DisableTimeWidgetUpdate
    {
        private static bool Prefix(TimeWidget __instance)
        {
            return !SundialItem.WasTimeChecked;
        }
    }
}
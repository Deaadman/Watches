using Watches.Components;
using Watches.Enums;
using Watches.Managers;
using Watches.Utilities;

namespace Watches.Patches;

internal static class PlayerManagerPatches
{
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseInventoryItem), typeof(GearItem), typeof(bool))]
    private static class UseTimeItems
    {
        private static void Postfix(PlayerManager __instance, GearItem gi, bool suppressWeaponsErrorMessage)
        {
            if (gi == null) return;
            if (GearItemUtilities.GetSundialItem(gi) == true)
            {
                TimeManager.UseSundialItem(gi);
            }
            else if (GearItemUtilities.GetWatchItem(gi) == true && GearItemUtilities.GetWatchItem(gi).m_WatchType == WatchType.Stopwatch)
            {
                TimeManager.UseWatchItem(gi);
            }
        }
    }
}
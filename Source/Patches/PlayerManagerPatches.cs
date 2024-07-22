using Watches.Managers;

namespace Watches.Patches;

internal static class PlayerManagerPatches
{
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseInventoryItem), typeof(GearItem), typeof(bool))]
    private static class UseTimeItems
    {
        private static void Postfix(PlayerManager __instance, GearItem gi, bool suppressWeaponsErrorMessage)
        {
            TimeManager.UseAndGetItems(gi);
        }
    }
}
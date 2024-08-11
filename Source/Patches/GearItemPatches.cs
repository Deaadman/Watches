using Watches.Components;
using Watches.Enums;
using Watches.Managers;
using Watches.Utilities;

namespace Watches.Patches;

internal static class GearItemPatches
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    private static class AssignComponentsToGearItems
    {
        private static void Postfix(ref GearItem __instance)
        {
            if (__instance.name.Contains("GEAR_Sundial"))
            {
                _ = __instance.GetComponent<SundialItem>() ?? __instance.gameObject.AddComponent<SundialItem>();
            }
            else if (__instance.name.Contains("GEAR_Stopwatch"))
            {
                ComponentManager.SetupWatchItem(__instance, WatchType.Stopwatch);
            }
            else if (__instance.name.Contains("GEAR_AnalogWatch"))
            {
                ComponentManager.SetupWatchItem(__instance, WatchType.Analog);
            }
            else if (__instance.name.Contains("GEAR_DigitalWatch"))
            {
                ComponentManager.SetupWatchItem(__instance, WatchType.Digital);
            }
        }
    }
    
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Deserialize))]
    private static class DeserializeWatchItemData
    {
        private static void Postfix(GearItem __instance) => GearItemUtilities.GetGearItemComponent<WatchItem>(__instance)?.Deserialize();
    }
    
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.ManualUpdate))]
    private static class RechargeDigitalWatchItem
    {
        private static void Postfix(GearItem __instance) => GearItemUtilities.GetGearItemComponent<WatchItem>(__instance)?.Recharge();
    }
    
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Serialize))]
    private static class SerializeWatchItemData
    {
        private static void Postfix(GearItem __instance) => GearItemUtilities.GetGearItemComponent<WatchItem>(__instance)?.Serialize();
    }
}
using Watches.Components;

namespace Watches;

// All these methods will be moved eventually, just all here for convenience while setting logic up.
internal sealed class Mod : MelonMod
{
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal static class AssignComponentsToTimeItems
    {
        private static void Postfix(GearItem __instance)
        {
            if (__instance.name == "GEAR_Sundial")
            {
                _ = __instance.GetComponent<SundialItem>() ?? __instance.gameObject.AddComponent<SundialItem>();
            }
            
            switch (__instance.name)
            {
                case "GEAR_Stopwatch":
                    ComponentManager.SetupWatchItem(__instance, WatchItem.WatchType.Stopwatch);
                    break;
                case "GEAR_AnalogWatch":
                    ComponentManager.SetupWatchItem(__instance, WatchItem.WatchType.Analog);
                    break;
                case "GEAR_DigitalWatch":
                    ComponentManager.SetupWatchItem(__instance, WatchItem.WatchType.Digital);
                    break;
            }
        }
    }
    
    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Start))]
    internal static class AttachDisplayTimeComponent
    {
        private static void Postfix(TimeWidget __instance)
        {
            _ = __instance.GetComponent<DisplayTime>() ?? __instance.gameObject.AddComponent<DisplayTime>();
        }
    }
    
    // This throws errors, whereas Prefix doesn't
    // Don't know why.
    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Update))]
    internal static class Testing
    {
        private static bool Postfix(TimeWidget __instance)
        {
            return !SundialItem.IsPaused;
        }
    }
    
    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseInventoryItem), typeof(GearItem), typeof(bool))]
    internal static class UseTimeItems
    {
        private static void Postfix(PlayerManager __instance, GearItem gi, bool suppressWeaponsErrorMessage)
        {
            if (gi == null) return;

            if (gi.GetComponent<SundialItem>() == true)
            {
                TimeManager.UseSundialItem(gi);
                return;
            }
            if (gi.GetComponent<WatchItem>() == true && gi.GetComponent<WatchItem>().m_WatchType == WatchItem.WatchType.Stopwatch)
            {
                TimeManager.UseWatchItem(gi);
            }
        }
    }
}
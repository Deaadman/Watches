using Watches.Components;

namespace Watches;

internal class InitializeComponents
{
    //static bool UseWatchInventoryItem(GearItem gearItem)
    //{
    //    if (gearItem.gameObject.name == "GEAR_Stopwatch")
    //    {
    //        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), 5f, 0f, 0f, null, null, true, true, new OnExitDelegate(GameManager.GetPlayerManagerComponent().OnSmashComplete));
    //    }
    //    return !InterfaceManager.GetPanel<Panel_Inventory>().IsEnabled();
    //}

    //[HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseInventoryItem))]
    //internal class Testing
    //{
    //    static void Postfix(PlayerManager __instance, GearItem gi)
    //    {
    //        UseWatchInventoryItem(gi);
    //    }
    //}

    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Start))]
    internal class AttachDigitalTime
    {
        static void Postfix(TimeWidget __instance)
        {
            _ = __instance.GetComponent<DisplayTime>() ?? __instance.gameObject.AddComponent<DisplayTime>();
        }
    }
}
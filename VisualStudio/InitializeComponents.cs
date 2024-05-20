using Watches.Components;
using Watches.Utilities;

namespace Watches;

internal class InitializeComponents
{   
    [HarmonyPatch(typeof(GearItem), nameof(GearItem.Awake))]
    internal class AttachWatchItemComponent
    {
        static void Postfix(GearItem __instance)
        {
            if (__instance.gameObject.name == GlobalVariables.m_SundialGearName)
            {
                WatchItem watchItemComponent = __instance.GetComponent<WatchItem>() ?? __instance.gameObject.AddComponent<WatchItem>();
                watchItemComponent.m_WatchType = WatchType.Sundial;
            }
            if (__instance.gameObject.name == GlobalVariables.m_StopwatchGearName)
            {
                WatchItem watchItemComponent = __instance.GetComponent<WatchItem>() ?? __instance.gameObject.AddComponent<WatchItem>();
                watchItemComponent.m_WatchType = WatchType.Stopwatch;
            }
            if (__instance.gameObject.name == GlobalVariables.m_AnalogWatchGearName)
            {
                WatchItem watchItemComponent = __instance.GetComponent<WatchItem>() ?? __instance.gameObject.AddComponent<WatchItem>();
                watchItemComponent.m_WatchType = WatchType.Analog;
            }
            if (__instance.gameObject.name == GlobalVariables.m_DigitalWatchGearName)
            {
                WatchItem watchItemComponent = __instance.GetComponent<WatchItem>() ?? __instance.gameObject.AddComponent<WatchItem>();
                watchItemComponent.m_WatchType = WatchType.Digital;
            }
        }
    }

    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Start))]
    internal class AttachDigitalTimeComponent
    {
        static void Postfix(TimeWidget __instance)
        {
            _ = __instance.GetComponent<DisplayTime>() ?? __instance.gameObject.AddComponent<DisplayTime>();
        }
    }
}
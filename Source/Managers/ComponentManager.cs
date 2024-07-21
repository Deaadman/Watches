using Watches.Components;
using Watches.Enums;

namespace Watches.Managers;

internal static class ComponentManager
{
    internal static void SetupWatchItem(GearItem gearItem, WatchType watchType)
    {
        var watchItemComponent = gearItem.GetComponent<WatchItem>() ?? gearItem.gameObject.AddComponent<WatchItem>();
        watchItemComponent.m_WatchType = watchType;
    }

    internal static void UseTimeItem(int seconds, GearItem gearItem, Action<bool, bool, float> completedMethod)
    {
        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), seconds,
            0, 0, "", "", false, false, completedMethod);
        gearItem.Degrade(1);
    }
}
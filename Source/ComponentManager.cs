using Watches.Components;

namespace Watches;

internal static class ComponentManager
{
    internal static void SetupWatchItem(GearItem gearItem, WatchItem.WatchType watchType)
    {
        if (gearItem.GetComponent<WatchItem>() == null) return;
            
        var watchItemComponent = gearItem.gameObject.AddComponent<WatchItem>();
        watchItemComponent.m_WatchType = watchType;
    }

    internal static void UseTimeItem(int seconds, GearItem gearItem, Action<bool, bool, float> completedMethod)
    {
        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), seconds,
            0, 0, "", "", false, false, completedMethod);
        gearItem.Degrade(1);
    }
}
using Watches.Components;
using Watches.Enums;

namespace Watches.Managers;

internal static class ComponentManager
{
    internal static void SetupWatchItem(GearItem gearItem, WatchType watchType)
    {
        var watchItemComponent = gearItem.GetComponent<WatchItem>() ?? gearItem.gameObject.AddComponent<WatchItem>();
        watchItemComponent.WatchType = watchType;
    }
}
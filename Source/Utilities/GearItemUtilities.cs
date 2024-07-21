using Watches.Components;

namespace Watches.Utilities;

internal static class GearItemUtilities
{
    internal static WatchItem GetWatchItem(GearItem gearItem) => gearItem.GetComponent<WatchItem>();
}
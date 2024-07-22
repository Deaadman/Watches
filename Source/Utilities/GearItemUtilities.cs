using Watches.Components;
using Watches.Enums;

namespace Watches.Utilities;

internal static class GearItemUtilities
{
    internal static T GetGearItemComponent<T>(GearItem gearItem) where T : Component => gearItem.GetComponent<T>();
    
    internal static WatchType GetCurrentlyWornWatchType()
    {
        var accessoryGearItem = GameManager.GetPlayerManagerComponent().GetClothingInSlot(ClothingRegion.Accessory, ClothingLayer.Base);
        if (accessoryGearItem?.GetComponent<WatchItem>() is null)
        {
            DisplayTime.GetInstance().DigitalTimeLabel.gameObject.SetActive(false);
            return WatchType.None;
        }
        
        var watchItem = accessoryGearItem.GetComponent<WatchItem>();
        return watchItem.WatchType;
    }
}
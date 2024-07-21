using Watches.Components;
using Watches.Enums;

namespace Watches.Utilities;

internal static class GearItemUtilities
{
    internal static WatchItem GetWatchItem(GearItem gearItem) => gearItem.GetComponent<WatchItem>();

    internal static SundialItem GetSundialItem(GearItem gearItem) => gearItem.GetComponent<SundialItem>();
    
    internal static WatchType GetCurrentlyWornWatchType()
    {
        var accessoryGearItem = GameManager.GetPlayerManagerComponent().GetClothingInSlot(ClothingRegion.Accessory, ClothingLayer.Base);
        if (accessoryGearItem?.GetComponent<WatchItem>() is null)
        {
            DisplayTime.GetInstance().m_DigitalTimeLabel.gameObject.SetActive(false);
            return WatchType.None;
        }
        
        var watchItem = accessoryGearItem.GetComponent<WatchItem>();
        return watchItem.m_WatchType;
    }
}
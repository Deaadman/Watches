namespace Watches.Utilities;

internal class InventoryUtilities
{
    internal static bool IsInInventory(string gearItemGameObjectPrefab)
    {
        Inventory inventoryInstance = GameManager.GetInventoryComponent();

        for (int i = 0; i < inventoryInstance.m_Items.Count; i++)
        {
            GearItem gearItem = inventoryInstance.m_Items[i];
            if (gearItem.gameObject.name == gearItemGameObjectPrefab)
            {
                return true;
            }
        }

        return false;
    }
}
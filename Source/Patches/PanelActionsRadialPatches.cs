using Watches.Managers;

namespace Watches.Patches;

internal static class PanelActionsRadialPatches
{
    [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.ShowNavigationRadial))]
    public class AddItemsToNavigationRadial
    {
        public static void Prefix(Panel_ActionsRadial __instance)
        {
            var newOrder = new List<string>(__instance.m_NavigationRadialOrder);
            newOrder.RemoveAll(item => item is "GEAR_Stopwatch" or "GEAR_Sundial");
            
            if (GameManager.GetInventoryComponent().GetHighestConditionGearThatMatchesName("GEAR_Stopwatch") != null)
            {
                newOrder.Insert(5, "GEAR_Stopwatch");
            }
            else if (GameManager.GetInventoryComponent().GetHighestConditionGearThatMatchesName("GEAR_Sundial") != null)
            {
                newOrder.Insert(5, "GEAR_Sundial");
            }
            
            __instance.m_NavigationRadialOrder = newOrder.ToArray();
        }
    }
    
    [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.UseItem))]
    public class UseCustomNavigationalItems
    {
        public static void Postfix(Panel_ActionsRadial __instance, GearItem gi) => TimeManager.UseAndGetItems(gi);
    }
}
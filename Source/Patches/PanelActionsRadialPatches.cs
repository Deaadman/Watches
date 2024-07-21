namespace Watches.Patches;

internal static class PanelActionsRadialPatches
{
    // The following below doesn't work, but you get the idea - add both of these somewhere on the radial menu.
    
    // [HarmonyPatch(typeof(Panel_ActionsRadial), nameof(Panel_ActionsRadial.Initialize))]
    // private static class Tes 
    // {
    //     private static void Postfix(Panel_ActionsRadial __instance)
    //     {
    //         __instance.m_NavigationRadialOrder.AddItem("GEAR_Sundial");
    //         __instance.m_NavigationRadialOrder.AddItem("GEAR_Stopwatch");
    //     }
    // }
}
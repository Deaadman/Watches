using Watches.Utilities;

namespace Watches;

internal class InitializeComponents
{
    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Start))]
    internal class AttachDigitalTime
    {
        static void Postfix(TimeWidget __instance)
        {
            _ = __instance.GetComponent<DisplayTime>() ?? __instance.gameObject.AddComponent<DisplayTime>();

            UserInterfaceUtilities.UISpriteAlphas(__instance.gameObject.GetComponentsInChildren<UISprite>(), 0f);
        }
    }

    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Update))]
    internal class HideDigitalTime
    {
        static void Postfix(TimeWidget __instance)
        {            
            float alphaAmount = InventoryUtilities.IsInInventory("GEAR_Sundial") ? 1f : 0f;
            UserInterfaceUtilities.UISpriteAlphas(__instance.gameObject.GetComponentsInChildren<UISprite>(), alphaAmount);

            UILabel digitalTimeUILabel = __instance.GetComponent<DisplayTime>().m_LabelDisplayTime;
            digitalTimeUILabel.alpha = InventoryUtilities.IsInInventory("GEAR_Stopwatch") ? 0f : 1f;
            digitalTimeUILabel.alpha = InventoryUtilities.IsInInventory("GEAR_AnalogWatch") ? 0f : 1f;
            digitalTimeUILabel.alpha = InventoryUtilities.IsInInventory("GEAR_DigitalWatch") ? 0f : 1f;
        }
    }
}
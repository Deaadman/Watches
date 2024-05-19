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
            DisplayTime displayTimeComponent = __instance.GetComponent<DisplayTime>();

            float alphaAmount = InventoryUtilities.IsInInventory("GEAR_Sundial") ? 1f : 0f;
            UserInterfaceUtilities.UISpriteAlphas(__instance.gameObject.GetComponentsInChildren<UISprite>(), alphaAmount);

            // Not displaying time when in inventory, my guess is it's something to do with formatting in DisplayTime.cs
            displayTimeComponent.m_LabelDisplayTime.alpha = InventoryUtilities.IsInInventory("GEAR_Stopwatch") ? 1f : 0f;
            displayTimeComponent.m_TimeDisplayState = InventoryUtilities.IsInInventory("GEAR_Stopwatch") ? DisplayTime.TimeDisplayState.Partial : DisplayTime.TimeDisplayState.Full;

            // Not displaying time when in inventory, my guess is it's something to do with formatting in DisplayTime.cs
            displayTimeComponent.m_LabelDisplayTime.alpha = InventoryUtilities.IsInInventory("GEAR_AnalogWatch") ? 1f : 0f;
            displayTimeComponent.m_TimeDisplayState = InventoryUtilities.IsInInventory("GEAR_AnalogWatch") ? DisplayTime.TimeDisplayState.Partial : DisplayTime.TimeDisplayState.Full;

            displayTimeComponent.m_LabelDisplayTime.alpha = InventoryUtilities.IsInInventory("GEAR_DigitalWatch") ? 1f : 0f;
            displayTimeComponent.m_TimeDisplayState = InventoryUtilities.IsInInventory("GEAR_DigitalWatch") ? DisplayTime.TimeDisplayState.Full : DisplayTime.TimeDisplayState.Partial;
        }
    }
}
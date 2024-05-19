using Watches.Components;
using Watches.Utilities;
using static Watches.Components.DisplayTime;

namespace Watches;

internal class InitializeComponents
{
    static bool sundialPaused = false;

    internal static void CheckingTimeFinishedStopwatch(bool success, bool playerCancel, float progress)
    {
        DisplayTime displayTimeWidget = InterfaceManager.m_TimeWidget.gameObject.GetComponent<DisplayTime>();

        displayTimeWidget.m_LabelDisplayTime.alpha = 1f;
        displayTimeWidget.m_TimeDisplayState = TimeDisplayState.Partial;
        UserInterfaceUtilities.UISpriteAlphas(displayTimeWidget.GetComponentsInChildren<UISprite>(), false);
    }

    //internal static void CheckingTimeFinishedSundial(bool success, bool playerCancel, float progress)
    //{
    //    DisplayTime displayTimeWidget = InterfaceManager.m_TimeWidget.gameObject.GetComponent<DisplayTime>();

    //    sundialPaused = true;
    //    displayTimeWidget.m_LabelDisplayTime.alpha = 0f;
    //    displayTimeWidget.m_TimeDisplayState = TimeDisplayState.Partial;
    //    UserInterfaceUtilities.UISpriteAlphas(displayTimeWidget.GetComponentsInChildren<UISprite>(), true);
    //}

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseInventoryItem))]
    internal class UseWatchItems
    {
        static void Postfix(PlayerManager __instance, GearItem gi)
        {
            // Maybe add checks if the stopwatch is frozen / low condition - then notify the player using...
            // Add checks for the Sundial depending on what time it is, if indoors etc...
            // GameAudioManager.PlayGUIError();
            // and
            // HUDMessage.AddMessage("ERROR MESSAGE HERE", true, true);

            if (gi.gameObject.name == "GEAR_Stopwatch")
            {
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), 5f, 0f, 0f, "", "", false, false, new Action<bool, bool, float>(CheckingTimeFinishedStopwatch));
            }
            //else if (gi.gameObject.name == "GEAR_Sundial")
            //{
            //    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), 5f, 0f, 0f, "", "", false, false, new Action<bool, bool, float>(CheckingTimeFinishedSundial));
            //}
        }
    }

    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Start))]
    internal class AttachDigitalTimeComponent
    {
        static void Postfix(TimeWidget __instance)
        {
            _ = __instance.GetComponent<DisplayTime>() ?? __instance.gameObject.AddComponent<DisplayTime>();
        }
    }

    //[HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Update))]
    //internal class PauseWidgetUpdates
    //{
    //    static bool Prefix(TimeWidget __instance)
    //    {
    //        return !sundialPaused;
    //    }
    //}
}
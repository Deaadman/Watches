using Watches.Components;
using Watches.Utilities;

namespace Watches;

internal class ConfigureComponents
{
    static bool SundialPaused = false;

    internal static void CheckingTimeFinishedStopwatch(bool success, bool playerCancel, float progress)
    {
        DisplayTime displayTimeWidget = InterfaceManager.m_TimeWidget.gameObject.GetComponent<DisplayTime>();

        // Something funky is going on with this m_StopwatchUsed logic.
        displayTimeWidget.m_StopwatchUsed = true;
        displayTimeWidget.m_LabelDisplayTime.alpha = 1f;
        displayTimeWidget.m_TimeDisplayState = TimeDisplayState.Partial;
        displayTimeWidget.UpdateTime();
    }

    internal static void CheckingTimeFinishedSundial(bool success, bool playerCancel, float progress)
    {
        DisplayTime displayTimeWidget = InterfaceManager.m_TimeWidget.gameObject.GetComponent<DisplayTime>();

        SundialPaused = true;
        displayTimeWidget.m_TimeDisplayState = TimeDisplayState.Partial;
        UserInterfaceUtilities.UISpriteAlphas(displayTimeWidget.GetComponentsInChildren<UISprite>(), true);
    }

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseInventoryItem))]
    internal class UseWatchItems
    {
        static void Postfix(PlayerManager __instance, GearItem gi)
        {
            // Haven't checked to see if the wornout works or not.
            if (!gi.m_WornOut)
            {
                if (gi.gameObject.name == GlobalVariables.m_StopwatchGearName)
                {
                    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), 1f, 0f, 0f, "", "", false, false, new Action<bool, bool, float>(CheckingTimeFinishedStopwatch));
                }
                if (gi.gameObject.name == GlobalVariables.m_SundialGearName)
                {
                    if (GameManager.GetWeatherComponent().IsIndoorEnvironment())
                    {
                        GameAudioManager.PlayGUIError();
                        HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CheckingSundialIndoors"), true, true);
                    }
                    else if (GameManager.GetTimeOfDayComponent().IsNight())
                    {
                        GameAudioManager.PlayGUIError();
                        HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CheckingSundialNightTime"), true, true);
                    }
                    else
                    {
                        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), 1f, 0f, 0f, "", "", false, false, new Action<bool, bool, float>(CheckingTimeFinishedSundial));
                    }
                }
            }
            else
            {
                GameAudioManager.PlayGUIError();
                HUDMessage.AddMessage(Localization.Get("GAMEPLAY_WornOut"), true, true);
            }
        }
    }

    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Update))]
    internal class PauseWidgetUpdates
    {
        static bool Prefix(TimeWidget __instance)
        {
            if (SundialPaused)
            {
                SundialPaused = false;
                return true;
            }
            return false;
        }
    }
}
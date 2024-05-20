using Watches.Components;
using Watches.Utilities;

namespace Watches;

internal class ConfigureComponents
{
    internal static bool SundialPaused = false;

    [HarmonyPatch(typeof(PlayerManager), nameof(PlayerManager.UseInventoryItem))]
    internal class UseWatchItems
    {
        static void Postfix(PlayerManager __instance, GearItem gi)
        {
            if (gi.gameObject.name == GlobalVariables.m_StopwatchGearName)
            {
                WatchItem.m_IsStopwatch = true;
                InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), 1f, 0f, 0f, "", "", false, false, new Action<bool, bool, float>(WatchItem.CompletedCheckingTime));
                gi.Degrade(1f);
            }
            if (gi.gameObject.name == GlobalVariables.m_SundialGearName)
            {
                WeatherStage getWeatherStages = GameManager.GetWeatherComponent().GetWeatherStage();
                if (GameManager.GetWeatherComponent().IsIndoorEnvironment())
                {
                    GameAudioManager.PlayGUIError();
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CheckingSundialIndoors"), true, true);
                }
                if (getWeatherStages == WeatherStage.Blizzard || getWeatherStages == WeatherStage.HeavySnow || getWeatherStages == WeatherStage.DenseFog || getWeatherStages == WeatherStage.Cloudy)
                {
                    GameAudioManager.PlayGUIError();
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CheckingSundialWeather"), true, true);
                }
                else if (GameManager.GetTimeOfDayComponent().IsNight())
                {
                    GameAudioManager.PlayGUIError();
                    HUDMessage.AddMessage(Localization.Get("GAMEPLAY_CheckingSundialNightTime"), true, true);
                }
                else
                {
                    WatchItem.m_IsStopwatch = false;
                    InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), 5f, 0f, 0f, "", "", false, false, new Action<bool, bool, float>(WatchItem.CompletedCheckingTime));
                    gi.Degrade(1f);
                }
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
using Watches.Components;
using Watches.Enums;
using Watches.Utilities;

namespace Watches.Managers;

internal static class TimeManager
{
    private static void StopWatchUnusableError(string localizationKey)
    {
        GameAudioManager.PlayGUIError();
        HUDMessage.AddMessage(Localization.Get(localizationKey), true, true);
    }
    
    internal static void UseAndGetItems(GearItem gearItem)
    {
        if (gearItem == null) return;
        if (GearItemUtilities.GetGearItemComponent<SundialItem>(gearItem)) UseSundialItem(gearItem);
        if (GearItemUtilities.GetGearItemComponent<WatchItem>(gearItem) && GearItemUtilities.GetGearItemComponent<WatchItem>(gearItem).WatchType == WatchType.Stopwatch) UseWatchItem(gearItem);
    }
    
    private static void UseTimeItem(int seconds, GearItem gearItem, Action<bool, bool, float> completedMethod)
    {
        InterfaceManager.GetPanel<Panel_GenericProgressBar>().Launch(Localization.Get("GAMEPLAY_CheckingTime"), seconds,
            0, 0, "", "", false, false, completedMethod);
        gearItem.Degrade(1);
    }
    
    private static void UseSundialItem(GearItem gearItem)
    {
        if (GameManager.GetWeatherComponent().IsIndoorEnvironment())
        {
            StopWatchUnusableError("GAMEPLAY_CheckingSundialIndoors");
        }
        else if (GameManager.GetWeatherComponent().GetWeatherStage() is WeatherStage.Blizzard or WeatherStage.HeavySnow or WeatherStage.DenseFog)
        {
            StopWatchUnusableError("GAMEPLAY_CheckingSundialWeather");
        }
        else if (GameManager.GetTimeOfDayComponent().IsNight())
        {
            StopWatchUnusableError("GAMEPLAY_CheckingSundialNightTime");
        }
        else
        {
            WatchItem.WasTimeChecked = false;
            UseTimeItem(2, gearItem, SundialItem.TimeChecked);
        }
    }

    private static void UseWatchItem(GearItem gearItem)
    {
        SundialItem.WasTimeChecked = false;
        UseTimeItem(1, gearItem, GearItemUtilities.GetGearItemComponent<WatchItem>(gearItem).TimeChecked);
    }
}
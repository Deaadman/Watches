using Watches.Components;

namespace Watches.Managers;

internal static class TimeManager
{
    private static void StopWatchUnusableError(string localizationKey)
    {
        GameAudioManager.PlayGUIError();
        HUDMessage.AddMessage(Localization.Get(localizationKey), true, true);
    }
    
    internal static void UseSundialItem(GearItem gearItem)
    {
        if (GameManager.GetWeatherComponent().IsIndoorEnvironment())
        {
            StopWatchUnusableError("GAMEPLAY_CheckingSundialIndoors");
        }
        if (GameManager.GetWeatherComponent().GetWeatherStage() is WeatherStage.Blizzard or WeatherStage.HeavySnow or WeatherStage.DenseFog)
        {
            StopWatchUnusableError("GAMEPLAY_CheckingSundialWeather");
        }
        else if (GameManager.GetTimeOfDayComponent().IsNight())
        {
            StopWatchUnusableError("GAMEPLAY_CheckingSundialNightTime");
        }
        else
        {
            ComponentManager.UseTimeItem(1, gearItem, SundialItem.TimeChecked);
        }
    }

    internal static void UseWatchItem(GearItem gearItem)
    {
        ComponentManager.UseTimeItem(1, gearItem, WatchItem.TimeChecked);
    }
}
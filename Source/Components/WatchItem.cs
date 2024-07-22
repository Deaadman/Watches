using Watches.Enums;
using Watches.Managers;
using Watches.Properties;
using Watches.Utilities;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class WatchItem : MonoBehaviour
{
    internal static bool WasTimeChecked;
    private const float BatteryDepletionRate = 10f; // Higher number = slower depletion
    private const float BatteryRechargeRate = 1f; // Lower number = faster recharge
    private const float ConditionThreshold = 20f;
    private const float FrozenThreshold = 80f;
    private float CurrentBatteryCharge;
    
    private Color FrozenColour;
    private DisplayTime DisplayTime;
    private GearItem GearItem;
    private TimeOfDay TimeOfDay;
    internal WatchType WatchType;
    
    private void Awake()
    {
        DisplayTime = DisplayTime.GetInstance();
        TimeOfDay = GameManager.GetTimeOfDayComponent();
        GearItem = GetComponent<GearItem>();
        FrozenColour = InterfaceManager.GetPanel<Panel_Clothing>().m_ItemDescriptionPage.m_FrozenStatusColor;
        
        if (WatchType == WatchType.Digital) CurrentBatteryCharge = UnityEngine.Random.Range(0f, 1f);
    }
    
    private string GetDigitalTimeDisplay()
    {
        if (CurrentBatteryCharge == 0f || GearItem.m_CurrentHP == 0f) return "??:??";
        
        var hour = TimeOfDay.GetHour();
        var minutes = TimeOfDay.GetMinutes();
        var isAuroraActive = GameManager.GetAuroraManager().AuroraIsActive();
        
        if (GearItem.m_CurrentHP <= ConditionThreshold && !isAuroraActive) return Settings.Instance.TwelveHourTime ? TimeDisplayUtilities.ConvertTo12HourFormat(-1, minutes) : $"??:{minutes:D2}";
        
        return TimeDisplayUtilities.GetTimeDisplay(hour, minutes, Settings.Instance.TwelveHourTime, isAuroraActive);
    }
    
    internal void Deserialize()
    {
        var loadedData = DataManager.LoadData<float>("DigitalWatchBattery");
        if (loadedData.HasValue)
        {
            CurrentBatteryCharge = loadedData.Value;
        }
    }
    
    internal void Serialize() => DataManager.SaveData(CurrentBatteryCharge, "DigitalWatchBattery");
    
    internal void Recharge()
    {
        var todHours = TimeOfDay.GetTODHours(Time.deltaTime);
        if (GameManager.GetAuroraManager().AuroraIsActive()) CurrentBatteryCharge += todHours / BatteryRechargeRate;
    }

    internal void TimeChecked(bool arg1, bool arg2, float arg3)
    {
        UpdateAnalogTime();
        WasTimeChecked = true;
    }

    internal void UpdateAnalogTime()
    {
        if (GearItem.m_ClothingItem is not null && GearItem.m_ClothingItem.m_PercentFrozen >= FrozenThreshold)
        {
            DisplayTime.HourHandSprite.color = FrozenColour;
            DisplayTime.MinuteHandSprite.color = FrozenColour;
            return;
        }
        
        DisplayTime.MinuteHandSprite.color = Color.white;

        var minute = TimeOfDay.GetMinutes();
        var minuteAngle = minute / 60f * 360f - 90f;
        DisplayTime.MinuteHandSprite.transform.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
        DisplayTime.HourHandSprite.color = Color.red;

        if (!(GearItem.m_CurrentHP > ConditionThreshold)) return;
        
        var hour = TimeOfDay.GetHour() + minute / 60f;
        var hourAngle = hour % 12 / 12f * 360f - 90f;
        DisplayTime.HourHandSprite.transform.localRotation = Quaternion.Euler(0, 0, -hourAngle);
        DisplayTime.HourHandSprite.color = Color.white;
    }
    
    internal void UpdateDigitalTime()
    {
        if (GearItem.m_ClothingItem.m_PercentFrozen >= FrozenThreshold)
        {
            DisplayTime.DigitalTimeLabel.color = FrozenColour;
            DisplayTime.DurationObjectForegroundSprite.color = FrozenColour;
            DisplayTime.BatterySprite.color = FrozenColour;
            return;
        }
        
        DisplayTime.DigitalTimeLabel.color = Color.white;
        DisplayTime.DurationObjectForegroundSprite.color = Color.white;
        DisplayTime.BatterySprite.color = Color.white;
        
        var todHours = TimeOfDay.GetTODHours(Time.deltaTime);
        if (!GameManager.GetAuroraManager().AuroraIsActive()) CurrentBatteryCharge -= todHours / BatteryDepletionRate;
        
        CurrentBatteryCharge = Mathf.Clamp(CurrentBatteryCharge, 0f, 1f);
        DisplayTime.DurationObjectForegroundSprite.fillAmount = Mathf.Lerp(0.14f, 1f - 0.14f, CurrentBatteryCharge);
        DisplayTime.DigitalTimeLabel.text = GetDigitalTimeDisplay();
    }
}
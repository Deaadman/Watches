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
    private float _currentBatteryCharge = 1f;
    
    private Color _frozenColour;
    private DisplayTime _displayTime;
    private GearItem _gearItem;
    private TimeOfDay _timeOfDay;
    internal WatchType WatchType;
    
    private void Awake()
    {
        _displayTime = DisplayTime.GetInstance();
        _timeOfDay = GameManager.GetTimeOfDayComponent();
        _gearItem = GetComponent<GearItem>();
        _frozenColour = InterfaceManager.GetPanel<Panel_Clothing>().m_ItemDescriptionPage.m_FrozenStatusColor;
    }
    
    private string GetDigitalTimeDisplay()
    {
        if (_currentBatteryCharge == 0f || _gearItem.m_CurrentHP == 0f) return "??:??";
        
        var hour = _timeOfDay.GetHour();
        var minutes = _timeOfDay.GetMinutes();
        var isAuroraActive = GameManager.GetAuroraManager().AuroraIsActive();
        
        if (_gearItem.m_CurrentHP <= ConditionThreshold && !isAuroraActive) return Settings.Instance.TwelveHourTime ? TimeDisplayUtilities.ConvertTo12HourFormat(-1, minutes) : $"??:{minutes:D2}";
        
        return TimeDisplayUtilities.GetTimeDisplay(hour, minutes, Settings.Instance.TwelveHourTime, isAuroraActive);
    }

    internal void Deserialize()
    {
        var loadedData = DataManager.LoadData<float>("DigitalWatchBattery");
        if (loadedData.HasValue) _currentBatteryCharge = loadedData.Value;
    }
    
    internal void Serialize() => DataManager.SaveData(_currentBatteryCharge, "DigitalWatchBattery");
    
    internal void Recharge()
    {
        var todHours = _timeOfDay.GetTODHours(Time.deltaTime);
        if (GameManager.GetAuroraManager().AuroraIsActive()) _currentBatteryCharge += todHours / BatteryRechargeRate;
    }

    internal void TimeChecked(bool arg1, bool arg2, float arg3)
    {
        UpdateAnalogTime();
        WasTimeChecked = true;
    }

    internal void UpdateAnalogTime()
    {
        if (_gearItem.m_ClothingItem is not null && _gearItem.m_ClothingItem.m_PercentFrozen >= FrozenThreshold)
        {
            _displayTime.HourHandSprite.color = _frozenColour;
            _displayTime.MinuteHandSprite.color = _frozenColour;
            return;
        }
        
        _displayTime.MinuteHandSprite.color = Color.white;

        var minute = _timeOfDay.GetMinutes();
        var minuteAngle = minute / 60f * 360f - 90f;
        _displayTime.MinuteHandSprite.transform.localRotation = Quaternion.Euler(0, 0, -minuteAngle);
        _displayTime.HourHandSprite.color = Color.red;

        if (!(_gearItem.m_CurrentHP > ConditionThreshold)) return;
        
        var hour = _timeOfDay.GetHour() + minute / 60f;
        var hourAngle = hour % 12 / 12f * 360f - 90f;
        _displayTime.HourHandSprite.transform.localRotation = Quaternion.Euler(0, 0, -hourAngle);
        _displayTime.HourHandSprite.color = Color.white;
    }
    
    internal void UpdateDigitalTime()
    {
        if (_gearItem.m_ClothingItem.m_PercentFrozen >= FrozenThreshold)
        {
            _displayTime.DigitalTimeLabel.color = _frozenColour;
            _displayTime.DurationObjectForegroundSprite.color = _frozenColour;
            _displayTime.BatterySprite.color = _frozenColour;
            return;
        }
        
        _displayTime.DigitalTimeLabel.color = Color.white;
        _displayTime.DurationObjectForegroundSprite.color = Color.white;
        _displayTime.BatterySprite.color = Color.white;
        
        var todHours = _timeOfDay.GetTODHours(Time.deltaTime);
        if (!GameManager.GetAuroraManager().AuroraIsActive()) _currentBatteryCharge -= todHours / BatteryDepletionRate;
        
        _currentBatteryCharge = Mathf.Clamp(_currentBatteryCharge, 0f, 1f);
        _displayTime.DurationObjectForegroundSprite.fillAmount = Mathf.Lerp(0.14f, 1f - 0.14f, _currentBatteryCharge);
        _displayTime.DigitalTimeLabel.text = GetDigitalTimeDisplay();
    }
}
using Watches.Enums;
using Watches.Managers;
using Watches.Properties;
using Watches.Utilities;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class WatchItem : MonoBehaviour
{
    internal WatchType m_WatchType;
    private GearItem m_GearItem;
    private DisplayTime m_DisplayTime;
    private TimeOfDay m_TimeOfDay;
    
    internal static bool WasTimeChecked;
    private float m_CurrentBatteryCharge;
    private const float BatteryDepletionRate = 6f;  // Higher number = slower depletion
    private const float BatteryRechargeRate = 2f;   // Lower number = faster recharge
    
    private void Awake()
    {
        m_DisplayTime = DisplayTime.GetInstance();
        m_TimeOfDay = GameManager.GetTimeOfDayComponent();
        m_GearItem = GetComponent<GearItem>();
        
        if (m_WatchType == WatchType.Digital) m_CurrentBatteryCharge = UnityEngine.Random.Range(0f, 1f);
    }

    private string GetDigitalTimeDisplay()
    {
        if (m_CurrentBatteryCharge == 0f || m_GearItem.m_CurrentHP == 0f) return "??:??";
        
        var hour = m_TimeOfDay.GetHour();
        var minutes = m_TimeOfDay.GetMinutes();
        var isAuroraActive = GameManager.GetAuroraManager().AuroraIsActive();
        
        if (m_GearItem.m_CurrentHP <= 50f && !isAuroraActive) return Settings.Instance.TwelveHourTime ? TimeDisplayUtilities.ConvertTo12HourFormat(hour, -1) : $"{hour:D2}:??";
        
        return TimeDisplayUtilities.GetTimeDisplay(hour, minutes, Settings.Instance.TwelveHourTime, isAuroraActive);
    }
    
    internal void Deserialize()
    {
        var loadedData = DataManager.LoadData<float>("DigitalWatchBattery");
        if (loadedData.HasValue)
        {
            m_CurrentBatteryCharge = loadedData.Value;
        }
    }
    
    internal void Serialize()
    {
        DataManager.SaveData(m_CurrentBatteryCharge, "DigitalWatchBattery");
    }
    
    internal void Recharge()
    {
        var todHours = m_TimeOfDay.GetTODHours(Time.deltaTime);
        if (GameManager.GetAuroraManager().AuroraIsActive()) m_CurrentBatteryCharge += todHours / BatteryRechargeRate;
    }

    internal static void TimeChecked(bool arg1, bool arg2, float arg3)
    {
        WasTimeChecked = true;
    }

    internal void UpdateAnalogTime() { }
    
    internal void UpdateDigitalTime()
    {
        var todHours = m_TimeOfDay.GetTODHours(Time.deltaTime);
        if (!GameManager.GetAuroraManager().AuroraIsActive()) m_CurrentBatteryCharge -= todHours / BatteryDepletionRate;

        m_CurrentBatteryCharge = Mathf.Clamp(m_CurrentBatteryCharge, 0f, 1f);
        m_DisplayTime.m_ObjectDurationForegroundSprite.fillAmount = Mathf.Lerp(0.14f, 1f - 0.14f, m_CurrentBatteryCharge);
        m_DisplayTime.m_DigitalTimeLabel.text = GetDigitalTimeDisplay();
    }
}
using Watches.Enums;
using Watches.Managers;
using Watches.Properties;
using Watches.Utilities;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class WatchItem : MonoBehaviour
{
    internal WatchType m_WatchType;
    private DisplayTime m_DisplayTime;
    private TimeOfDay m_TimeOfDay;
    private float m_CurrentBatteryCharge;

    private void Awake()
    {
        m_DisplayTime = DisplayTime.GetInstance();
        m_TimeOfDay = GameManager.GetTimeOfDayComponent();

        if (m_WatchType == WatchType.Digital) m_CurrentBatteryCharge = UnityEngine.Random.Range(0f, 1f);
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
    
    internal static void TimeChecked(bool arg1, bool arg2, float arg3) { }

    internal void Update()
    {
        switch (m_WatchType)
        {
            case WatchType.Digital:
                UpdateDigitalTime();
                break;
            case WatchType.Analog:
                UpdateAnalogTime();
                break;
        }
    }

    private void UpdateAnalogTime() { }

    // This works however it only recharges when the widget is active. We will need to change this, so it recharges anywhere.
    private void UpdateDigitalTime()
    {
        var todHours = GameManager.GetTimeOfDayComponent().GetTODHours(Time.deltaTime);
        if (GameManager.GetAuroraManager().AuroraIsActive())
        {
            m_CurrentBatteryCharge += todHours / 5f;
        }
        else
        {
            m_CurrentBatteryCharge -= todHours / 10f;   
        }

        m_CurrentBatteryCharge = Mathf.Clamp(m_CurrentBatteryCharge, 0f, 1f);

        m_DisplayTime.m_ObjectDurationForegroundSprite.fillAmount = Mathf.Lerp(0.14f, 1f - 0.14f, m_CurrentBatteryCharge);

        if (Settings.Instance.TwelveHourTime)
        {
            m_DisplayTime.m_DigitalTimeLabel.text = m_CurrentBatteryCharge != 0f ? TimeUtilities.ConvertTo12HourFormat(m_TimeOfDay.GetHour(), m_TimeOfDay.GetMinutes()) : Localization.Get("GAMEPLAY_OutOfCharge");
        }
        else
        {
            m_DisplayTime.m_DigitalTimeLabel.text = m_CurrentBatteryCharge != 0f ? $"{m_TimeOfDay.GetHour():D2}:{m_TimeOfDay.GetMinutes():D2}" : Localization.Get("GAMEPLAY_OutOfCharge");
        }
    }
}
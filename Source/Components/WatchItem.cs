using Watches.Enums;
using Watches.Properties;

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
            m_CurrentBatteryCharge += todHours / 1.25f;
        }
        else
        {
            m_CurrentBatteryCharge -= todHours / 1.5f;   
        }

        m_CurrentBatteryCharge = Mathf.Clamp(m_CurrentBatteryCharge, 0f, 1f);

        m_DisplayTime.m_ObjectDurationForegroundSprite.fillAmount = Mathf.Lerp(0.14f, 1f - 0.14f, m_CurrentBatteryCharge);

        if (Settings.Instance.DigitalTimeFormat)
        {
            var hour12 = m_TimeOfDay.GetHour() % 12;
            hour12 = hour12 == 0 ? 12 : hour12;
            var amPm = m_TimeOfDay.GetHour() < 12 ? "AM" : "PM";
            m_DisplayTime.m_DigitalTimeLabel.text = m_CurrentBatteryCharge != 0f ? $"{hour12}:{m_TimeOfDay.GetMinutes():D2} {amPm}" : Localization.Get("GAMEPLAY_OutOfCharge");
        }
        else
        {
            m_DisplayTime.m_DigitalTimeLabel.text = m_CurrentBatteryCharge != 0f ? $"{m_TimeOfDay.GetHour():D2}:{m_TimeOfDay.GetMinutes():D2}" : Localization.Get("GAMEPLAY_OutOfCharge");
        }
    }
}
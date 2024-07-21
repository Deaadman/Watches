using Watches.Enums;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class WatchItem : MonoBehaviour
{
    internal WatchType m_WatchType;
    private DisplayTime m_DisplayTime;
    private TimeOfDay m_TimeOfDay;

    private void Awake()
    {
        m_DisplayTime = DisplayTime.GetInstance();
        m_TimeOfDay = GameManager.GetTimeOfDayComponent();
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
    
    private void UpdateDigitalTime() => m_DisplayTime.m_DigitalTimeLabel.text = $"{m_TimeOfDay.GetHour():D2}:{m_TimeOfDay.GetMinutes():D2}";
}
using Watches.Utilities;

namespace Watches;

[RegisterTypeInIl2Cpp(false)]
public class DisplayTime : MonoBehaviour
{

#nullable disable
    internal TimeDisplayState m_TimeDisplayState;
    TimeOfDay m_TimeOfDay;
    internal UILabel m_LabelDisplayTime;
#nullable enable

    internal enum TimeDisplayState
    {
        Full,
        Partial
    }

    void Awake() => InitializeComponents();

    void ConfigureComponents()
    {
        UserInterfaceUtilities.SetupUILabel(m_LabelDisplayTime, "", FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Automatic, UILabel.Overflow.ResizeHeight, false, 20, 20, Color.white, true);
        m_LabelDisplayTime.alpha = 0f;
    }

    void InitializeComponents()
    {
        m_TimeOfDay = GameManager.GetTimeOfDayComponent();
        m_LabelDisplayTime = UserInterfaceUtilities.SetupGameObjectUILabel("DigitalTime", transform, false, 0, -20, 0);
        ConfigureComponents();
    }

    void Update()
    {
        int currentHour = m_TimeOfDay.GetHour();
        string currentMinutes = m_TimeOfDay.GetMinutes().ToString("D2");
        m_LabelDisplayTime.text = m_TimeDisplayState == TimeDisplayState.Full ? $"{currentHour}:{currentMinutes}" : $"{currentHour}:??";
    }
}
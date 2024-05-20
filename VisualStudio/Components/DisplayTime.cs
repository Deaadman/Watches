using Watches.Utilities;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class DisplayTime : MonoBehaviour
{

#nullable disable
    PlayerManager m_PlayerManager;
    internal bool m_StopwatchUsed;
    internal TimeDisplayState m_TimeDisplayState;
    TimeOfDay m_TimeOfDay;
    internal UILabel m_LabelDisplayTime;
#nullable enable

    void Awake() => InitializeComponents();

    void ConfigureComponents()
    {
        UserInterfaceUtilities.SetupUILabel(m_LabelDisplayTime, "", FontStyle.Normal, UILabel.Crispness.Always, NGUIText.Alignment.Automatic, UILabel.Overflow.ResizeHeight, false, 20, 25, Color.white, true);
        UserInterfaceUtilities.UISpriteAlphas(GetComponentsInChildren<UISprite>(), false);
        m_LabelDisplayTime.alpha = 0f;
    }

    void InitializeComponents()
    {
        m_TimeOfDay = GameManager.GetTimeOfDayComponent();
        m_PlayerManager = GameManager.GetPlayerManagerComponent();
        m_LabelDisplayTime = UserInterfaceUtilities.SetupGameObjectUILabel("DigitalTime", transform, false, 0, 0, 0);
        ConfigureComponents();
    }

    void Update()
    {
        if (m_LabelDisplayTime.alpha == 1f && UserInterfaceUtilities.CheckUISpriteAlphas(GetComponentsInChildren<UISprite>()))
        {
            m_LabelDisplayTime.transform.localPosition = new Vector3(0, -20, 0);
        }
        else
        {
            m_LabelDisplayTime.transform.localPosition = new Vector3(0, 20, 0);
        }

        if (m_PlayerManager.IsWearingClothingName(GlobalVariables.m_DigitalWatchGearName))
        {
            m_LabelDisplayTime.alpha = 1f;
            m_TimeDisplayState = TimeDisplayState.Full;
            UpdateTime();
            m_StopwatchUsed = false;
        }
        else if (m_PlayerManager.IsWearingClothingName(GlobalVariables.m_AnalogWatchGearName))
        {
            m_LabelDisplayTime.alpha = 1f;
            m_TimeDisplayState = TimeDisplayState.Partial;
            UpdateTime();
            m_StopwatchUsed = false;
        }
        else if (!m_StopwatchUsed)
        {
            m_LabelDisplayTime.alpha = 0f;
        }
    }

    internal void UpdateTime()
    {
        string currentHour = m_TimeOfDay.GetHour().ToString("D2");
        string currentMinutes = m_TimeOfDay.GetMinutes().ToString("D2");

        m_LabelDisplayTime.text = m_TimeDisplayState == TimeDisplayState.Full ? $"{currentHour}:{currentMinutes}" : $"{currentHour}:??";
    }
}
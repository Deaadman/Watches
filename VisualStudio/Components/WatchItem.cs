using Watches.Utilities;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class WatchItem : MonoBehaviour
{

#nullable disable
    static DisplayTime m_DisplayTime;
    internal static bool m_IsStopwatch;
    internal WatchType m_WatchType;
#nullable enable

    void Awake() => InitializeComponents();

    void ConfigureComponents()
    {
    }

    internal static void CompletedCheckingTime(bool success, bool playerCancel, float progress)
    {
        if (m_IsStopwatch)
        {
            // Something funky is going on with this m_StopwatchUsed logic.
            m_DisplayTime.m_StopwatchUsed = true;
            m_DisplayTime.m_LabelDisplayTime.alpha = 1f;
            m_DisplayTime.m_TimeDisplayState = TimeDisplayState.Partial;
            m_DisplayTime.UpdateTime();
        }
        else
        {
            Watches.ConfigureComponents.SundialPaused = true;
            m_DisplayTime.m_TimeDisplayState = TimeDisplayState.Partial;
            UserInterfaceUtilities.UISpriteAlphas(m_DisplayTime.GetComponentsInChildren<UISprite>(), true);
        }
    }

    void InitializeComponents()
    {
        // This is will always return null no matter how to try and get it. It's not added yet until the TimeWidget is shown first.
        m_DisplayTime = InterfaceManager.m_TimeWidget.gameObject.GetComponent<DisplayTime>();
        ConfigureComponents();
    }
}
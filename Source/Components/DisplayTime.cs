namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class DisplayTime : MonoBehaviour
{
    private TimeWidget m_TimeWidget;
    
    private void Awake()
    {
        m_TimeWidget = transform.GetComponent<TimeWidget>();
    }

    // This might be the key to hide the TimeWidget.
    // Not sure how to stop it from updating though without a harmony patch.
    private void LateUpdate()
    { 
        if (!SundialItem.IsVisible) m_TimeWidget.gameObject.SetActive(false);
    }
}
namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class SundialItem : MonoBehaviour
{
    internal static bool WasTimeChecked;
    
    internal static void TimeChecked(bool arg1, bool arg2, float arg3)
    {
        WasTimeChecked = false;
        InterfaceManager.m_TimeWidget.GetComponent<TimeWidget>().Update();
        WasTimeChecked = true;
    }
}
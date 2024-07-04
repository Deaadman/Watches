namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class WatchItem : MonoBehaviour
{
    internal enum WatchType
    {
        Stopwatch,
        Analog,
        Digital
    }

    internal WatchType m_WatchType;
    
    internal static void TimeChecked(bool arg1, bool arg2, float arg3)
    {
        
    }
}
namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class SundialItem : MonoBehaviour
{
    internal static bool IsPaused = true;
    internal static bool IsVisible = false;
    
    // I think we are pausing too quickly after unpausing, therefor not giving it enough time to actually update the icons positions.
    internal static void TimeChecked(bool arg1, bool arg2, float arg3)
    {
        IsPaused = false;
        IsVisible = true;
        IsPaused = true;
    }
}
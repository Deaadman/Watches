using Watches.Utilities;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class WatchItem : MonoBehaviour
{

#nullable disable
    internal WatchType m_WatchType;
#nullable enable

    void Awake() => InitializeComponents();

    void ConfigureComponents()
    {
    }

    void InitializeComponents()
    {
        ConfigureComponents();
    }
}
using Watches.Utilities;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class DisplayTime : MonoBehaviour
{
    internal UILabel m_DigitalTimeLabel;
    internal GameObject m_AnalogTime;
    //private GameObject m_TimeWidget = InterfaceManager.m_TimeWidget;

    private void Start()
    {
        m_DigitalTimeLabel = DisplayTimeUI.SetupDigitalTime(gameObject.transform,false);
    }

    internal static DisplayTime GetInstance() => InterfaceManager.m_TimeWidget.transform.parent.gameObject.GetComponent<DisplayTime>();

    private void Update()
    {
        if (!m_DigitalTimeLabel.gameObject.active) return;
        
        var watchInSlot = GameManager.GetPlayerManagerComponent().GetClothingInSlot(ClothingRegion.Accessory, ClothingLayer.Base).GetComponent<WatchItem>();
        if (watchInSlot is null)
            m_DigitalTimeLabel.gameObject.SetActive(false);
        else
            watchInSlot.Update();
    }
}
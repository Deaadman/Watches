using Watches.Enums;
using Watches.UserInterface;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class DisplayTime : MonoBehaviour
{
    internal GameObject m_AnalogTime;
    internal GameObject m_ObjectDuration;
    
    internal UILabel m_DigitalTimeLabel;
    internal UISprite m_ObjectDurationForegroundSprite;
    internal UISprite m_BatterySprite;
    
    internal UISprite m_HourHandSprite;
    internal UISprite m_MinuteHandSprite;

    private void Start()
    {
        m_DigitalTimeLabel = DisplayTimeUserInterface.SetupDigitalTime(transform,false);
        m_ObjectDuration = Instantiate(InterfaceManager.GetPanel<Panel_HUD>().m_EquipItemPopup.m_ObjectDuration, m_DigitalTimeLabel.transform);
        m_ObjectDuration.transform.localPosition = new Vector3(-65f, -20f, 0);
        m_ObjectDuration.SetActive(false);

        m_AnalogTime = DisplayTimeUserInterface.SetupAnalogTime(transform, false);
        
        m_ObjectDurationForegroundSprite = m_ObjectDuration.transform.Find("Foreground").GetComponent<UISprite>();
        m_BatterySprite = m_DigitalTimeLabel.transform.Find("BatterySprite").GetComponent<UISprite>();
        m_HourHandSprite = m_AnalogTime.transform.Find("HourHand").GetComponent<UISprite>();
        m_MinuteHandSprite = m_AnalogTime.transform.Find("MinuteHand").GetComponent<UISprite>();
    }

    internal static DisplayTime GetInstance() => InterfaceManager.m_TimeWidget.transform.parent.gameObject.GetComponent<DisplayTime>();

    private void LateUpdate()
    {
        if (!m_DigitalTimeLabel.gameObject.active && !m_AnalogTime.gameObject.active) return;
    
        var accessoryGearItem = GameManager.GetPlayerManagerComponent().GetClothingInSlot(ClothingRegion.Accessory, ClothingLayer.Base);
        if (accessoryGearItem?.GetComponent<WatchItem>() is null)
        {
            m_DigitalTimeLabel.gameObject.SetActive(false);
            if (!WatchItem.WasTimeChecked) m_AnalogTime.gameObject.SetActive(false);
            return;
        }
        
        var watchItem = accessoryGearItem.GetComponent<WatchItem>();
        switch (watchItem.m_WatchType)
        {
            case WatchType.Digital:
                watchItem.UpdateDigitalTime();
                break;
            case WatchType.Analog:
                watchItem.UpdateAnalogTime();
                break;
        }
    }
}
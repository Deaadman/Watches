using Watches.Enums;
using Watches.Utilities;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class DisplayTime : MonoBehaviour
{
    internal UILabel m_DigitalTimeLabel;
    internal GameObject m_AnalogTime;
    internal GameObject m_ObjectDuration;
    internal UISprite m_ObjectDurationForegroundSprite;
    //private GameObject m_TimeWidget = InterfaceManager.m_TimeWidget;

    private void Start()
    {
        m_DigitalTimeLabel = DisplayTimeUI.SetupDigitalTime(transform,false);
        m_ObjectDuration = Instantiate(InterfaceManager.GetPanel<Panel_HUD>().m_EquipItemPopup.m_ObjectDuration, m_DigitalTimeLabel.transform);
        m_ObjectDuration.transform.localPosition = new Vector3(-65f, -20f, 0);
        m_ObjectDuration.SetActive(false);
        
        m_ObjectDurationForegroundSprite = m_ObjectDuration.transform.Find("Foreground").GetComponent<UISprite>();
    }

    internal static DisplayTime GetInstance() => InterfaceManager.m_TimeWidget.transform.parent.gameObject.GetComponent<DisplayTime>();

    private void LateUpdate()
    {
        if (!m_DigitalTimeLabel.gameObject.active) return;
    
        var accessoryGearItem = GameManager.GetPlayerManagerComponent().GetClothingInSlot(ClothingRegion.Accessory, ClothingLayer.Base);
        if (accessoryGearItem?.GetComponent<WatchItem>() is null)
        {
            m_DigitalTimeLabel.gameObject.SetActive(false);
            return;
        }
        
        var watchItem = accessoryGearItem.GetComponent<WatchItem>();
        if (watchItem.m_WatchType == WatchType.Digital)
        {
            watchItem.UpdateDigitalTime();
        }
        else
        {
            watchItem.UpdateAnalogTime();
        }
    }
}
using Watches.Enums;
using Watches.UserInterface;

namespace Watches.Components;

[RegisterTypeInIl2Cpp(false)]
public class DisplayTime : MonoBehaviour
{
    internal GameObject AnalogTime;
    internal GameObject DurationObject;
    internal UILabel DigitalTimeLabel;
    internal UISprite BatterySprite;
    internal UISprite DurationObjectForegroundSprite;
    internal UISprite HourHandSprite;
    internal UISprite MinuteHandSprite;

    private void Start()
    {
        DigitalTimeLabel = DisplayTimeUserInterface.SetupDigitalTime(transform,false);
        DurationObject = Instantiate(InterfaceManager.GetPanel<Panel_HUD>().m_EquipItemPopup.m_ObjectDuration, DigitalTimeLabel.transform);
        DurationObject.transform.localPosition = new Vector3(-65f, -20f, 0);
        DurationObject.SetActive(false);

        AnalogTime = DisplayTimeUserInterface.SetupAnalogTime(transform, false);
        
        DurationObjectForegroundSprite = DurationObject.transform.Find("Foreground").GetComponent<UISprite>();
        BatterySprite = DigitalTimeLabel.transform.Find("BatterySprite").GetComponent<UISprite>();
        HourHandSprite = AnalogTime.transform.Find("HourHand").GetComponent<UISprite>();
        MinuteHandSprite = AnalogTime.transform.Find("MinuteHand").GetComponent<UISprite>();
        
        HourHandSprite.transform.localPosition = Vector3.zero;
        MinuteHandSprite.transform.localPosition = Vector3.zero;
    }

    internal static DisplayTime GetInstance() => InterfaceManager.m_TimeWidget.transform.parent.gameObject.GetComponent<DisplayTime>();

    private void LateUpdate()
    {
        if (!DigitalTimeLabel.gameObject.active && !AnalogTime.gameObject.active) return;
    
        var accessoryGearItem = GameManager.GetPlayerManagerComponent().GetClothingInSlot(ClothingRegion.Accessory, ClothingLayer.Base);
        if (accessoryGearItem?.GetComponent<WatchItem>() is null)
        {
            DigitalTimeLabel.gameObject.SetActive(false);
            if (!WatchItem.WasTimeChecked) AnalogTime.gameObject.SetActive(false);
            return;
        }
        
        var watchItem = accessoryGearItem.GetComponent<WatchItem>();
        switch (watchItem.WatchType)
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
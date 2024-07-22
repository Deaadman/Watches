using Watches.Components;
using Watches.Enums;
using Watches.UserInterface;
using Watches.Utilities;

namespace Watches.Patches;

internal static class InterfaceManagerPatches
{
    [HarmonyPatch(typeof(InterfaceManager), nameof(InterfaceManager.SetTimeWidgetActive))]
    private static class SetVisibilityOfDisplayTimeElements
    {
        private static bool Prefix(InterfaceManager __instance, bool active)
        {
            var displayTime = DisplayTime.GetInstance();
            if (GearItemUtilities.GetCurrentlyWornWatchType() == WatchType.Digital)
            {
                WatchItem.WasTimeChecked = false;
                SundialItem.WasTimeChecked = false;
                displayTime.DigitalTimeLabel.gameObject.SetActive(active);
                displayTime.DurationObject.gameObject.SetActive(active);
            }
            else if (GearItemUtilities.GetCurrentlyWornWatchType() == WatchType.Analog)
            {
                WatchItem.WasTimeChecked = false;
                SundialItem.WasTimeChecked = false;
                displayTime.AnalogTime.SetActive(active);
            }
            else if (WatchItem.WasTimeChecked)
            {
                displayTime.AnalogTime.SetActive(active);
            }
            else if (SundialItem.WasTimeChecked)
            {
                InterfaceManager.m_TimeWidget.SetActive(active);
            }
            return false;
        }
    }
    
    [HarmonyPatch(typeof(InterfaceManager), nameof(InterfaceManager.InstantiateTimeWidget))]
    private static class InstantiateDisplayTimesGameObject
    {
        private static bool Prefix(InterfaceManager __instance)
        {
            if (InterfaceManager.m_TimeWidget != null) return false;
            
            var displayTimesGameObject = DisplayTimeUserInterface.SetupDisplayTimesGameObject(InterfaceManager.s_CommonUIAnchor, false);
            
            InterfaceManager.m_TimeWidget = UnityEngine.Object.Instantiate(__instance.m_TimeWidgetPrefab, displayTimesGameObject.transform, false);
            InterfaceManager.m_TimeWidget.name = __instance.m_TimeWidgetPrefab.name;
            UnityEngine.Object.DontDestroyOnLoad(InterfaceManager.m_TimeWidget);
            InterfaceManager.m_TimeWidget.SetActive(false);
            
            return false;
        }
    }
    
    [HarmonyPatch(typeof(InterfaceManager), nameof(InterfaceManager.InitializeAndActivateTimeWidget), [typeof(Transform), typeof(Vector3)])]
    private static class SetupDisplayTimeGameObjectAsParent1
    {
        private static bool Prefix(InterfaceManager __instance, Transform parent, Vector3 pos)
        {
            var displayTime = DisplayTime.GetInstance();
            displayTime.transform.parent = parent;
            
            //InterfaceManager.m_TimeWidget.SetActive(true);
            //InterfaceManager.m_TimeWidget.transform.parent = displayTime.transform;
            InterfaceManager.m_TimeWidget.transform.position = pos;

            //displayTime.m_DigitalTimeLabel.gameObject.SetActive(true);
            displayTime.DigitalTimeLabel.transform.position = pos;
            displayTime.AnalogTime.transform.position = pos;
            
            return false;
        }
    }
    
    [HarmonyPatch(typeof(InterfaceManager), nameof(InterfaceManager.InitializeAndActivateTimeWidget), [typeof(Transform)])]
    private static class SetupDisplayTimeGameObjectAsParent2
    {
        private static bool Prefix(InterfaceManager __instance, Transform positionMarker)
        {
            var displayTime = DisplayTime.GetInstance();
            displayTime.transform.parent = positionMarker;
            
            //InterfaceManager.m_TimeWidget.SetActive(true);
            //InterfaceManager.m_TimeWidget.transform.parent = displayTime.transform;
            InterfaceManager.m_TimeWidget.transform.localPosition = Vector3.zero;
            
            //displayTime.m_DigitalTimeLabel.gameObject.SetActive(true);
            displayTime.DigitalTimeLabel.transform.localPosition = Vector3.zero;
            displayTime.AnalogTime.transform.position = Vector3.zero;
            
            return false;
        }
    }
}
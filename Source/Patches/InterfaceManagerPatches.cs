using Watches.Components;
using Watches.Enums;
using Watches.Utilities;
using Object = UnityEngine.Object;

namespace Watches.Patches;

internal static class InterfaceManagerPatches
{
    [HarmonyPatch(typeof(InterfaceManager), nameof(InterfaceManager.SetTimeWidgetActive))]
    private static class Test
    {
        private static bool Prefix(InterfaceManager __instance, bool active)
        {
            var displayTime = DisplayTime.GetInstance();
            if (GearItemUtilities.GetCurrentlyWornWatchType() == WatchType.Digital)
            {
                displayTime.m_DigitalTimeLabel.gameObject.SetActive(active);
                displayTime.m_ObjectDuration.gameObject.SetActive(active);
            }
            else if (GearItemUtilities.GetCurrentlyWornWatchType() == WatchType.Analog) // Need to add a check here that the Stopwatch was used as well.
            {
                displayTime.m_AnalogTime.SetActive(false);
            }
            
            //InterfaceManager.m_TimeWidget.SetActive(false);
            
            return false;
        }
    }
    
    [HarmonyPatch(typeof(InterfaceManager), nameof(InterfaceManager.InstantiateTimeWidget))]
    private static class Test2
    {
        private static bool Prefix(InterfaceManager __instance)
        {
            if (InterfaceManager.m_TimeWidget != null) return false;
            
            var displayTimesGameObject = DisplayTimeUI.SetupDisplayTimesGameObject(InterfaceManager.s_CommonUIAnchor, false);
            
            InterfaceManager.m_TimeWidget = Object.Instantiate(__instance.m_TimeWidgetPrefab, displayTimesGameObject.transform, false);
            InterfaceManager.m_TimeWidget.name = __instance.m_TimeWidgetPrefab.name;
            Object.DontDestroyOnLoad(InterfaceManager.m_TimeWidget);
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
            InterfaceManager.m_TimeWidget.transform.parent = displayTime.transform;
            InterfaceManager.m_TimeWidget.transform.position = pos;

            //displayTime.m_DigitalTimeLabel.gameObject.SetActive(true);
            displayTime.m_DigitalTimeLabel.transform.position = pos;
            
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
            InterfaceManager.m_TimeWidget.transform.parent = displayTime.transform;
            InterfaceManager.m_TimeWidget.transform.localPosition = Vector3.zero;
            
            //displayTime.m_DigitalTimeLabel.gameObject.SetActive(true);
            displayTime.m_DigitalTimeLabel.transform.localPosition = Vector3.zero;
            
            return false;
        }
    }
}
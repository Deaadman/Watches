namespace Watches;

class InitializeComponents
{
    [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.Start))]
    class AttachDigitalTime
    {
        static void Postfix(TimeWidget __instance)
        {
            _ = __instance.gameObject.GetComponent<DisplayTime>() ?? __instance.gameObject.AddComponent<DisplayTime>();
        }
    }
}
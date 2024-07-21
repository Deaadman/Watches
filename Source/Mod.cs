namespace Watches;

// All these commented out methods were testing for pausing the TimeWidget, hasn't really worked so far...
internal sealed class Mod : MelonMod
{
    //
    // [HarmonyPatch(typeof(TimeWidget), nameof(TimeWidget.UpdateIconPositions))]
    // private static class Test3
    // {
    //     private static bool firstRun = true;
    //     private static bool toggle = false;
    //
    //     private static bool Prefix(TimeWidget __instance, float angleDegrees)
    //     {
    //         if (firstRun)
    //         {
    //             firstRun = false;
    //             MelonCoroutines.Start(WaitOneFrameAndReturn());
    //             return true;
    //         }
    //     
    //         toggle = !toggle;
    //         return toggle;
    //     }
    //
    //     private static IEnumerator WaitOneFrameAndReturn()
    //     {
    //         yield return new WaitForEndOfFrame();
    //     }
    // }
}
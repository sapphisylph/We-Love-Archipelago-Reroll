using App.Katamari2;
using HarmonyLib;

namespace WeLoveArchipelago.Patcher;

public class QoL {


    [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiTutoCheckHirobaPresent1st)), HarmonyPrefix]
    public static bool SkipPresentTutorial() {
        return false;
    }

    [HarmonyPatch(typeof(NextArrow), nameof(NextArrow.SetVisible)), HarmonyPrefix]
    public static bool TurnOffGPS(ref bool __0) {  // Disables the guide that appears at the top of the screen telling you how to go to the next area. I'll likely make this an optional setting, I just have an unreasonable disdain for this thing
        if (Plugin.turnOffGPS) {
            if (__0 == true) {
                return false;
            } else { return true; }
        } else { return true; }
    }
    
}
   
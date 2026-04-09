using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using App.Katamari2;
using HarmonyLib;
using Il2CppSystem.Threading;

namespace WeLoveArchipelago.Patcher;

public class DisableFanShortcutMenu {

    private static bool isMenuOpen = false; 
    private static byte framesSinceMenuOpened = 0;
    
        
    [HarmonyPatch(typeof(SelectHiroba_ShortcutController), nameof(SelectHiroba_ShortcutController.SetActiveFalse)), HarmonyPostfix]
    public static void DetectMenuClosed() {
        isMenuOpen = false;
        framesSinceMenuOpened = 0;
    }


    [HarmonyPatch(typeof(SelectHiroba_ShortcutController), nameof(SelectHiroba_ShortcutController.ShortcutMain)), HarmonyPostfix]
    public static void DetectMenuOpen() {
        isMenuOpen = true;
        Plugin.BepinLogger.LogMessage("Hey! Close that!");
    }


    [HarmonyPatch(typeof(InputKeyboard), nameof(InputKeyboard.GetKeyboardInput)), HarmonyPrefix]
    public static bool TurnTheMenuOff(ref ulong __result) {
        if (isMenuOpen) {

            if (framesSinceMenuOpened == 0) {
                Plugin.LogDebug("Closing that...");
                __result = 66;  // When menu is opened, simulate a backspace press to immediately close it
            } 
            else if (framesSinceMenuOpened == 1) {
                Plugin.LogDebug("Alright buster, you wanna hold backspace? Letting go of backspace...");
                __result = 0;
            } 
            else if (framesSinceMenuOpened == 2) {
                Plugin.LogDebug("Re-pressing backspace...");
                __result = 66;
                framesSinceMenuOpened = 0;
            }

            framesSinceMenuOpened += 1;
            return false;
        }
        return true;
    }


    // [HarmonyPatch(typeof(SelectHiroba_ShortcutController), nameof(SelectHiroba_ShortcutController.TalkMain)), HarmonyPostfix]
    // public static void DetectDialogueBoxOpen() {
        
    // }



}
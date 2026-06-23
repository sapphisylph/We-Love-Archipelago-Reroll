using App.Katamari2;
using HarmonyLib;
using System;
using UnityEngine;
using WeLoveArchipelago.Utils;

namespace WeLoveArchipelago.Patcher;

public class DetectGameState {
    
    
    // Upon stage load, get the name of the stage the player has entered for referencing
    [HarmonyPatch(typeof(AssetLoader), nameof(AssetLoader.LoadSceneAsync), new Type[] { typeof(string), typeof(bool) }), HarmonyPostfix]
    public static void SetCurrentStage(string sceneName, bool isAdditive) {

        // Also save the AP save data
        _ = APSaveData.SaveAPDataToFile();

        Plugin.currentStage = sceneName;
        Plugin.LogDebug($"Scene loaded: {sceneName}");
        if (sceneName == "Result") {

            Plugin.isCurrentlyInALevel = false;
            
        } else {

            Plugin.isCurrentlyInALevel = true;
            Plugin.isCurrentlyInDialogue = true;

        }
    } 
    
    // The above function doesn't detect returning to the meadow from the pause menu, so I have to do that separately
    [HarmonyPatch(typeof(Game_Pause), nameof(Game_Pause.sToSelect)), HarmonyPostfix]
    public static void DetectReturnToMeadow() {
        Plugin.currentStage = "Result";
        Plugin.LogDebug($"Returning to select meadow...");
        Plugin.isCurrentlyInALevel = false;
    } 



    [HarmonyPatch(typeof(UIOusamaMessage), nameof(UIOusamaMessage.Initiate)), HarmonyPostfix]    
    public static void GetKingDialogueInstance(UIOusamaMessage __instance, string targetText, string command) {

        Plugin.isCurrentlyInDialogue = true;

        Plugin.KingMessage = __instance;

        // Plugin.LogDebug($"Message parameters: {command}");   // Use to get details on how messages are initiated

    }

    [HarmonyPatch(typeof(UIOusamaMessage), nameof(UIOusamaMessage.Terminate)), HarmonyPostfix]    
    public static void DetectDialogueBoxClose() {

        Plugin.isCurrentlyInDialogue = false;

    }


}
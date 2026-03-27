using App.Katamari2;
using HarmonyLib;
using System;
using UnityEngine;

namespace WeLoveArchipelago.Patcher;

public class LocationCheckHandler {



    // Upon stage load, store the name of the stage the player has entered for referencing below
    [HarmonyPatch(typeof(AssetLoader), nameof(AssetLoader.LoadSceneAsync), new Type[] { typeof(string), typeof(bool) }), HarmonyPostfix]
    public static void SetCurrentStage(App.Katamari2.AssetLoader __instance, string sceneName, bool isAdditive) {
        Plugin.currentStage = sceneName;
    } 


    [HarmonyPatch(typeof(Game), nameof(Game.mYm_GiSetCatchOuji)), HarmonyPostfix]
    public static void DetectCousinRollUp(sbyte _id) {
        int cousinId = (int)_id; 
        Plugin.APClient.SendCheck(cousinId + Plugin.COUSIN_ID_OFFSET);
    }

    // There's a different function for cousin roll-ups after the first time, so we'll hook this one too in case of a disconnect (or in case the AP screws with the first function)
    [HarmonyPatch(typeof(Game), nameof(Game.mYm_GiSetCatchOujiID_NotFirst)), HarmonyPostfix]
    public static void DetectCousinRollUpSecondTime(sbyte val) {
        int cousinId = (int)val; 
        Plugin.APClient.SendCheck(cousinId + Plugin.COUSIN_ID_OFFSET);
    }


    // Detect present roll-ups using the King's dialogue trigger as reference
    [HarmonyPatch(typeof(TextMessageTable), nameof(TextMessageTable.GetItem)), HarmonyPostfix]
    public static void DetectPresentRollUp(int idx) {
        if (idx == 18 || idx == 19) {    // All present collection dialogues have ID 18, except for race and possibly others which have ID 19
            byte currentMission = 0;
            if (Plugin.currentStage == "MissionScene/just_size_2") {   // Since these are the only separate versions of a stage that have presents, these need a special case (else they'd all be the same check)
                    currentMission = 39;
            } else if (Plugin.currentStage == "MissionScene/just_size_3") {
                    currentMission = 40;
            } else {
                currentMission = App.Katamari2.Game.mYm_GiGetMission();    // Since all present IDs are the same, get the mission ID to figure out which check to send 
            }
            Plugin.BepinLogger.LogMessage($"King Dialogue ID {idx} detected: Sending present check for level {currentMission}");
            Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET); 
        }
    }


    // Each level has its own clear function, so this is gonna be a little ugly, bear with me

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sTutorial)), HarmonyPostfix]
    public static void DetectTutorialClear() {
        Plugin.APClient.SendCheck(3 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sBig)), HarmonyPostfix]
    public static void DetectALAPClear() {
        byte currentMission = App.Katamari2.Game.mYm_GiGetMission();    // All ALAPs use the same function, so we need to retrieve the mission ID to know which check to send
        if (Plugin.currentStage.EndsWith("B")) {  // If the scene name ends with B, it's an AFAP, so add the AFAP offset
            currentMission += 26; // AFAP offset = 26
        }
        Plugin.APClient.SendCheck(currentMission + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sFlower)), HarmonyPostfix]
    public static void DetectFlowersClear() {
        if (Plugin.currentStage.EndsWith("B")) {
            Plugin.APClient.SendCheck(35 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(9 + Plugin.MISSION_ID_OFFSET);             
        }
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sSchool)), HarmonyPostfix]
    public static void DetectSchoolClear() {
        if (Plugin.currentStage.EndsWith("B")) {
            Plugin.APClient.SendCheck(36 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(10 + Plugin.MISSION_ID_OFFSET);             
        } 
    }

    public static GameObject LOAD_BEARING_RICKSHAW = null;

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sRace)), HarmonyPostfix]
    public static void DetectRaceClear() {
        LOAD_BEARING_RICKSHAW = GameObject.Find("/Props 0/SELFCAR01_E");    // This object (a rickshaw) only appears in Fast Race (as one of the racers), but because both races take place in the same 'scene', it's still loaded in race ALAP, just invisible and out of bounds (along with the guy who drives it)
        if (LOAD_BEARING_RICKSHAW.activeSelf) {     // if the rickshaw is active, then it must be AFAP
            Plugin.APClient.SendCheck(37 + Plugin.MISSION_ID_OFFSET); 
        } else {    // if it is not active, then it must be ALAP
            Plugin.APClient.SendCheck(11 + Plugin.MISSION_ID_OFFSET);             
        }
    }
    // For more info on why I do the above Like That, see the Campfire clear checks below which do the same thing for the same reason.


    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sSky)), HarmonyPostfix]
    public static void DetectCloudsClear() {
        Plugin.APClient.SendCheck(12 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sZoo)), HarmonyPostfix]
    public static void DetectFriendsClear() {
        if (Plugin.currentStage.EndsWith("B")) {
            Plugin.APClient.SendCheck(38 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(13 + Plugin.MISSION_ID_OFFSET);             
        }
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sOrigami)), HarmonyPostfix]
    public static void DetectOrigamiClear() {
        Plugin.APClient.SendCheck(14 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sJustSize)), HarmonyPostfix]
    public static void DetectJustRightClear() {
        if (Plugin.currentStage.EndsWith("1")) {
            Plugin.APClient.SendCheck(15 + Plugin.MISSION_ID_OFFSET); 
        } else if (Plugin.currentStage.EndsWith("2")) {
            Plugin.APClient.SendCheck(39 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(40 + Plugin.MISSION_ID_OFFSET);             
        }
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sCow)), HarmonyPostfix]
    public static void DetectCowbearClear() {
        Plugin.APClient.SendCheck(16 + Plugin.MISSION_ID_OFFSET); 
    }
    
    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sGetLimit)), HarmonyPostfix]
    public static void DetectLimitedTo50Clear() {
        Plugin.APClient.SendCheck(17 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sRoom)), HarmonyPostfix]
    public static void DetectCleaningClear() {
        Plugin.APClient.SendCheck(18 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sShopping)), HarmonyPostfix]
    public static void DetectMoneyClear() {
        Plugin.APClient.SendCheck(19 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sSweet)), HarmonyPostfix]
    public static void DetectSweetsClear() {
        if (Plugin.currentStage.EndsWith("B")) {
            Plugin.APClient.SendCheck(41 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(20 + Plugin.MISSION_ID_OFFSET);             
        }
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sLake)), HarmonyPostfix]
    public static void DetectUnderwaterClear() {
        if (Plugin.currentStage.EndsWith("B")) {
            Plugin.APClient.SendCheck(42 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(21 + Plugin.MISSION_ID_OFFSET);             
        }
    }

 
    // These are here for the campfire checks to reference
    public static GameObject LOGTOWER01_E = null;
    public static GameObject LOGTOWER02_E = null;

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sCamp)), HarmonyPostfix]
    public static void DetectCampfireClear() {
        // The campfire level in particular has all three stages in the same Scene, so the detection I use for every other level doesn't work here.
        // Instead, I check to see which campfire log is currently active when the level is cleared, since only one will be active at any given time.
        LOGTOWER01_E = GameObject.Find("/Props 0/LOGTOWER01_E");
        LOGTOWER02_E = GameObject.Find("/Props 0/LOGTOWER02_E");
        if (LOGTOWER01_E == null) {
            Plugin.BepinLogger.LogDebug("Unable to find object LOGTOWER01_E");
        }
        if (LOGTOWER02_E == null) {
            Plugin.BepinLogger.LogDebug("Unable to find object LOGTOWER02_E");
        }
        if (LOGTOWER01_E.activeSelf) {
            Plugin.APClient.SendCheck(22 + Plugin.MISSION_ID_OFFSET); 
        } else if (LOGTOWER02_E.activeSelf) {
            Plugin.APClient.SendCheck(43 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(44 + Plugin.MISSION_ID_OFFSET);             
        }
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sSumo)), HarmonyPostfix]
    public static void DetectSumoClear() {
        if (Plugin.currentStage.EndsWith("1")) {
            Plugin.APClient.SendCheck(23 + Plugin.MISSION_ID_OFFSET); 
        } else if (Plugin.currentStage.EndsWith("2")) {
            Plugin.APClient.SendCheck(45 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(46 + Plugin.MISSION_ID_OFFSET);             
        }
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sSnow)), HarmonyPostfix]
    public static void DetectSnowmanClear() {
        Plugin.APClient.SendCheck(24 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sHotaru)), HarmonyPostfix]
    public static void DetectFirefliesClear() {
        Plugin.APClient.SendCheck(25 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sEarth_ym_cl_record)), HarmonyPostfix]
    public static void DetectCountriesClear() {
        Plugin.APClient.SendCheck(28 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sItoko)), HarmonyPostfix]
    public static void DetectCousinsClear() {
        Plugin.APClient.SendCheck(29 + Plugin.MISSION_ID_OFFSET); 
    }

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sSpace)), HarmonyPostfix]
    public static void DetectRollUpSunClear() {
        Plugin.APClient.Goal(); 
    }

    // Later: Add Royal Reverie clears (sDLC_0, sDLC_1, etc)

}
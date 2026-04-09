using App.Katamari2;
using HarmonyLib;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

namespace WeLoveArchipelago.Patcher;

public class LocationCheckHandler {



    // Upon stage load, store the name of the stage the player has entered for referencing below
    [HarmonyPatch(typeof(AssetLoader), nameof(AssetLoader.LoadSceneAsync), new Type[] { typeof(string), typeof(bool) }), HarmonyPostfix]
    public static void SetCurrentStage(string sceneName, bool isAdditive) {
        Plugin.currentStage = sceneName;
        Plugin.LogDebug($"Scene loaded: {sceneName}");
    } 

    // The above function doesn't detect returning to the meadow from the pause menu, so I have to do that separately
    [HarmonyPatch(typeof(Game_Pause), nameof(Game_Pause.sToSelect)), HarmonyPostfix]
    public static void DetectReturnToMeadow() {
        Plugin.currentStage = "Result";
        Plugin.LogDebug($"Returning to select meadow...");
    } 



    [HarmonyPatch(typeof(Game), nameof(Game.mYm_GiSetCatchOuji)), HarmonyPostfix]
    public static void DetectCousinRollUp(sbyte _id) {
        Plugin.LogDebug("First cousin roll-up detected - sending check...");
        int cousinId = (int)_id; 
        Plugin.APClient.SendCheck(cousinId + Plugin.COUSIN_ID_OFFSET);
    }


    private static List<sbyte> ALAP5Cousins = [9, 32, 26, 38, 0];

    // There's a different function for cousin roll-ups after the first time, so we'll hook this one too in case of a disconnect (or in case the AP screws with the first function)
    [HarmonyPatch(typeof(Game), nameof(Game.mYm_GiSetCatchOujiID_NotFirst)), HarmonyPostfix]
    public static void DetectCousinRollUpSecondTime(sbyte val) {
        Plugin.LogDebug("Subsequent cousin roll-up detected - sending check...");
        int currentMission = App.Katamari2.Game.mYm_GiGetMission();
        if (currentMission == 8 && !ALAP5Cousins.Contains(val)) {
            Plugin.LogDebug("lol nvm you're in ALAP5");
            return; // Skip sending the check if on ALAP5 and cousin is not one of the four in the level. Or the prince bc he's nice and he deserves another place to be found
        }
        int cousinId = (int)val; 
        Plugin.APClient.SendCheck(cousinId + Plugin.COUSIN_ID_OFFSET);
    }


    private static List<int> levelsWithPresentId18 = [9, 12, 13, 14, 16, 17, 18, 19, 20, 22, 23, 25];
    private static List<int> levelsWithPresentId19 = [11, 10, 21];
    private static List<int> sweetsvilleSuperClearIDs = [102, 122, 142, 162, 182, 202, 222, 242, 262];


    // Detect present roll-ups, super clears, shooting stars, and goaling in Roll Up the Sun, all using the King's dialogue triggers as reference
    [HarmonyPatch(typeof(TextMessageTable), nameof(TextMessageTable.GetItem)), HarmonyPostfix]
    public static void DetectPresentRollUpsAndAlsoShootingStarsAndSuperClearsAndAlsoGoaling(int idx) {
        // I refuse to apologize for this wonderful, whimsical, and descriptive function name

        Plugin.LogDebug($"Playing King Dialogue with ID {idx}.");      

        int currentMission = App.Katamari2.Game.mYm_GiGetMission();

        Plugin.LogDebug($"Current Mission: {currentMission}");

        if (!(Plugin.currentStage == "Result")) {

            // If the current stage isn't the results screen, then it must be a level
            // This block contains all the present checks and the goal in RUtS
            // (These are in their own block so that the results screen checks don't have to check for every single one of these every time, or vice versa)

            if (idx == 18) {    // Most present collection dialogues have ID 18, except for most of them
                if (levelsWithPresentId18.Contains(currentMission)) {   // make sure that current mission is one of the ones with the present dialogue at ID 18
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET); 
                    return;
                } else {
                    Plugin.LogDebug($"King Dialogue ID {idx} was detected, but this is not the known present dialogue ID for level {currentMission}, so present check was not sent.");
                }
            } if (idx == 19) {    
                if (levelsWithPresentId19.Contains(currentMission)) {   // make sure that current mission is one of the ones with the present dialogue at ID 19
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET); 
                    return;
                } else {
                    Plugin.LogDebug($"King Dialogue ID {idx} was detected, but this is not the known present dialogue ID for level {currentMission}, so present check was not sent.");
                }
            } if (idx == 17) {   // Snowman
                if (currentMission == 24) {
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            } if (idx == 28 || idx == 68) {   // ALAP1 and AFAP1
                if (currentMission == 4) {
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            } if (idx == 29 || idx == 69) {   // ALAP2 and AFAP2
                if (currentMission == 5) {
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            } if (idx == 30 || idx == 70) {   // ALAP3 and ALAP4, AFAP3 and AFAP4
                if (currentMission == 6 || currentMission == 7) {
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            } if (idx == 32 || idx == 76) {   // ALAP5 and AFAP5
                if (currentMission == 8) {
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            } if (idx == 50) {   // Fast Flowers and Fast Friends
                if (currentMission == 9 || currentMission == 13) {
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            } if (idx == 51) {   // Fast School
                if (currentMission == 10) {
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            } if (idx == 52 || idx == 86) {   // Sumo 2/3 and Medium/Large Fire
                if (currentMission == 23 || currentMission == 22) {
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            } if (idx == 19 || idx == 52 || idx == 85) {   // Just-Rights
                if (currentMission == 15) {
                    if (Plugin.currentStage == "MissionScene/just_size_2") {   // Since these are the only separate versions of a stage that have presents, these need a special case (else they'd all be the same check)
                            currentMission = 39;
                    } else if (Plugin.currentStage == "MissionScene/just_size_3") {
                            currentMission = 40;
                    }
                    Plugin.LogDebug($"King Dialogue ID {idx} detected as present dialogue: Sending present check for level ID {currentMission}.");
                    Plugin.APClient.SendCheck(currentMission + Plugin.PRESENT_LOCATION_ID_OFFSET);                 
                    return;
                }
            }
            // Fast Underwater and Fast Race have presents in them, but they're impossible to collect because they are both larger than the target sizes

            if (idx == 6) {     // Sun roll-up dialogue ID in RUtS
                if (currentMission == 1) {
                    Plugin.LogDebug("Sun roll-up dialogue trigger detected: Sending goal. Conglaturations!");
                    Plugin.APClient.Goal();
                }
            }
        } else {    

            // These are all in the results screen (Shooting Stars and Super Clears)
            // Since these are all in the results scene, they all have different IDs, so we don't need to check what the current level is before sending the check
            
            // Super Clears and Shooting Stars 

            switch (currentMission) {   // Check which mission was the last one played, cause sometimes the IDs overlap between missions

                case 3:
                    if (idx == 18) {
                        Plugin.LogDebug("Tutorial Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(3 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 4:
                    if (idx == 38) {
                        Plugin.LogDebug("ALAP1 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(4 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 58) {
                        Plugin.LogDebug("AFAP1 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(30 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 54) {
                        Plugin.LogDebug("ALAP1 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(4 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 74) {
                        Plugin.LogDebug("AFAP1 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(30 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 5:
                    if (idx == 78) {
                        Plugin.LogDebug("ALAP2 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(5 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 98) {
                        Plugin.LogDebug("AFAP2 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(31 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 94) {
                        Plugin.LogDebug("ALAP2 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(5 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 114) {
                        Plugin.LogDebug("AFAP2 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(31 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 6:
                    if (idx == 118) {
                        Plugin.LogDebug("ALAP3 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(6 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 138) {
                        Plugin.LogDebug("AFAP3 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(32 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 134) {
                        Plugin.LogDebug("ALAP3 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(6 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 154) {
                        Plugin.LogDebug("AFAP3 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(32 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 7:
                    if (idx == 158) {
                        Plugin.LogDebug("ALAP4 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(7 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 178) {
                        Plugin.LogDebug("AFAP4 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(33 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 194) {
                        Plugin.LogDebug("AFAP4 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(33 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 174) {
                        Plugin.LogDebug("ALAP4 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(7 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 8:
                    if (idx == 198) {
                        Plugin.LogDebug("ALAP5 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(8 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 221) {
                        Plugin.LogDebug("AFAP5 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(34 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 217) {
                        Plugin.LogDebug("ALAP5 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(8 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 237) {
                        Plugin.LogDebug("AFAP5 Shooting Star detected, sending check.");
                        Plugin.APClient.SendCheck(34 + Plugin.SHOOTING_STAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 9:
                    if (idx == 241) {
                        Plugin.LogDebug("Flowers Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(9 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 261) {
                        Plugin.LogDebug("Fast Flowers Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(35 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 10:
                    if (idx == 281) {
                        Plugin.LogDebug("School Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(10 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 301) {
                        Plugin.LogDebug("Fast Students Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(36 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 11:
                    if (idx == 321) {
                        Plugin.LogDebug("Race Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(11 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 341) {
                        Plugin.LogDebug("Fast Race Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(37 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 12:
                    if (idx == 361) {
                        Plugin.LogDebug("Clouds Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(12 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 13:
                    if (idx == 381) {
                        Plugin.LogDebug("Friends Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(13 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 401) {
                        Plugin.LogDebug("Fast Friends Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(38 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 14:
                    if (idx == 421) {
                        Plugin.LogDebug("1000 Cranes detected, sending check.");
                        Plugin.APClient.SendCheck(14 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 15:
                    if (idx == 441) {
                        Plugin.LogDebug("Small Just-Right Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(15 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 461) {   // PRESUMABLY
                        Plugin.LogDebug("Medium Just-Right Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(39 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 481) {   // PRESUMABLY
                        Plugin.LogDebug("Large Just-Right Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(40 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 16:
                    if (idx == 2) {     // whaddaya mean cowbear super clear has an id of 2???
                        Plugin.LogDebug("Cowbear Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(16 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 17:
                    if (idx == 22) {    // oh this is probably gonna cause problems. oh no. why is this in the middle of the tutorial dialogues
                        Plugin.LogDebug("Limited to 50 Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(17 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 18:
                    if (idx == 42) {
                        Plugin.LogDebug("Cleaning Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(18 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 19:
                    if (idx == 62) {
                        Plugin.LogDebug("Money Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(19 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 20:
                    if (idx == 82) {
                        Plugin.LogDebug("Sweets Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(20 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (sweetsvilleSuperClearIDs.Contains(idx)) {
                        // Sweetsville has a unique set of dialogue IDs for each picture. Pain.
                        Plugin.LogDebug($"Sweetsville Super Clear detected (ID = {idx}), sending check.");
                        Plugin.APClient.SendCheck(41 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 21:
                    if (idx == 282) {
                        Plugin.LogDebug("Underwater Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(21 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 302) {
                        Plugin.LogDebug("Fast Underwater Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(42 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 22:
                    if (idx == 322) {
                        Plugin.LogDebug("Small Fire Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(22 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 342) {
                        Plugin.LogDebug("Medium Fire Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(43 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 362) {
                        Plugin.LogDebug("Large Fire Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(44 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 23:
                    if (idx == 382) {
                        Plugin.LogDebug("Small Sumo Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(23 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    if (idx == 402) {
                        Plugin.LogDebug("Medium Sumo Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(45 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    if (idx == 422) {
                        Plugin.LogDebug("Large Sumo Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(46 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    }
                    break;

                case 24:
                    if (idx == 442) {
                        Plugin.LogDebug("Snowman Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(24 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 25:
                    if (idx == 462) {
                        Plugin.LogDebug("Fireflies Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(25 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;

                case 28:
                    if (idx == 503) {
                        Plugin.LogDebug("Countries Super Clear detected, sending check.");
                        Plugin.APClient.SendCheck(28 + Plugin.SUPER_CLEAR_ID_OFFSET);
                        return;
                    } 
                    break;
                               
            }

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
        try {
            Plugin.LogDebug("Finding the Load-Bearing Rickshaw...");
            LOAD_BEARING_RICKSHAW = GameObject.Find("/Props 0/SELFCAR01_E");    // This object (a rickshaw) only appears in Fast Race (as one of the racers), but because both races take place in the same 'scene', it's still loaded in race ALAP, just invisible and out of bounds (along with the guy who drives it)
            Plugin.LogDebug($"Load-Bearing Rickshaw has been found at filepath: {LOAD_BEARING_RICKSHAW.ToString()}");
            if (LOAD_BEARING_RICKSHAW.activeSelf) {     // if the rickshaw is active, then it must be AFAP
                Plugin.APClient.SendCheck(37 + Plugin.MISSION_ID_OFFSET); 
            }             
        } catch {   // It runs into an exception if the rickshaw isn't loaded
            Plugin.LogDebug($"The Load-Bearing Rickshaw has collapsed! Sending ALAP check...");
            Plugin.APClient.SendCheck(11 + Plugin.MISSION_ID_OFFSET);
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
        try {

            if (LOGTOWER01_E.activeSelf) {
                Plugin.APClient.SendCheck(22 + Plugin.MISSION_ID_OFFSET); 
            } 
            
        } catch { 
            try { 
    
                if (LOGTOWER02_E.activeSelf) {
                Plugin.LogDebug($"Unable to find small campfire, but medium campfire was found. Sending medium fire check...");
                Plugin.APClient.SendCheck(43 + Plugin.MISSION_ID_OFFSET); 
                }

            } catch {

                Plugin.LogDebug($"Unable to find small or medium campfire, assuming large campfire. Sending large fire check...");
                Plugin.APClient.SendCheck(44 + Plugin.MISSION_ID_OFFSET);             

            }
        }
    }


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

 

    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sSumo)), HarmonyPostfix]
    public static void DetectSumoClear() {
        if (Plugin.currentStage.EndsWith("C")) {
            Plugin.APClient.SendCheck(46 + Plugin.MISSION_ID_OFFSET); 
        } else if (Plugin.currentStage.EndsWith("B")) {
            Plugin.APClient.SendCheck(45 + Plugin.MISSION_ID_OFFSET); 
        } else {
            Plugin.APClient.SendCheck(23 + Plugin.MISSION_ID_OFFSET);             
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

    // This also goals, but slightly later than the dialogue trigger. It's still here as a failsafe in case the first one doesn't work for whatever reason
    [HarmonyPatch(typeof(Game_Clear), nameof(Game_Clear.sSpace)), HarmonyPostfix]
    public static void DetectRollUpSunClear() {
        Plugin.LogDebug("Dialogue trigger missed: Sending goal anyway.");
        Plugin.APClient.Goal(); 
    }

    // Later: Add Royal Reverie clears (sDLC_0, sDLC_1, etc)


    [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiGameCheckMonoGet)), HarmonyPostfix]
    public static void DetectCollectionItemRollUp(int _code) {

        if (Plugin.currentStage == "Result") {
            return; // Don't bother trying to send checks if in the select meadow
        } 

        // For some reason, rolling up the Prince doesn't use the same function as every other cousin, so I just detect the raw item roll-up instead
        if (_code == 3381 || _code == 3421 || _code == 3461) {  // Listing these out manually instead of referencing the list in ForceCousinsToAppearPatch because I feel like it'd hurt performance to run a function on the same list of 3 items every time an item is rolled up    
            Plugin.LogDebug("Prince roll-up detected: Sending Prince roll-up check.");
            Plugin.APClient.SendCheck(Plugin.COUSIN_ID_OFFSET);
            return;
        }

        // Collectionsanity goes here... Eventually ;)

    }

}
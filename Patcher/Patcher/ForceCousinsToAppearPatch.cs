using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using App.Katamari2;
using HarmonyLib;
using Il2CppSystem.Threading;

namespace WeLoveArchipelago.Patcher;

public class ForceCousinsToAppearPatch {

    // Lists of every cousin's collection IDs, because for some reason each cousin has multiple and not all of them follow a consistent pattern. Credit goes to the WLK speedrunning discord for collecting all these!

    public static readonly List<int> collectionPrince = [3381, 3421, 3461, 1476];
    public static readonly List<int> collectionLalala = [3382, 3464, 1477];
    public static readonly List<int> collectionNik = [3463, 1478, 3383, 3423];
    public static readonly List<int> collectionAce = [3384, 3464, 1479, 3424];
    public static readonly List<int> collectionJohnson = [3465, 1480, 3385, 3425];
    public static readonly List<int> collectionVelvet = [3386, 3466, 1481];
    public static readonly List<int> collectionFujio = [3387, 1482, 3378];
    public static readonly List<int> collectionHavana = [3468, 1483, 3388, 3369, 3428];
    public static readonly List<int> collectionPeso = [3389, 3469, 1484];
    public static readonly List<int> collectionShikao = [3390, 1485, 3430];
    public static readonly List<int> collectionOdeko = [3391, 3471, 1486];
    public static readonly List<int> collectionHoney = [3432, 3472, 3370, 3392, 1487];
    public static readonly List<int> collectionMarny = [3393, 3473, 3371, 1488];
    public static readonly List<int> collectionKuro = [3394, 3434, 3474, 1489];
    public static readonly List<int> collectionFoomin = [3372, 3475, 3395, 1490];
    public static readonly List<int> collectionJune = [3379, 3396, 1491];
    public static readonly List<int> collectionIchigo = [3397, 3477, 1492];
    public static readonly List<int> collectionMarcy = [3478, 3398, 3438, 1493];
    public static readonly List<int> collectionNjamo = [3399, 3479];
    public static readonly List<int> collectionDipp = [3400, 3480];
    public static readonly List<int> collectionOpeo = [3401, 3481];
    public static readonly List<int> collectionNickel = [3402, 3442, 3482];
    public static readonly List<int> collectionJungle = [3403, 3483];
    public static readonly List<int> collectionMiso = [3484, 3375, 3404, 3444];
    public static readonly List<int> collectionTwinkle = [3376, 3405, 3485, 1697];
    public static readonly List<int> collectionHuey = [3406, 3486, 1698, 3446];
    public static readonly List<int> collectionNutsuo = [3407, 1699, 3487];
    public static readonly List<int> collectionBeyond = [3408, 3488, 1700];
    public static readonly List<int> collectionKinoko = [3409, 3489, 1701, 3449];
    public static readonly List<int> collectionMacho = [3490, 1702, 3410, 3450];
    public static readonly List<int> collectionLamour = [3411, 3451, 3491, 1703];
    public static readonly List<int> collectionDaisy = [3412, 3492, 1704];
    public static readonly List<int> collectionLucha = [3413, 1705];
    public static readonly List<int> collectionMiki = [3414, 3494, 1706];
    public static readonly List<int> collectionOdeon = [3415, 1707];
    public static readonly List<int> collectionCanCan = [3416, 3456, 3496, 1708];
    public static readonly List<int> collectionShy = [3417, 3497, 1709];
    public static readonly List<int> collectionSlip = [3418, 3498, 1710, 3458];
    public static readonly List<int> collectionDrooby = [3380, 1711, 3419];
    public static readonly List<int> collectionSignolo = [3500, 1712, 3420, 3460];


    // And a list of all the lists together to reference if all cousins should always appear
    public static List<List<int>> listOfCousinLists = [collectionPrince, collectionLalala, collectionNik, collectionAce, collectionJohnson, collectionVelvet, collectionFujio, collectionHavana, collectionPeso, collectionShikao, collectionOdeko, collectionHoney, collectionMarny, collectionKuro, collectionFoomin, collectionJune, collectionIchigo, collectionMarcy, collectionNjamo, collectionDipp, collectionOpeo, collectionNickel, collectionJungle, collectionMiso, collectionTwinkle, collectionHuey, collectionNutsuo, collectionBeyond, collectionKinoko, collectionMacho, collectionLamour, collectionDaisy, collectionLucha, collectionMiki, collectionOdeon, collectionCanCan, collectionShy, collectionSlip, collectionDrooby, collectionSignolo];

    public static bool queueForceNewCousinsSpawn = true;
    public static List<int> recentlyReceivedFans = [];
    public static List<int> cousinsToForceIn = collectionPrince;

    public static bool hasTutorialFlagBeenSet = false;




    // The collection determines which cousins should be able to spawn either in their non-native levels or as progressive unlocks (ie you need Can-Can in the collection before Havana will appear in ALAP2).
    // Here, cousin collection data is set for every cousin based on which stage they first appear in (or all at once, if the setting allows it)
    [HarmonyPatch(typeof(Select), nameof(Select.mYd_OujiSetFanNo)), HarmonyPostfix]
    public static void ForceProgressiveCousinSpawns() {     // Patches a function that runs every time you talk to an npc in the select meadow for quick changing of the collected cousins in case a level is found while in the meadow
        

        if (queueForceNewCousinsSpawn) {

            Plugin.LogDebug("Adding new cousins to the collection...");

            if (Plugin.cousinsAppearAnywhere) {

                Plugin.LogDebug("Adding all cousins to the collection...");
                foreach (List<int> list in listOfCousinLists) {
                    cousinsToForceIn.AddRange(list);
                }

            } else {
                
                foreach (int fanId in recentlyReceivedFans) {
                    
                    switch (fanId) {
                        
                        case 0:
                            cousinsToForceIn.AddRange(collectionMacho);
                            Plugin.LogDebug("Rainbow Girl received! Adding cousins to the collection.");
                            break;
                        case 1:
                            cousinsToForceIn.AddRange(collectionCanCan);
                            cousinsToForceIn.AddRange(collectionHavana);
                            Plugin.LogDebug("Lazybones received! Adding cousins to the collection.");
                            break;
                        case 2:
                            cousinsToForceIn.AddRange(collectionJohnson);
                            cousinsToForceIn.AddRange(collectionTwinkle);
                            cousinsToForceIn.AddRange(collectionKuro);
                            Plugin.LogDebug("Grandpa received! Adding cousins to the collection.");
                            break;
                        case 3:
                            cousinsToForceIn.AddRange(collectionFoomin);
                            cousinsToForceIn.AddRange(collectionMiki);
                            cousinsToForceIn.AddRange(collectionVelvet);
                            Plugin.LogDebug("Grandma received! Adding cousins to the collection.");
                            break;
                        case 4:
                            cousinsToForceIn.AddRange(collectionShikao);
                            cousinsToForceIn.AddRange(collectionLucha);
                            cousinsToForceIn.AddRange(collectionNutsuo);
                            cousinsToForceIn.AddRange(collectionDrooby);
                            Plugin.LogDebug("Bird and Elephant received! Adding cousins to the collection.");
                            break;
                        case 6:
                            cousinsToForceIn.AddRange(collectionSlip);
                            cousinsToForceIn.AddRange(collectionAce);
                            Plugin.LogDebug("Soccer Kid received! Adding cousins to the collection.");
                            break;
                        case 7:
                            cousinsToForceIn.AddRange(collectionIchigo);
                            Plugin.LogDebug("Ikebana Teacher received! Adding cousins to the collection.");
                            break;
                        case 8:
                            cousinsToForceIn.AddRange(collectionMiso);
                            cousinsToForceIn.AddRange(collectionHuey);
                            Plugin.LogDebug("Subst. Teacher received! Adding cousins to the collection.");
                            break;
                        case 9:
                            cousinsToForceIn.AddRange(collectionOdeko);
                            cousinsToForceIn.AddRange(collectionShy);
                            cousinsToForceIn.AddRange(collectionNickel);
                            Plugin.LogDebug("F1 Racer received! Adding cousins to the collection.");
                            break;
                        case 10:
                            cousinsToForceIn.AddRange(collectionJune);
                            cousinsToForceIn.AddRange(collectionFujio);
                            Plugin.LogDebug("Rain Coat Girl received! Adding cousins to the collection.");
                            break;
                        case 11:
                            cousinsToForceIn.AddRange(collectionBeyond);
                            cousinsToForceIn.AddRange(collectionNjamo);
                            Plugin.LogDebug("Dr. Katamari received! Adding cousins to the collection.");
                            break;
                        case 12:
                            cousinsToForceIn.AddRange(collectionOpeo);
                            Plugin.LogDebug("Crane Hat Boy received! Adding cousins to the collection.");
                            break;
                        case 13:
                            cousinsToForceIn.AddRange(collectionMarcy);
                            cousinsToForceIn.AddRange(collectionPeso);
                            cousinsToForceIn.AddRange(collectionSignolo);
                            Plugin.LogDebug("Just-right Girl received! Adding cousins to the collection.");
                            break;
                        case 14:
                            cousinsToForceIn.AddRange(collectionDaisy);
                            Plugin.LogDebug("Cowbear Farmer received! Adding cousins to the collection.");
                            break;
                        case 15:
                            cousinsToForceIn.AddRange(collectionJungle);
                            Plugin.LogDebug("Excited Baby received! Adding cousins to the collection.");
                            break;
                        case 16:
                            cousinsToForceIn.AddRange(collectionLamour);
                            Plugin.LogDebug("F1 Racer received! Adding cousins to the collection.");
                            break;
                        case 17:
                            cousinsToForceIn.AddRange(collectionOdeon);
                            Plugin.LogDebug("Fundraiser received! Adding cousins to the collection.");
                            break;
                        case 18:
                            cousinsToForceIn.AddRange(collectionHoney);
                            Plugin.LogDebug("Sweetsville received! Adding cousins to the collection.");
                            break;
                        case 19:
                            cousinsToForceIn.AddRange(collectionMarny);
                            Plugin.LogDebug("Float Boy received! Adding cousins to the collection.");
                            break;
                        case 20:
                            cousinsToForceIn.AddRange(collectionKinoko);
                            Plugin.LogDebug("Camper Man received! Adding cousins to the collection.");
                            break;
                        case 21:
                            cousinsToForceIn.AddRange(collectionNik);
                            Plugin.LogDebug("Sumo received! Adding cousins to the collection.");
                            break;
                        case 22:
                            cousinsToForceIn.AddRange(collectionLalala);
                            Plugin.LogDebug("Snow Child received! Adding cousins to the collection.");
                            break;
                        case 23:
                            cousinsToForceIn.AddRange(collectionDipp);
                            Plugin.LogDebug("Bookworm received! Adding cousins to the collection.");
                            break;
                        default:
                            Plugin.LogDebug($"Received fan with ID {fanId} does not have any cousins natively in their level, so no cousins were added to the collection.");
                            break;
                    } 


                }
            }

            foreach (int cousinId in cousinsToForceIn) {
                Game.mYm_SiGameSetMonoGet(cousinId, 1);   // Set each cousin ID to be in the collected state (1)
            }

            queueForceNewCousinsSpawn = false;  // make it not happen again until another fan is received
            recentlyReceivedFans = [];  // empty the list of received fans to make it not cycle through all of them again


        }


        // if (!hasTutorialFlagBeenSet) {
        //     Select.sTutorial();
        // } 
    }
}
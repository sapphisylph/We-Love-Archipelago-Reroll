using App.Katamari2;
using HarmonyLib;
using System.Collections.Generic;
using System.Linq;
using WeLoveArchipelago.Archipelago;
using WeLoveArchipelago.Utils;

namespace WeLoveArchipelago.Patcher;

public class Fun {


    // These have already been turned into text traps, but I'm leaving this here for reference for later in case I want to make other custom loading screen messages (maybe one that lists players/items/games in the multiworld?)
    // [HarmonyPatch(typeof(TextMessageTable), nameof(TextMessageTable.GetText)), HarmonyPrefix]
    // public static bool ChangeLoadingScreenText(ref string __result, int __0) { // allows changing the loading screen text
    //     switch(__0) {
    //         case 0:
    //             __result = "Crazy? We were crazy once. Papa locked Us in a room. A rubber room. A rubber room with rats. The rats made Us crazy. Crazy? We were crazy once. Papa locked Us in a room. A rubber room. A rubber room with rats. The rats made Us crazy. Crazy? We were crazy once. Papa locked Us in a room. A rubber room. A rubber room with rats. The rats made Us crazy.";
    //             break;
    //         case 1:
    //             __result = "Hello ladies and gentlemen, welcome to episode one of Kingcraft, the series where We play Minecraft! This is going to be a single player let's play, and when We say 'let's play,' We use that term... pretty loosely, because We are an idiot in this game. You're gonna see lots of fails, you're gonna see lots of triumphs, at least We're hoping, so go grab yourself a nice hot cup of coffee, hot cup of cocoa. We got apple cider right here, freshly-brewed. Let Us take a sip... ah, that's some good cider!";
    //             break;
    //         default:
    //             return true;
    //     }
    //         return false;
    // }


    [HarmonyPatch(typeof(BgmTable), nameof(BgmTable.GetData)), HarmonyPrefix]
    public static void MusicRando(App.Katamari2.BgmTable __instance, ref int __0) {
        if (Plugin.musicRandoEnabled) {
            if (__instance.name == "Bgm") {     // Make it so it only randomizes music, not anything else that this function calls
                __0 = Plugin.musicRandoList[__0];   // If the game tries to play music with ID of __0, make it play the randomized music track instead (with the ID in the __0th spot in the list)
                Plugin.LogDebug($"Music Rando: Playing music track with ID {__0}");
            }
        }
    }


}
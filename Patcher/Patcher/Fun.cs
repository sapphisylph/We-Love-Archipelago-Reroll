using App.Katamari2;
using HarmonyLib;

namespace WeLoveArchipelago.Patcher;

public class Fun {

    [HarmonyPatch(typeof(TextMessageTable), nameof(TextMessageTable.GetText)), HarmonyPrefix]
    public static bool ChangeLoadingScreenText(ref string __result, int __0) { // allows changing the loading screen text
        // Most of these will likely be turned into text trap dialogues once we can figure out how to make those
        switch(__0) {
            case 0:
                __result = "Crazy? We were crazy once. Papa locked Us in a room. A rubber room. A rubber room with rats. The rats made Us crazy. Crazy? We were crazy once. Papa locked Us in a room. A rubber room. A rubber room with rats. The rats made Us crazy. Crazy? We were crazy once. Papa locked Us in a room. A rubber room. A rubber room with rats. The rats made Us crazy.";
                break;
            case 1:
                __result = "Hello ladies and gentlemen, welcome to episode one of Kingcraft, The series where We play Minecraft! This is going to be a single player let's play, and when We say 'let's play,' We use that term... pretty loosely, because We are an idiot in this game. You're gonna see lots of fails, you're gonna see lots of triumphs, at least We're hoping, so go grab yourself a nice hot cup of coffee, hot cup of cocoa. We got apple cider right here, freshly-brewed. Let Us take a sip... ah, that's some good cider!";
                break;
            case 2:
                __result = "We're no stranger to love. You know the rules, and so do We! A katamari's what We're thinking of. You wouldn't get this from any other King. We just want to tell you how We're feeling. Gotta let you understand, We are gonna roll you up, We are gonna roll you 'round, We are gonna roll through town and collect you! Never gonna make you cry, never gonna say goodbye, never gonna tell a lie and hurt you!";
                break;
            case 3:
                __result = "The FitnessGram Katamari Test is a multistage rolling capacity test that gets progressively more difficult as it continues. The 20m Katamari test will begin in 30 seconds. Line up at the start. The rolling speed starts slowly, but gets faster each minute after you hear this signal *boop*. A single star should be created each time you hear this sound *ding*. Remember to roll in a straight line, and roll as long as possible. The second time you fail to create a star before the sound, your test is over. The test will begin on the word start. On your mark, get ready, start.";
                break;
            case 4:
                __result = "Cowbear? Aw man! So we back on the track, Katamaris rolling from side to side, side-side to side. This task a grueling one, hope to roll a Cowbear tonight, tonight, Cowbear tonight. Heads up! Hear a sound, turn around and look up! Total shock in Katamari. Oh no, it's you again! I think I remember those eyes, eyes, eyes, eyes-eyeseyesCAUSE BABY TONIGHT!!! COWBEAR'S GONNA KICK US AROUND AGAIN!!! ('round again) CAUSE BABY TONIGHT!! GRAB YOUR RICE, HAYBALES AND PRESENT AGAIN!! (present again-gain) So roll, roll, until your goal, goal, until the sun's rolled up in the morn'. CAUSE BABY TONIGHT!! COWBEAR'S GONNA KICK US 'ROUND AGAIN!!";
                break;
            case 5:
                __result = "It's Us, \\ouji[0], We're the PS5, speaking to you from inside your brain! Listen to Us, boy! Leave the fan, We don't need them, come with Us and play Our games! We'll have fun KATAMARI Time in... Space! Doo doo doo doo, yeah! You need Us, \\ouji[0], your free will is an illusion!";
                break;
            case 6:
                __result = "We've come to make an announcement. RoboKing is a complete loser. He rolled up Our Queen! That's right. He took his weird cuckoo clock core out and rolled up Our Queen, and he said his katamari was THIS BIG, And We said \"That's disgusting!\" So We're making a callout post on Our Katamari Damacy Rolling LIVE. RoboKing, you have a small katamari. It's the size of this walnut except WAY smaller. And guess what, here's what Our katamari looks like! That's right, \\ouji[0]! All lumps, no vaults, no cuckoo clocks! Look at that, it looks like a takoyaki but without the stick. He rolled up Our Queen, so guess what? We're gonna roll up the earth! Except We're not gonna roll up the earth, We're gonna go higher! We're rolling up the sun! How do you like that, Papa? We rolled up the sun, you idiot! You have 23 hours before a meteor hits the earth. Now get out of Our sight before We roll you up too!";
                break;
            case 7:
                __result = "Blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah ";
                break;
            default:
                return true;
        }
            return false;
    }


    [HarmonyPatch(typeof(BgmTable), nameof(BgmTable.GetData)), HarmonyPrefix]
    public static void MusicRando(App.Katamari2.BgmTable __instance, ref int __0) {
        if (Plugin.musicRandoEnabled) {
            if (__instance.name == "Bgm") {     // Make it so it only randomizes music, not anything else that this function calls
                __0 = Plugin.musicRandoList[__0];   // If the game tries to play music with ID of __0, make it play the randomized music track instead (with the ID in the __0th spot in the list)
                Plugin.BepinLogger.LogMessage($"Music Rando: Playing music track with ID {__0}");
            }
        }
    }



}
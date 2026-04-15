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
    //         case 2:
    //             __result = "We're no stranger to love. You know the rules, and so do We! A katamari's what We're thinking of. You wouldn't get this from any other King. We just want to tell you how We're feeling. Gotta let you understand, We are gonna roll you up, We are gonna roll you 'round, We are gonna roll through town and collect you! Never gonna make you cry, never gonna say goodbye, never gonna tell a lie and hurt you!";
    //             break;
    //         case 3:
    //             __result = "The FitnessGram Katamari Test is a multistage rolling capacity test that gets progressively more difficult as it continues. The 20m Katamari test will begin in 30 seconds. Line up at the start. The rolling speed starts slowly, but gets faster each minute after you hear this signal *boop*. A single star should be created each time you hear this sound *ding*. Remember to roll in a straight line, and roll as long as possible. The second time you fail to create a star before the sound, your test is over. The test will begin on the word start. On your mark, get ready, start.";
    //             break;
    //         case 4:
    //             __result = "Cowbear? Aw man! So we back on the track, Katamaris rolling from side to side, side-side to side. This task a grueling one, hope to roll a Cowbear tonight, tonight, Cowbear tonight. Heads up! Hear a sound, turn around and look up! Total shock in Katamari. Oh no, it's you again! I think I remember those eyes, eyes, eyes, eyes-eyeseyesCAUSE BABY TONIGHT!!! COWBEAR'S GONNA KICK US AROUND AGAIN!!! ('round again) CAUSE BABY TONIGHT!! GRAB YOUR RICE, HAYBALES AND PRESENT AGAIN!! (present again-gain) So roll, roll, until your goal, goal, until the sun's rolled up in the morn'. CAUSE BABY TONIGHT!! COWBEAR'S GONNA KICK US 'ROUND AGAIN!!";
    //             break;
    //         case 5:
    //             __result = "It's Us, \\ouji[0], We're the PS5, speaking to you from inside your brain! Listen to Us, boy! Leave the fan, We don't need them, come with Us and play Our games! We'll have fun KATAMARI Time in... Space! Doo doo doo doo, yeah! You need Us, \\ouji[0], your free will is an illusion!";
    //             break;
    //         case 6:
    //             __result = "We've come to make an announcement. RoboKing is a complete loser. He rolled up Our Queen! That's right. He took his weird cuckoo clock core out and rolled up Our Queen, and he said his katamari was THIS BIG, And We said \"That's disgusting!\" So We're making a callout post on Our Katamari Damacy Rolling LIVE. RoboKing, you have a small katamari. It's the size of this walnut except WAY smaller. And guess what, here's what Our katamari looks like! That's right, \\ouji[0]! All lumps, no vaults, no cuckoo clocks! Look at that, it looks like a takoyaki but without the stick. He rolled up Our Queen, so guess what? We're gonna roll up the earth! Except We're not gonna roll up the earth, We're gonna go higher! We're rolling up the sun! How do you like that, Papa? We rolled up the sun, you idiot! You have 23 hours before a meteor hits the earth. Now get out of Our sight before We roll you up too!";
    //             break;
    //         case 7:
    //             __result = "blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah blah ";
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


    public static void CreateKingRollUpDialogue(int location) {
        
        List<string> foundItemData = ArchipelagoClient.scoutedLocations[location]; // Get the scouted item data to use to display (item data is scouted upon connection)
        
        string foundItemName = foundItemData[0];
        string displayedItem = foundItemName;
        string foundItemClass = foundItemData[1];
        string foundItemRecipient = foundItemData[2];
        string foundItemGame = foundItemData[3];
        string isRecipientMe = foundItemData[4];

        if (foundItemName == null) {
            return;
        }

        if (!ReadFiles.itemDescriptions.ContainsKey(foundItemGame)) {
            
            Plugin.LogDebug($"Game {foundItemGame} does not have a valid json file. Using generic descriptions instead.");

            // If the game is not in the supported list of games with adjectives, use the generic ones based on item class instead    
            foundItemName = "default_unknown";
            switch (foundItemClass) {

                case "Advancement":
                    foundItemGame = "Unknown (Progression)";
                    break;

                case "NeverExclude":
                    foundItemGame = "Unknown (Useful)";
                    break;

                case "Trap":
                    foundItemGame = "Unknown (Trap)";
                    break;

                default:
                    foundItemGame = "Unknown (Filler)";
                    break;

            }
        }
        else if (!ReadFiles.itemDescriptions[foundItemGame].ContainsKey(foundItemName)) {
            
            Plugin.LogDebug($"Item {foundItemName} from {foundItemGame} does not have a description provided. Using defaults.");

            // If the item isn't in the list of items in the json, use the default for the game if it exists
            foundItemName = "default_unknown";

            // else, fall back on the generic defaults
            if (!ReadFiles.itemDescriptions[foundItemGame].ContainsKey("default_unknown")) {

                Plugin.LogDebug($"No defaults were provided for {foundItemGame}. Using generic defaults.");

                switch (foundItemClass) {

                    case "Advancement":
                        foundItemGame = "Unknown (Progression)";
                        break;

                    case "NeverExclude":
                        foundItemGame = "Unknown (Useful)";
                        break;

                    case "Trap":
                        foundItemGame = "Unknown (Trap)";
                        break;

                    default:
                        foundItemGame = "Unknown (Filler)";
                        break;

                }
            }
        }

        // Now that the scouted item data has been collected and set to defaults if necessary, time to build the king message using the json data 

        // Get a random description from the list of descriptions for the given item (or generic item)
        string foundItemDescription = ReadFiles.itemDescriptions[foundItemGame][foundItemName][Plugin.rand.Next(ReadFiles.itemDescriptions[foundItemGame][foundItemName].Count)];
        
        byte itemColor;

        switch (foundItemClass) {

            case "Advancement":
                itemColor = 2;
                break;

            case "NeverExclude":
                itemColor = 5;
                break;

            case "Trap":
                itemColor = 1;
                break;

            default:
                itemColor = 3;
                break;
            
        }

        byte descriptionColor = itemColor;

        if (ReadFiles.itemColors.ContainsKey(foundItemGame)) {
            
            if (ReadFiles.itemColors[foundItemGame].ContainsKey(foundItemName)) {
            
                descriptionColor = ReadFiles.itemColors[foundItemGame][foundItemName];
                // Set the description to have its custom-set color if defined in the json
                // Else, it defaults to the progression class color above

            }
        }

        if (displayedItem.EndsWith("<3")) {
            
            int displayedItemLength = displayedItem.Length;
            string heartItem = displayedItem.Remove(displayedItemLength - 2);
            heartItem.Concat("\\col[1]ΓÖÑ");    // \\col[1] is a character that turns the following text red, and ΓÖÑ turns into a heart when displayed in-game
            displayedItem = heartItem;

        }

        // This group here changes the dialogue based on Circumstances
        // This dialogue is put into the game using a function hooked in QoL.cs

        // Notes: 
            // \n is the newline character in C#
            // \\col[int] is the character used to change the color of the text

        string firstPart = $"Oh!! You just rolled up \nsome weird \\col[{descriptionColor}]{foundItemDescription}\\col[0]!!\n";
        string secondPart = $"Wait... it's just \\col[{itemColor}]{displayedItem}\\col[0].\nOh, go back to \\col[4]{foundItemRecipient}\\col[0]'s world!";

        if (foundItemDescription.EndsWith(" thing")) {   // If the item description ends with thing, separate out the "thing" so that only the other part is colored and the "thing" remains white like the rest of the text
            
            int descriptionLength = foundItemDescription.Length;
            string trimmedItemDescription = foundItemDescription.Remove(descriptionLength - 6);  // Remove the last 6 characters of the string (" thing")
            firstPart = $"Oh!! You just rolled up \nsome weird \\col[{descriptionColor}]{trimmedItemDescription}\\col[0] thing!!\n";

        }

        if (isRecipientMe == "True") {  // I know I could turn this back into a bool, but like who cares, I'm only using it for one comparison
            
            secondPart = $"Wait... it's just \\col[{itemColor}]{displayedItem}\\col[0].\nOh, stop getting in the way!";
            
        }

        Plugin.rollCheckDialogue = firstPart + secondPart;


        // Present checks are sent after the dialogue, but cousin checks are sent before the dialogue, so we need to handle them differently

        if (location >= Plugin.PRESENT_LOCATION_ID_OFFSET) {

            Plugin.showNewRollCheckDialogue = true;
            // This bool is checked in a function in QoL.cs to display the custom text instead of the default text

        } else {
        
            // Close any existing textbox from the vanilla cousin dialogue, then bring up the custom one

            if (Plugin.isCurrentlyInDialogue) {
                Plugin.TriggerKingMessage.TerminateText();
            }
            Plugin.TriggerKingMessage.Initiate(Plugin.rollCheckDialogue, Plugin.defaultDialogueConditions);

        }

    }

    // ID 6
    // Oh, when you're just a little bigger, \nyou could maybe roll Ace up. \nHere, this is Ace. A cousin to The Prince... \nOr a cousin once removed...? \nWe forget. \nCousin, kin, something like that. \nAll righty, time for you to challenge Ace head-on.
    // ID 7 is level clear 
}
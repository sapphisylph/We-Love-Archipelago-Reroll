using App.Katamari2;
using BepInEx;
using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using WeLoveArchipelago.Archipelago;
using WeLoveArchipelago.Utils;

namespace WeLoveArchipelago.Patcher;

public class KingDialogueCreator
{

    // Notes: 
    // \n is the newline character in C#
    // \\col[int] is the character used to change the color of the text

    

    public static byte GetItemColor(string itemClass) {
        
        byte itemColor;
        
        switch (itemClass) {

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

        return itemColor;

    }

    public static void CreateKingRollUpDialogue(int location) {
        
        // Don't show dialogue in the cousins level
        if (Plugin.currentStage == "MissionScene/itoko") {
            return;
        }

        // Don't show dialogue for ALAP5 unless it's one of the normal cousins in the level
        int cousinCheckId = location - Plugin.COUSIN_ID_OFFSET;
        if (Plugin.currentStage == "MissionScene/big5" && !LocationCheckHandler.ALAP5Cousins.Contains(Convert.ToSByte(cousinCheckId))) {
            return;
        }

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

        byte itemColor = GetItemColor(foundItemClass);



        // Now that the scouted item data has been collected and set to defaults if necessary, time to build the king message using the json data 


        byte descriptionColor = itemColor;

        if (ReadFiles.itemColors.ContainsKey(foundItemGame)) {
            
            if (ReadFiles.itemColors[foundItemGame].ContainsKey(foundItemName)) {
            
                descriptionColor = ReadFiles.itemColors[foundItemGame][foundItemName];
                // Set the description to have its custom-set color if defined in the json
                // Else, it defaults to the progression class color above

            }
        }

        if (displayedItem.EndsWith("<3")) {
            // Change Stardew hearts into heart characters
            int displayedItemLength = displayedItem.Length;
            string heartItem = displayedItem.Remove(displayedItemLength - 2);
            heartItem.Concat("\\col[1]ΓÖÑ");    // \\col[1] is a character that turns the following text red, and ΓÖÑ turns into a heart when displayed in-game
            displayedItem = heartItem;

        }

        Plugin.LogDebug("Retrieving item descriptions...");
        // Get a random description from the list of descriptions for the given item (or generic item)
        string foundItemDescription = ReadFiles.itemDescriptions[foundItemGame][foundItemName][Plugin.rand.Next(ReadFiles.itemDescriptions[foundItemGame][foundItemName].Count)];


        if (location >= Plugin.COUSIN_ID_OFFSET) {

            string firstPart = $"Oh!! You just rolled up \nsome weird \\col[{descriptionColor}]{foundItemDescription}\\col[0]!!\n";
            string secondPart = $"Wait... it's just \\col[{itemColor}]{displayedItem}\\col[0].\nOh, go back to \\col[4]{foundItemRecipient}\\col[0]'s world!";
    

            // This group below changes the dialogue based on Circumstances


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
            
                // Close any existing textbox from the vanilla dialogue, then bring up the custom one

                if (Plugin.isCurrentlyInDialogue) {
                    Plugin.KingMessage.TerminateText();
                }
                Plugin.KingMessage.Initiate(Plugin.rollCheckDialogue, Plugin.defaultDialogueConditions);

            }

        } else {

            // End-of-level dialogues
            Plugin.LogDebug("Creating end-of-level dialogue...");

            string firstPart = $"Oh? Done already, \\col[2]\\ouji[0]\\col[0]? \nGood, good. Wonderful, even. \nWhile you were rolling, We noticed that this \\col[6]katamari\\col[0] \nhad some weird \\col[{descriptionColor}]{foundItemDescription}\\col[0] stuck to it.\n";
            string secondPart = $"Wait... that was just \\col[{itemColor}]{displayedItem}\\col[0].\nWe'll send it back to \\col[4]{foundItemRecipient}\\col[0]'s world. \nAnyway, want to go home now?";
    

            // This group below changes the dialogue based on Circumstances


            if (foundItemDescription.EndsWith(" thing")) {   // If the item description ends with thing, separate out the "thing" so that only the other part is colored and the "thing" remains white like the rest of the text
                
                int descriptionLength = foundItemDescription.Length;
                string trimmedItemDescription = foundItemDescription.Remove(descriptionLength - 6);  // Remove the last 6 characters of the string (" thing")
                firstPart = $"Oh? Done already, \\col[2]\\ouji[0]\\col[0]? \nGood, good. Wonderful, even. \nWhile you were rolling, We noticed that this \\col[6]katamari\\col[0] \nhad some weird \\col[{descriptionColor}]{trimmedItemDescription}\\col[0] thing stuck to it.\n";

            }

            if (isRecipientMe == "True") {  // I know I could turn this back into a bool, but like who cares, I'm only using it for one comparison
                
                if (foundItemClass == "Advancement") {
                    // Afaik all the progression items in WLK are characters so far, while everything else is an object. So I'm using this distinction to determine which items to use they/them for and which ones to use it/its for
                    secondPart = $"Wait... that was just \\col[{itemColor}]{displayedItem}\\col[0].\nWe'll send them back to the select meadow. \nAnyway, want to go home now?";
                } else {
                    secondPart = $"Wait... that was just \\col[{itemColor}]{displayedItem}\\col[0].\nWe'll send it back to the select meadow. \nAnyway, want to go home now?"; 
                }
            }

            Plugin.rollCheckDialogue = firstPart + secondPart;

            Plugin.showNewRollCheckDialogue = true;
            // This bool is checked in a function in QoL.cs to display the custom text instead of the default text
            Plugin.LogDebug("End-of-level dialogue created!");



        }

    }



    // ID 6
    // Oh, when you're just a little bigger, \nyou could maybe roll Ace up. \nHere, this is Ace. A cousin to The Prince... \nOr a cousin once removed...? \nWe forget. \nCousin, kin, something like that. \nAll righty, time for you to challenge Ace head-on.
    // ID 7 is level clear 
}
using System.Text.Json;
using WeLoveArchipelago;
using System;
using System.IO;
using System.Collections.Generic;


namespace WeLoveArchipelago.Utils;

public class ReadFiles {
    
    
    public static Dictionary<string, Dictionary<string, List<string>>> itemDescriptions = new Dictionary<string, Dictionary<string, List<string>>>();
    
    public static Dictionary<string, Dictionary<string, byte>> itemColors = new Dictionary<string, Dictionary<string, byte>>();

    public static void ReadDescriptionJSONs() {

        // Load the item description for all games with a file in the KingItemDescriptions directory

        Plugin.LogDebug("Reading JSON description files...");

        if (!Directory.Exists("BepInEx/plugins/WeLoveArchipelago/KingItemDescriptions")) {
            Directory.CreateDirectory("BepInEx/plugins/WeLoveArchipelago/KingItemDescriptions");
        }

        string[] descriptionFiles = Directory.GetFiles("BepInEx/plugins/WeLoveArchipelago/KingItemDescriptions");

        if (descriptionFiles != null) {

            foreach (string filePath in descriptionFiles) {
                
                if (!filePath.EndsWith(".json")) { return; }    // Skip any files that aren't .json format

                try {

                    using StreamReader jsonReader = new StreamReader(filePath);
                    string jsonContents = jsonReader.ReadToEnd();
                    JSONGame gameDescriptions = JsonSerializer.Deserialize<JSONGame>(jsonContents);
                    
                    string game = gameDescriptions.game_name;

                    Plugin.LogDebug($"Adding descriptions for game '{game}' from {filePath}...");

                    if (gameDescriptions.default_descriptions != null) {

                        // If there exists a default for a game, add those to the dictionary under the generic item name "default_unknown" (which I intentionally made a bit ugly so it hopefully won't overlap with any actual item names)
                        gameDescriptions.descriptionsByItem["default_unknown"] = gameDescriptions.default_descriptions;

                    } 

                    foreach (var item in gameDescriptions.items) {
                        
                        foreach (string name in item.item_names) {

                            gameDescriptions.descriptionsByItem[name] = item.descriptions;

                            if (item.item_color != null) {

                                gameDescriptions.colorsByItem[name] = item.item_color.Value;

                            }
                        }

                    }

                    itemDescriptions.Add(game, gameDescriptions.descriptionsByItem);
                    itemColors.Add(game, gameDescriptions.colorsByItem);

                    
                    
                } catch (Exception e) {
                    Plugin.BepinLogger.LogError($"Failed to parse item descriptions JSON located at {filePath}. Check that your JSON formatting is correct. \nCaught exception: \n{e}");
                }
                
            }

            // Force-populate the dictionaries for unknown items in case the player deleted the unknown json files (or in case I forget to make them)

            List<string> defaultProgression = ["important thing", "purple-named thing", "unwall", "progression thing", "critical thing"];
            List<string> defaultUseful = ["helpful thing", "dark blue-named thing", "utility", "useful thing", "nice-to-have thing"];
            List<string> defaultTrap = ["problematic thing", "red-named thing", "setback", "trap-like thing", "awful thing"];
            List<string> defaultFiller = ["pointless thing", "light blue-named thing", "filler", "probably-useless thing", "common thing"];
            
            // If any individual item class is missing, add it back in
            if (!itemDescriptions.ContainsKey("Unknown (Progression)")) {    
                Dictionary<string, List<string>> defaultProgressionDescriptions = new Dictionary<string, List<string>>();
                defaultProgressionDescriptions["default_unknown"] = defaultProgression;
                itemDescriptions.Add("Unknown (Progression)", defaultProgressionDescriptions);
                // The second dictionaries need to be valid defined dictionaries, which makes this code a bit ugly with how I currently have it written, but whatever
                // I should probably rewrite this at some point to make "Unknown" be the game name and the item classes be the items, but I'm too deep into it now to turn back, so whatever
            }

            if (!itemDescriptions.ContainsKey("Unknown (Useful)")) {
                Dictionary<string, List<string>> defaultUsefulDescriptions = new Dictionary<string, List<string>>();
                defaultUsefulDescriptions["default_unknown"] = defaultUseful;
                itemDescriptions.Add("Unknown (Useful)", defaultUsefulDescriptions);
            }

            if (!itemDescriptions.ContainsKey("Unknown (Trap)")) {
                Dictionary<string, List<string>> defaultTrapDescriptions = new Dictionary<string, List<string>>();
                defaultTrapDescriptions["default_unknown"] = defaultTrap;
                itemDescriptions.Add("Unknown (Trap)", defaultTrapDescriptions);
            }

            if (!itemDescriptions.ContainsKey("Unknown (Filler)")) {
                Dictionary<string, List<string>> defaultFillerDescriptions = new Dictionary<string, List<string>>();
                defaultFillerDescriptions["default_unknown"] = defaultFiller;
                itemDescriptions.Add("Unknown (Filler)", defaultFillerDescriptions);
            }

        }

        
    }

    public static List<string> textTrapList = [];

    public static void GetTextTraps() {
        
        Plugin.LogDebug("Getting text trap files...");

        if (!Directory.Exists("BepInEx/plugins/WeLoveArchipelago/TextTraps")) {
            Directory.CreateDirectory("BepInEx/plugins/WeLoveArchipelago/TextTraps");
        }

        string[] textTrapFiles = Directory.GetFiles("BepInEx/plugins/WeLoveArchipelago/TextTraps");

        if (textTrapFiles != null) {    // TODO: This check doesn't actually work, make it work properly

            foreach (string filePath in textTrapFiles) {
                
                if (!filePath.EndsWith(".txt")) { return; }    // Skip any files that aren't .txt format

                try {

                    using StreamReader txtReader = new StreamReader(filePath);
                    string txtContents = txtReader.ReadToEnd();
                    textTrapList.Add(txtContents);
                    
                } catch (Exception e) {
                    Plugin.BepinLogger.LogError($"Failed to get text trap dialogue located at {filePath}. \nCaught exception: \n{e}");
                }
                

            }
        }
    }
}

public class JSONGame {
    public Dictionary<string, List<string>> descriptionsByItem = new Dictionary<string, List<string>>();
    public Dictionary<string, byte> colorsByItem = new Dictionary<string, byte>();
    public string game_name {get; set;}
    public List<JSONItem> items {get; set;}
    public List<string> default_descriptions {get; set;}
}

public class JSONItem {
    public List<string> item_names {get; set;}
    public List<string> descriptions {get; set;}
    public byte? item_color {get; set;}
}
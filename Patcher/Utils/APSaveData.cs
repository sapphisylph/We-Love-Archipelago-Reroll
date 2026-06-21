using System.Text.Json;
using WeLoveArchipelago;
using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine.ResourceManagement.Util;
using WeLoveArchipelago.Archipelago;

namespace WeLoveArchipelago.Utils;

public class APSaveData {
    
    public static string roomSeed;
    public static int currentUsedTextTraps;
    public static int currentUsedWishYouWereHereTraps;
    public static int currentUsedTimeStopTraps;
    public static int[] musicRandoOrder;
    public static bool musicRandoLoaded = false;
    public static string saveFilePath;
    
    public static async Task SaveAPDataToFile() {

        Plugin.LogDebug("Saving AP data...");

        if (!Directory.Exists("BepInEx/plugins/WeLoveArchipelago/APSaveData")) {
            Directory.CreateDirectory("BepInEx/plugins/WeLoveArchipelago/APSaveData");
        }

        try {
            
            var NewSave = new SavedData { 
                // receivedFans = Plugin.fans,
                // receivedCousins = Plugin.cousins,
                // receivedPresents = Plugin.presents,
                checkedLocations = ArchipelagoClient.checkedLocations,
                usedTextTraps = currentUsedTextTraps,
                usedWishYouWereHereTraps = currentUsedWishYouWereHereTraps,
                usedTimeStopTraps = currentUsedTimeStopTraps,
                musicRandoOrder = Plugin.musicRandoList
            };

            string jsonOutput = JsonSerializer.Serialize(NewSave, new JsonSerializerOptions { WriteIndented = false });
            await File.WriteAllTextAsync(saveFilePath, jsonOutput);

        } catch (Exception e) {
            Plugin.BepinLogger.LogError($"Error while writing save data: \n{e}");
        }


        Plugin.LogDebug("Data saved successfully!");

    }

    public static void LoadAPDataFromFile() {

        if (!Directory.Exists("BepInEx/plugins/WeLoveArchipelago/APSaveData")) {
            Directory.CreateDirectory("BepInEx/plugins/WeLoveArchipelago/APSaveData");
        }

        if (!File.Exists(saveFilePath)) {
            Plugin.BepinLogger.LogMessage("No save data found for current seed.");
            return;
        }

        Plugin.BepinLogger.LogMessage("Loading saved AP data...");

        try {

            using StreamReader jsonReader = new StreamReader(saveFilePath);
            string jsonContents = jsonReader.ReadToEnd();
            SavedData storedData = JsonSerializer.Deserialize<SavedData>(jsonContents);
        
            // Get stored trap information
            currentUsedTextTraps = storedData.usedTextTraps;
            currentUsedTimeStopTraps = storedData.usedTimeStopTraps;
            currentUsedWishYouWereHereTraps = storedData.usedWishYouWereHereTraps;

            // Get music rando order
            if (storedData.checkedLocations != null) {
                musicRandoOrder = storedData.musicRandoOrder;
                musicRandoLoaded = true;
            }

            // Retrieve all already-sent checks (if there are any) to reduce server strain 
            if (storedData.checkedLocations != null) {
                ArchipelagoClient.checkedLocations = storedData.checkedLocations;
            }

            Plugin.BepinLogger.LogMessage("AP save data successfully loaded!");

        } catch (Exception e) {

            Plugin.BepinLogger.LogError($"Error while reading AP save data at {saveFilePath}. \nException: \n{e}");

        }

    }


    public static void SaveUsedTrap(byte trapNumber) {
        
        switch (trapNumber)
        {
            case 0:
                currentUsedTextTraps += 1;
                break;
            case 1:
                currentUsedWishYouWereHereTraps += 1;
                break;
            case 2:
                currentUsedTimeStopTraps += 1;
                break;
            default:
                Plugin.BepinLogger.LogWarning($"Trap with ID {trapNumber} was not recognized and could not be saved to AP save data.");
                break;

        }

    }



}

public class SavedData {
       

    // I don't have a use for storing fan data yet since it's always retrieved from the server anyway, but I accidentally started making it, so I'm leaving it here in case I want to put it back in eventually
    // public List<int> receivedFans {get; set;}
    // public List<int> receivedCousins {get; set;} 
    // public List<int> receivedPresents {get; set;} 
    
    public List<int> checkedLocations {get; set;}

    public int usedTextTraps {get; set;}
    public int usedWishYouWereHereTraps {get; set;}
    public int usedTimeStopTraps {get; set;}

    public int[] musicRandoOrder {get; set;}



}
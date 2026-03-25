using BepInEx;
using BepInEx.Logging;
using WeLoveArchipelago.Archipelago;
using WeLoveArchipelago.Patcher;
using BepInEx.Unity.IL2CPP;
using UnityEngine;
using HarmonyLib;
using System.Collections.Generic;
using BepInEx.Configuration;
using App.Katamari2;
// using WeLoveArchipelago.Utils;

namespace WeLoveArchipelago;

[BepInPlugin(PluginGUID, PluginName, PluginVersion)]
public class Plugin : BasePlugin
{
    public const string PluginGUID = "com.Zachamari.WeLoveArchipelago";
    public const string PluginName = "WeLoveArchipelago";
    public const string PluginVersion = "0.0.1";

    public const string ModDisplayInfo = $"{PluginName} v{PluginVersion}";
    private const string APDisplayInfo = $"Archipelago v{ArchipelagoClient.APVersion}";
    public static ManualLogSource BepinLogger;
    public static ArchipelagoClient APClient;

    public static List<int> fans = [];
    public static List<int> cousins = [];
    public static List<int> presents = [];
    
    public static int FAN_ID_OFFSET = 1;
    public static int COUSIN_ID_OFFSET = 100;
    public static int PRESENT_ID_OFFSET = 200;
    public static int MISSION_ID_OFFSET = 1;
    public static int PRESENT_LOCATION_ID_OFFSET = 200;
    public static int SHOOTING_STAR_ID_OFFSET = 300;
    public static int SUPER_CLEAR_ID_OFFSET = 400;
    public static int COUSINSANITY_ID_OFFSET = 500;
    public static int PRESENTSANITY_ID_OFFSET = 600;

    
    public static int[] musicRandoList = new int[35];
    public static bool musicRandoEnabled = false;
    public static bool turnOffGPS = false;

    public static string currentStage = "SceneMain";

    public override void Load()
    {
        // Plugin startup logic
        BepinLogger = Log;
        // ArchipelagoConsole.Awake();

        // I stole most of the below from the OUAK implementation, sorry Seafoamy :p
        // I'll change most of this anyway once I figure out how to change the ingame UI (if I can)

        // Load values from the config file
        ConfigEntry<string> uri = Config.Bind("Archipelago", "port", "archipelago.gg:12345", "Server address and port.");
        ConfigEntry<string> slotName = Config.Bind("Archipelago", "slotName", "Player1", "Your slot name (as defined in your YAML).");
        ConfigEntry<string> password = Config.Bind("Archipelago", "password", "", "Room password. Only put something here if the room has a password set, otherwise leave blank.");
        musicRandoEnabled = Config.Bind("Aesthetics", "musicRando", false, "Shuffles all in-game music tracks with each other, both in and out of levels.").Value;
        turnOffGPS = Config.Bind("Aesthetics", "turnOffGPS", false, "Gets rid of the indicator that shows up when you cross a size barrier telling you where you're supposed to go.").Value;

		// Connect to archipelago
		APClient = new ArchipelagoClient();
		ArchipelagoClient.ServerData.Uri = uri.Value;
		ArchipelagoClient.ServerData.SlotName = slotName.Value;
		ArchipelagoClient.ServerData.Password = password.Value;
		APClient.Connect();

        // Music Rando stuff, should probably be moved elsewhere later

        // Fill the musicRandoList array with song IDs 0 - 34 (the number of song IDs in the base game) in order
        for (int i = 0; i < musicRandoList.Length; i++) {
            musicRandoList[i] = i;
        } 
        // This is a Fisher-Yates randomizer algorithm, it reorders the list of song IDs randomly
        System.Random rand = new System.Random();
        int count = musicRandoList.Length;
        while (count > 1) {
            int i = rand.Next(count--);
            (musicRandoList[i], musicRandoList[count]) = (musicRandoList[count], musicRandoList[i]); // Switch the ID in spot number [count] with a random other ID in spot number [i]. Repeat for each ID in the array
        }

        Harmony.CreateAndPatchAll(typeof(LocationCheckHandler));
        Harmony.CreateAndPatchAll(typeof(ReceivedItemHandler));
        Harmony.CreateAndPatchAll(typeof(Fun));
        Harmony.CreateAndPatchAll(typeof(QoL));

        BepinLogger.LogMessage($"{ModDisplayInfo} was successfully loaded!");

    }

    // private void OnGUI()
    // {
    //     // show the mod is currently loaded in the corner
    //     GUI.Label(new Rect(16, 16, 300, 20), ModDisplayInfo);
    //     ArchipelagoConsole.OnGUI();

    //     string statusMessage;
    //     // show the Archipelago Version and whether we're connected or not
    //     if (ArchipelagoClient.Authenticated)
    //     {
    //         statusMessage = " Status: Connected";
    //         GUI.Label(new Rect(16, 50, 300, 20), APDisplayInfo + statusMessage);
    //     }
    //     else
    //     {
    //         statusMessage = " Status: Disconnected";
    //         GUI.Label(new Rect(16, 50, 300, 20), APDisplayInfo + statusMessage);
    //         GUI.Label(new Rect(16, 70, 150, 20), "Host: ");
    //         GUI.Label(new Rect(16, 90, 150, 20), "Player Name: ");
    //         GUI.Label(new Rect(16, 110, 150, 20), "Password: ");

    //         ArchipelagoClient.ServerData.Uri = GUI.TextField(new Rect(150, 70, 150, 20),
    //             ArchipelagoClient.ServerData.Uri);
    //         ArchipelagoClient.ServerData.SlotName = GUI.TextField(new Rect(150, 90, 150, 20),
    //             ArchipelagoClient.ServerData.SlotName);
    //         ArchipelagoClient.ServerData.Password = GUI.TextField(new Rect(150, 110, 150, 20),
    //             ArchipelagoClient.ServerData.Password);

    //         // requires that the player at least puts *something* in the slot name
    //         if (GUI.Button(new Rect(16, 130, 100, 20), "Connect") &&
    //             !ArchipelagoClient.ServerData.SlotName.IsNullOrWhiteSpace())
    //         {
    //             ArchipelagoClient.Connect();
    //         }
    //     }
    //     // this is a good place to create and add a bunch of debug buttons
    // }
}
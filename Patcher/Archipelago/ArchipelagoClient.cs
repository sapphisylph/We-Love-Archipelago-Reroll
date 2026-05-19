using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Models;
using Archipelago.MultiClient.Net.Packets;
using WeLoveArchipelago.Patcher;
using System.Threading.Tasks;
using WeLoveArchipelago.Utils;
using Archipelago.MultiClient.Net.Exceptions;
using System.Net.WebSockets;
// using WeLoveArchipelago.Utils;

namespace WeLoveArchipelago.Archipelago;

public class ArchipelagoClient
{
    public const string APVersion = "0.6.7";
    private const string Game = "We Love Katamari Reroll";

    public static bool Authenticated;
    private bool attemptingConnection;
    private static bool initialConnectionStepsComplete = false;


    public static ArchipelagoData ServerData = new();
    private DeathLinkHandler DeathLinkHandler;
    private ArchipelagoSession session;

    /// <summary>
    /// call to connect to an Archipelago session. Connection info should already be set up on ServerData
    /// </summary>
    /// <returns></returns>
    public void Connect()
    {
        if (Authenticated || attemptingConnection) return;

        try
        {
            session = ArchipelagoSessionFactory.CreateSession(ServerData.Uri);
            SetupSession();
        }
        catch (Exception e)
        {
            Plugin.BepinLogger.LogError(e);
        }

        TryConnect();
    }

    /// <summary>
    /// add handlers for Archipelago events
    /// </summary>
    private void SetupSession()
    {
        session.MessageLog.OnMessageReceived += message => Plugin.BepinLogger.LogMessage(message.ToString());
        session.Items.ItemReceived += OnItemReceived;
        session.Socket.ErrorReceived += OnSessionErrorReceived;
        session.Socket.SocketClosed += OnSessionSocketClosed;
    }

    /// <summary>
    /// attempt to connect to the server with our connection info
    /// </summary>
    private void TryConnect()
    {
        try
        {
            // it's safe to thread this function call but unity notoriously hates threading so do not use excessively
            ThreadPool.QueueUserWorkItem(
                _ => HandleConnectResult(
                    session.TryConnectAndLogin(
                        Game,
                        ServerData.SlotName,
                        ItemsHandlingFlags.AllItems, // TODO make sure to change this line
                        new Version(APVersion),
                        password: ServerData.Password,
                        requestSlotData: true // ServerData.NeedSlotData
                    )));
        }
        catch (Exception e)
        {
            Plugin.BepinLogger.LogError(e);
            HandleConnectResult(new LoginFailure(e.ToString()));
            attemptingConnection = false;
        }
    }

    /// <summary>
    /// handle the connection result and do things
    /// </summary>
    /// <param name="result"></param>
    private void HandleConnectResult(LoginResult result)
    {
        string outText;
        if (result.Successful)
        {
            var success = (LoginSuccessful)result;

            ServerData.SetupSession(success.SlotData, session.RoomState.Seed);
            Authenticated = true;

            DeathLinkHandler = new(session.CreateDeathLinkService(), ServerData.SlotName);
            session.Locations.CompleteLocationChecksAsync(ServerData.CheckedLocations.ToArray());
            outText = $"Successfully connected to {ServerData.Uri} as {ServerData.SlotName}!";

            Plugin.BepinLogger.LogMessage(outText);

            if (!initialConnectionStepsComplete) {
                
                // Everything here only needs to be done once upon the first connection per session. Using the reconnect door after the initial connection should skip these steps

                // Retrieve YAML settings from server 
                try {
                    Plugin.LogDebug("Getting YAML information from server...");
                    Plugin.cousinsAppearAnywhere = (long) success.SlotData["enable_alternative_cousin_logic"] == 1;
                    Plugin.LogDebug("Retrieved cousin logic setting.");
                    Plugin.selectedGoal = (long) success.SlotData["goal_option"];
                    Plugin.LogDebug("Retrieved goal option.");
                    if (Plugin.selectedGoal == 0 || Plugin.selectedGoal == 2) {
                        Plugin.LogDebug("Cousin hunt detected as goal - getting more information.");
                        Plugin.cousinHuntActive = true;
                        Plugin.cousinHuntPercentage = (long) success.SlotData["cousin_hunt_percentage"];
                        Plugin.LogDebug("Retrieved cousin hunt percentage.");
                        Plugin.cousinsInPool = (long) success.SlotData["cousin_amount"];
                        Plugin.LogDebug("Retrieved number of cousins in pool.");
                        Plugin.requiredCousins = Plugin.cousinsInPool * (Plugin.cousinHuntPercentage / 100);
                    }
                } catch (Exception e)
                {
                    Plugin.BepinLogger.LogError("Error while reading YAML information: \n" + e);
                }
                
                // Stole this bit from Hollow Knight's AP, sorry :p
                // This is used to get all of the slot's location data and store it for later
                Plugin.LogDebug("Scouting locations...");
                Task<Dictionary<long, ScoutedItemInfo>> scoutTask = session.Locations.ScoutLocationsAsync(session.Locations.AllLocations.ToArray());
                scoutTask.Wait();
                Plugin.LogDebug("Locations scouted!");
                Dictionary<long, ScoutedItemInfo> scoutResult = scoutTask.Result;
                    
                ProcessScoutedLocationData(scoutResult);
                
                // Get the seed from the server to make/get the save file name, then try to load it
                APSaveData.roomSeed = session.RoomState.Seed;
                APSaveData.saveFilePath = $"BepInEx/plugins/WeLoveArchipelago/APSaveData/AP_{APSaveData.roomSeed}.json";
                APSaveData.LoadAPDataFromFile();

                initialConnectionStepsComplete = true;
            }
        }
        else
        {
            var failure = (LoginFailure)result;
            outText = $"Failed to connect to {ServerData.Uri} as {ServerData.SlotName}.";
            outText = failure.Errors.Aggregate(outText, (current, error) => current + $"\n    {error}");

            Plugin.BepinLogger.LogError(outText);

            Authenticated = false;
            Disconnect();
        }

        Plugin.BepinLogger.LogMessage(outText);
        attemptingConnection = false;
    }


    public static Dictionary<int, List<string>> scoutedLocations = new Dictionary<int, List<string>>();

    public static void ProcessScoutedLocationData(Dictionary<long, ScoutedItemInfo> scoutResult){
        
        foreach (KeyValuePair<long, ScoutedItemInfo> scout in scoutResult)
        {
            try {

                int locationId = (int) scout.Key;   // I store it as an int here bc the location sending function uses integers and quite frankly I don't want to bother changing all that for a likely unnoticeable decrease in memory usage. Maybe later if I need to optimize it better
                Plugin.LogDebug($"Processing scouted location data for location {locationId}...");
                ScoutedItemInfo item = scout.Value;
                string itemName = item.ItemName ?? $"?Item {item.ItemId}";
                string receivingPlayer = item.Player.Alias ?? "someone else";
                string receivingGame = item.Player.Game ?? "Unknown Game";
                string itemClass = item.Flags.ToString() ?? "None";
                string isLocalItem = item.IsReceiverRelatedToActivePlayer.ToString();
                List<string> scoutedItemData = [itemName, itemClass, receivingPlayer, receivingGame, isLocalItem];
                scoutedLocations.Add(locationId, scoutedItemData);  // Store all the above information in a list that can be called using the location ID

            } catch (Exception e) {
                Plugin.LogDebug($"Error while collecting scouted location data! \n{e}");
            }
        }
    }

    /// <summary>
    /// something went wrong, or we need to properly disconnect from the server. cleanup and re null our session
    /// </summary>
    public void Disconnect()
    {
        try {
            Plugin.BepinLogger.LogDebug("Disconnecting from server...");
            session?.Socket.DisconnectAsync();
            session = null;
            Authenticated = false;
        } catch (Exception e) {
            Plugin.BepinLogger.LogError("Error while disconnecting from server: \n" + e);
            session = null;
            Authenticated = false;
        }
    }

    public void SendMessage(string message)
    {
        session.Socket.SendPacketAsync(new SayPacket { Text = message });
    }

    public static List<int> checkedLocations = []; 
    
	public void SendCheck(int location) {

        Plugin.LogDebug($"Sending check: {location}");

        // Plugin.LogDebug("Checked Locations:");
        // foreach (int i in checkedLocations) {
        //     string j = $"{i}";
        //     Plugin.LogDebug(j);
        // }


    
        if (location < 300) { // If the check is a level clear, present, or cousin, create the new King dialogue and set it to appear
            try {
                KingDialogueCreator.CreateKingRollUpDialogue(location); // This runs every attempted check so that the custom dialogue still appears on repeat playthroughs    
                ForceCousinsToAppearPatch.forcePresentSpawns = true;    // Force the present to spawn again if it was rolled up (for convenience)
            } catch (Exception e) { Plugin.LogDebug("Error while creating roll-up dialogue:\n" + e); }
        }

        if (!checkedLocations.Contains(location)) {

            try {

                session.Locations.CompleteLocationChecks(location);
                checkedLocations.Add(location);  // Add the location to the list of cached locations so it doesn't try to send the same check 9 billion times
                // If save data is cleared or edited, the memory will be cleared to allow sending the checks again


            } catch (NullReferenceException e) {

                Plugin.BepinLogger.LogError("Failed to send location check. The server may be down or you may have otherwise lost connection to the AP server. Check that the server is still running and reconnect. \n Full error message: \n" + e);    
            
            } catch (Exception e) {

                Plugin.BepinLogger.LogError($"An unexpected error occurred: {e}");

            }

        } else {

            Plugin.LogDebug($"Check {location} was already sent.");
        
        }
	}

	public void Goal() {
		session.SetGoalAchieved();
	}

	// public void CheckDeathLink(MainGameManager man) {
	// 	DeathLinkHandler.KillPlayer(man);
	// }

	public void SendDeathLink() {
		DeathLinkHandler.SendDeathLink();
	}


    /// <summary>
    /// we received an item so reward it here
    /// </summary>
    /// <param name="helper">item helper which we can grab our item from</param>
    private void OnItemReceived(ReceivedItemsHelper helper) {
        
        var receivedItem = helper.DequeueItem();

        if (helper.Index <= ServerData.Index) return;

        ServerData.Index++;

        // if items can be received while in an invalid state for actually handling them, they can be placed in a local
        // queue/collection to be handled later

        int itemId = (int) receivedItem.ItemId;
        Plugin.BepinLogger.LogMessage($"Received {receivedItem.ItemName} from {receivedItem.Player.Name}!");

        if (itemId >= Plugin.TRAP_ID_OFFSET) {

            int trapId = itemId - Plugin.TRAP_ID_OFFSET;

            if (trapId == 0) {
                TrapHandler.QueueDialogueTrap();
            } else if (trapId == 1) {
                TrapHandler.QueueWishYouWereHere(); 
            } else if (trapId == 2) {
                // Time Stop Trap
            } else if (trapId == 3) {
                // UI Loss Trap
            } else {
                Plugin.BepinLogger.LogMessage($"Received trap {receivedItem.ItemName} was not recognized. Contact the mod developer if you see this message! (ID = {itemId})");
            }
        }
        else

        if (itemId >= Plugin.FILLER_ID_OFFSET) {

            int fillerId = itemId - Plugin.FILLER_ID_OFFSET;

            if (fillerId == 0) {
                Patcher.ReceivedItemHandler.stardustQueue += 10;    // "Stardust" adds 10 stardust to the queue, which is later put ingame by a hooked function in ReceivedItemHandler
            } else if (fillerId == 1) {
                Patcher.ReceivedItemHandler.stardustQueue += 50;    // "Shining Stars" adds 50 stardust to the queue
            } else if (fillerId == 2) {
                Patcher.ReceivedItemHandler.stardustQueue += 100;   // "Bright Shining Stars" adds 100 stardust to the queue
            } else {
                Plugin.BepinLogger.LogMessage($"Received filler item {receivedItem.ItemName} was not recognized. Contact the mod developer if you see this message! (ID = {itemId})");
            }
        }

        else if (itemId >= Plugin.PRESENT_ID_OFFSET) {
            Plugin.presents.Add(itemId - Plugin.PRESENT_ID_OFFSET);
        }

        else if (itemId >= Plugin.COUSIN_ID_OFFSET) {
            Plugin.cousins.Add(itemId - Plugin.COUSIN_ID_OFFSET);
            // If the player has reached their cousin hunt goal, give the player every cousin and put every cousin into the collection
            if (Plugin.cousinHuntActive) {
                if (Plugin.cousins.Count >= Plugin.requiredCousins) {
                    for (int i = 0; i < 40; i++) {
                        Plugin.cousins.Add(i);
                    }
                    Plugin.LogDebug("Adding all cousins to the collection...");
                    foreach (List<int> list in ForceCousinsToAppearPatch.listOfCousinLists) {
                        ForceCousinsToAppearPatch.cousinsToForceIn.AddRange(list);
                    }
                    ForceCousinsToAppearPatch.queueForceNewCousinsSpawn = true;
                }
            }
        }

        else if (itemId >= Plugin.FAN_ID_OFFSET) {
            Plugin.fans.Add(itemId - Plugin.FAN_ID_OFFSET);
            if (!Plugin.cousinsAppearAnywhere) {
                ForceCousinsToAppearPatch.queueForceNewCousinsSpawn = true;
                ForceCousinsToAppearPatch.recentlyReceivedFans.Add(itemId - Plugin.FAN_ID_OFFSET);
            }
            // If the received fan was Mutsuo Hoshino and the player has already reached their cousin hunt goal, give the player every cousin and put every cousin into the collection
            if (itemId == (27 + Plugin.FAN_ID_OFFSET)) {
                if (Plugin.cousinHuntActive) {
                    if (Plugin.cousins.Count >= Plugin.requiredCousins) {
                        for (int i = 0; i < 40; i++) {
                            Plugin.cousins.Add(i);
                        }
                        Plugin.LogDebug("Adding all cousins to the collection...");
                        foreach (List<int> list in ForceCousinsToAppearPatch.listOfCousinLists) {
                            ForceCousinsToAppearPatch.cousinsToForceIn.AddRange(list);
                        }
                        ForceCousinsToAppearPatch.queueForceNewCousinsSpawn = true;
                    }
                }
            }
        }

        else {
            Plugin.BepinLogger.LogMessage($"Received item {receivedItem.ItemName} was not recognized. Contact the mod developer if you see this message! (ID = {itemId})");
        }
    }

    /// <summary>
    /// something went wrong with our socket connection
    /// </summary>
    /// <param name="e">thrown exception from our socket</param>
    /// <param name="message">message received from the server</param>
    private void OnSessionErrorReceived(Exception e, string message)
    {
        Plugin.BepinLogger.LogError(e);
        Plugin.BepinLogger.LogMessage(message);
    }

    /// <summary>
    /// something went wrong closing our connection. disconnect and clean up
    /// </summary>
    /// <param name="reason"></param>
    private void OnSessionSocketClosed(string reason)
    {
        Plugin.BepinLogger.LogError($"Connection to Archipelago lost: {reason}");
        Disconnect();
    }
}
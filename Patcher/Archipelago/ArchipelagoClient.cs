using System;
using System.Linq;
using System.Threading;
using Archipelago.MultiClient.Net;
using Archipelago.MultiClient.Net.BounceFeatures.DeathLink;
using Archipelago.MultiClient.Net.Enums;
using Archipelago.MultiClient.Net.Helpers;
using Archipelago.MultiClient.Net.Packets;
using WeLoveArchipelago.Patcher;
// using WeLoveArchipelago.Utils;

namespace WeLoveArchipelago.Archipelago;

public class ArchipelagoClient
{
    public const string APVersion = "0.6.6";
    private const string Game = "We Love Katamari Reroll";

    public static bool Authenticated;
    private bool attemptingConnection;

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

    /// <summary>
    /// something went wrong, or we need to properly disconnect from the server. cleanup and re null our session
    /// </summary>
    private void Disconnect()
    {
        Plugin.BepinLogger.LogDebug("disconnecting from server...");
        session?.Socket.DisconnectAsync();
        session = null;
        Authenticated = false;
    }

    public void SendMessage(string message)
    {
        session.Socket.SendPacketAsync(new SayPacket { Text = message });
    }


	public void SendCheck(int location) {
        Plugin.BepinLogger.LogMessage($"Sending check: {location}");
		session.Locations.CompleteLocationChecks(location);
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


            // Trap stuff (currently broken)


                // if (itemId >= Plugin.TRAP_ID_OFFSET) {

                //     int trapId = itemId - Plugin.TRAP_ID_OFFSET;

                    // TODO: Put these into a queue rather than letting them sit here

                //     if (trapId == 0) {
                //         TrapHandler.TriggerDialogueTrap();
                //     } else if (trapId == 1) {
                //         TrapHandler.WishYouWereHere((byte)Plugin.rand.Next(7)); // choose a random number from 0 to 7, and trigger that photo frame to appear (i made this function use bytes bc lower memory requirements, that's why the (byte) is there)
                //     } else if (trapId == 2) {
                //         // Time Stop Trap
                //     } else if (trapId == 3) {
                //         // UI Loss Trap
                //     } else {
                //         Plugin.BepinLogger.LogMessage($"Received trap {receivedItem.ItemName} was not recognized. Contact the mod developer if you see this message! (ID = {itemId})");
                //     }
                // }
                // else

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
        }

        else if (itemId >= Plugin.FAN_ID_OFFSET) {
            Plugin.fans.Add(itemId - Plugin.FAN_ID_OFFSET);
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
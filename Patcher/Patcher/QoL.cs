using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using App.Katamari2;
using Archipelago.MultiClient.Net.Packets;
using HarmonyLib;
using Il2CppSystem.Runtime.InteropServices;
using Il2CppSystem.Threading;
using UnityEngine;
using WeLoveArchipelago.Archipelago;

namespace WeLoveArchipelago.Patcher;

public class QoL {




    [HarmonyPatch(typeof(NextArrow), nameof(NextArrow.SetVisible)), HarmonyPrefix]
    public static bool TurnOffGPS(ref bool __0) {  // Disables the guide that appears at the top of the screen telling you how to go to the next area. I'll likely make this an optional setting, I just have an unreasonable disdain for this thing
        if (Plugin.turnOffGPS) {
            if (__0 == true) {
                return false;
            } else { return true; }
        } else { return true; }
    }
    
    static string[] fillerDialogue = ["blah blah blah", "yadda yadda yadda", "et cetera, et cetera", "whatever whatever", "words words words", "dialogue", "more text", "alan please add dialogue", "zzzzzzzzzzzzzz", "lalalalalala~", "nana nanananana na na nana na nana naaaa", "ok", "yeah", "mhm", "yep", "you win!!!!!", "*thumbs up emoji*", "we are the king", "stop skipping our text!!!", "witty remark", "something something cosmos", "katamari something something damacy", "woah you collected lots of whatever", "...", "something something stardust", "something something presents", "something something cousins", "katamari is cool", "katamari is awesome", "we love katamari!!!", "balatro"];

    [HarmonyPatch(typeof(UIOusamaMessage), nameof(UIOusamaMessage.DecodeText)), HarmonyPrefix]
    public static void ShortenText(ref string __0) {
        
        // Plugin.LogDebug($"Playing King dialogue: \n{__0}");

        if (Plugin.showNewRollCheckDialogue) {
            __0 = Plugin.rollCheckDialogue;
            Plugin.showNewRollCheckDialogue = false;
            return;
        }
        
        if (Plugin.quickText) {
            if (Plugin.currentStage == "Result") {
                __0 = fillerDialogue[Plugin.rand.Next(fillerDialogue.Length - 1)];  // Take a random entry from the array and use that as the dialogue instead
                return;
            }
        }

    }



    // Tutorial Skipping Stuff 
    // Some of this doesn't work, some of it is actively counterproductive, some of it just hasn't been tested

    // [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiTutoSetHiroba1st)), HarmonyPrefix]
    // public static bool RemoveIntroMeadowPopup() {
    //     return false;
    // }

    [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiTutoCheckHirobaAfterTutorial)), HarmonyPrefix]
    public static bool DontExplainWhatTheSelectMeadowIs(ref byte __result) {
        __result = 1;
        return false;
    }

    [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiTutoCheckHirobaMovie1st)), HarmonyPrefix]
    public static bool DontShowTheKingsBackstory(ref byte __result) {
        __result = 1;
        return false;
    }

    [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiTutoCheckHirobaPresent1st)), HarmonyPrefix]
    public static bool DontExplainWhatPresentsAre(ref byte __result) {
        __result = 1;
        return false;
    }


    // Turning the Quit Game door into a Reconnect to AP door

    [HarmonyPatch(typeof(SelectHiroba_DoorController), nameof(SelectHiroba_DoorController.Action)), HarmonyPrefix]
    public static bool TurnQuitDoorIntoReconnectDoor()
    {

        if (ArchipelagoClient.Authenticated) { 
            // This if statement should help prevent getting stuck in a disconnect/reconnect loop by alternating between disconnecting and reconnecting on every successive attempt
            Plugin.LogDebug("Disconnecting from server...");
            Plugin.APClient.Disconnect();   

        } else {

            Plugin.LogDebug("Attempting to reconnect to server...");
            Plugin.APClient.Connect();

        } 
        return false;
    }

    public static SelectHiroba_ObjectSelect_SuperClass DoorTextPanel = null;

    [HarmonyPatch(typeof(SelectHiroba_ObjectSelect_SuperClass), nameof(SelectHiroba_ObjectSelect_SuperClass.LoadText)), HarmonyPrefix]
    public static bool ChangeDoorText(SelectHiroba_ObjectSelect_SuperClass __instance, int __0)
    {
        try {
            if (__0 == 27)
            {

                if (DoorTextPanel == null) {
                    DoorTextPanel = __instance;
                }

                DoorTextPanel.panel_text = "TOGGLE AP CONNECTION";

                return false;

            } else { 

                return true; 

            }
        } catch (Exception e) {

            Plugin.BepinLogger.LogError("Error while creating AP door panel text: \n" + e);
            return true;

        }


    }



}
   
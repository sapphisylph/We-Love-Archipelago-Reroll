using App.Katamari2;
using HarmonyLib;
using System;
using UnityEngine;

namespace WeLoveArchipelago.Patcher;

public class TrapHandler {


    private static string defaultConditions = "kfuki_fix[2,85] \n kswing[8,50,6,40] \n pwait[20] \n wndcol[45,45,45,90]";
    private static string dialoguePrint = "We are the King. \n We are incredible!";


    public static void TriggerDialogueTrap() {
        UIOusamaMessage KingMessage = new UIOusamaMessage(); 
        KingMessage.Initiate(dialoguePrint, defaultConditions);
    }


    public static GameObject ChosenFrame = null;
    public static GameObject FrameUI = null;
    public static GameObject Frame00 = null;

    public static void WishYouWereHere(byte frameNumber) {
        string frameToFind = ("PhotoFrame/Canvas/Frame0" + frameNumber.ToString());
        ChosenFrame = GameObject.Find(frameToFind);
        if (ChosenFrame == null) {
            Plugin.BepinLogger.LogDebug("Unable to find object " + frameToFind);
        } else {
            FrameUI = GameObject.Find("PhotoFrame");
            Frame00 = GameObject.Find("PhotoFrame/Canvas/Frame00");     // Frame00 is active by default, so we need to disable it manually 
            Frame00.SetActive(false);
            FrameUI.SetActive(true);
            ChosenFrame.SetActive(true);    // I know I'm just turning Frame00 off and back on again if it's the one that was chosen, but it's not a horrible hit on performance

            // TODO: Make it turn off after a specified amount of time

        }

    }


}

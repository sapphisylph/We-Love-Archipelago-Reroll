using App.Katamari2;
using HarmonyLib;
using System;
using System.Collections.Generic;
using UnityEngine;
using WeLoveArchipelago.Utils;
using static WeLoveArchipelago.Plugin;

namespace WeLoveArchipelago.Patcher;

public class TrapHandler {


            // Testing stuff, remove later
            // else {
            //     StringBuilder multiLine = new StringBuilder();
            //     multiLine.AppendLine("This is Our dialogue.");
            //     multiLine.AppendLine("This is a second line.");
            //     multiLine.AppendLine("And this'll be in");
            //     multiLine.AppendLine("a second textbox.");
            //     multiLine.AppendLine("Thrilling.");
            //     multiLine.AppendLine("");
            //     multiLine.AppendLine("Truly thrilling.");
            //     __0 = multiLine.ToString();
            // }




    public static void QueueDialogueTrap() {

        try {

            Plugin.LogDebug("Adding a dialogue trap to the queue...");
            Plugin.LogDebug($"Current size of queue: {queuedDialogueTraps}");
            queuedDialogueTraps += 1;
            Plugin.LogDebug("Toggling the queue...");
            areAnyTrapsQueued = true;
            Plugin.LogDebug("Adding to the total...");
            queuedTrapsTotal += 1;
            Plugin.LogDebug("Dialogue trap successfully added to queue!");

        } catch (Exception e) {

            Plugin.LogDebug("Exception caught: " + e);

        }
    }

    public static void QueueWishYouWereHere() {

        queuedWishYouWereHereTraps += 1;
        areAnyTrapsQueued = true;
        queuedTrapsTotal += 1;

    }

    [HarmonyPatch(typeof(MissionSceneManager), nameof(MissionSceneManager.Tick)), HarmonyPrefix]    // This function runs every tick while in a level, so I can use it to trigger traps while only in a level
    public static void SetOffTraps() {

        if (areAnyTrapsQueued) {

            if (isCurrentlyInALevel) {  // Don't set off traps if the player doesn't have control of the katamari yet

                if (!isCurrentlyInDialogue) { // Don't set off traps if the player is currently being talked to by the King

                    if (queuedDialogueTraps > 0) {
                        
                        Plugin.LogDebug("Playing dialogue trap...");

                        try {

                            int randomNumber = Plugin.rand.Next(ReadFiles.textTrapList.Count);
                            dialoguePrint = ReadFiles.textTrapList[randomNumber];
                            string dialogueConditions = defaultDialogueConditions;


                            dialogueConditions = "kfuki_fix[1,85] \nkswing[8,50,6,40] \npwait[20] \nwndcol[110,30,30,95]";
                            Plugin.LogDebug("Conditions: " + defaultDialogueConditions);

                            TriggerKingMessage.Initiate(dialoguePrint, dialogueConditions);

                        } catch (Exception e) {
                            Plugin.LogDebug("Exception caught: " + e);
                        }
                    
                        Plugin.queuedDialogueTraps -= 1;

                    }

                    if (queuedWishYouWereHereTraps > 0) {
                        
                        if (trapTimer == 0) {
                            
                            frameNumber = rand.Next(7); // choose a random number from 0 to 7, and trigger that photo frame to appear

                            string frameToFind = ("PhotoFrame/Canvas/Frame0" + frameNumber.ToString());

                            Plugin.LogDebug($"Finding {frameToFind}");
                            ChosenFrame = GameObject.Find(frameToFind);

                            if (ChosenFrame == null) {
                                Plugin.BepinLogger.LogDebug("Unable to find object " + frameToFind);
                            } else {

                                FrameUI = GameObject.Find("PhotoFrame");
                                Frame00 = GameObject.Find("PhotoFrame/Canvas/Frame00");     // Frame00 is active by default, so we need to disable it manually 
                                Frame00.SetActive(false);
                                FrameUI.SetActive(true);
                                ChosenFrame.SetActive(true);    // I know I'm just turning Frame00 off and back on again if it's the one that was chosen, but it's not a horrible hit on performance

                                trapTimer += 1;
                                trapTimerLimit = rand.Next(600, 1200);   // Set the limit somewhere between 600 and 1200 frames (at 60fps, between 10 and 20 seconds. idk if this game runs at 60fps honestly but whatever)

                            }

                        } else {
                            
                            if (trapTimer >= trapTimerLimit) {

                                FrameUI.SetActive(false);                      
                                ChosenFrame.SetActive(false);

                                queuedWishYouWereHereTraps -= 1;
                                
                            } else {
                                
                                trapTimer += 1;
                            
                            }

                        }

                    } 
                    // Put other trap triggers here


                    queuedTrapsTotal -= 1;
                    if (queuedTrapsTotal == 0) {
                        areAnyTrapsQueued = false;
                    }
                }
            }
        }
    }



}

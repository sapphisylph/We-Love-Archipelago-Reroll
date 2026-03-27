using App.Katamari2;
using HarmonyLib;
using System;
using UnityEngine;
using BepInEx.Configuration;

namespace WeLoveArchipelago.Patcher;

public class DebugCommands {

    public static void DebugAddFansToMeadow() {    
        for (int i = 0; i < 30; i++) {
            Plugin.fans.Add(i);
        }
    }

    public static void DebugAddPresentsToMeadow() {    
        for (int i = 0; i < 40; i++) {
            Plugin.presents.Add(i);
        }
    }

    public static void DebugAddCousinsToMeadow() {    
        for (int i = 0; i < 40; i++) {
            Plugin.cousins.Add(i);
        }
    }



}
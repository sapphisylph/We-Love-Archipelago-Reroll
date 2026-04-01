using App.Katamari2;
using HarmonyLib;

namespace WeLoveArchipelago.Patcher;

public class ReceivedItemHandler {
    

    public static uint stardustQueue = 0; 


    [HarmonyPatch(typeof(Save_Data), nameof(Save_Data.GetFanIsAlive)), HarmonyPrefix]
    public static bool FanSpawner(ref bool __result, int fan_index, ref int clear_count) {
        clear_count = 25;       // Not sure if this is required
        if (Plugin.fans.Contains(fan_index)) {
            __result = true;    // if fan has been received, force them to spawn in
            App.Katamari2.Game.SetFanState(fan_index, 3);   // fans spawn in a broken state if brought in early, this makes them look normal 
            // ^ (eventually I want to make this be an indicator for if a check remains in the level, but that's for later) 
        } else {
            __result = false;
        }
        return false;   // skip the original function so it can't mess with my patch
    }


    [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiGameSetOujiGet)), HarmonyPrefix]
    public static void CousinSpawner(int _id, ref byte _s) {
        if (Plugin.cousins.Contains(_id)) {
            _s = 1;     // Set state to allow them to be spawned in if they're received
        } else {
            _s = 0;     // else, send them to the principal's office to have them EXPELLED!!!!
        }
    }


    [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiGameCheckPresent)), HarmonyPrefix]
    public static bool PresentSpawner(ref byte __result, int _pre) {
        if (Plugin.presents.Contains(_pre)) {
            __result = 1;
        } else {
            __result = 0;
        }
        return false;
    }


    [HarmonyPatch(typeof(Game), nameof(Game.mYm_SiGameSetStarDustCount)), HarmonyPrefix]
    public static void AddStardust(ref uint _n) {
        _n += stardustQueue;    // Add any queued stardust from the multiworld to the amount in-game
        stardustQueue = 0;      // Reset the queue
    }

}
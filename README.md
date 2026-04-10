# We-Love-Archipelago-Reroll
An Archipelago Multiworld Randomizer implementation for We Love Katamari Reroll + Royal Reverie

## Setup Guide
If you're unfamiliar with Archipelago, refer to [the Archipelago setup guide](https://archipelago.gg/tutorial/Archipelago/setup_en) for how to generate and host a randomized multiworld. 
The apworld and plugin for WLKRR can be found in the [releases](https://github.com/sapphisylph/We-Love-Archipelago-Reroll/releases/latest/).

### Mod Installation
1. Download the [latest version of BepInEx 6.0](https://builds.bepinex.dev/projects/bepinex_be).
   - Under the "Artifacts" tab, open the most recent dropdown menu and download the version labeled *"BepInEx Unity (IL2CPP) for Windows (x64) games"*.
   - Note: If you're on Linux, you'll need to run the game through Wine, so you should still use the Windows version.
2. Extract the contents of the bepinex .zip directly into the WLKRR folder (same folder as the game's .exe file). By default on Windows, this folder is located at `C:\Program Files (x86)\Steam\steamapps\common\WLKRR`.
3. Download the plugin from the [releases](https://github.com/sapphisylph/We-Love-Archipelago-Reroll/releases/latest/) and extract the contents of the .zip into `...\WLKRR\BepInEx\plugins`.
4. *(Linux only)* Add `WINEDLLOVERRIDES="winhttp.dll=n,b" %command%` to the launch options on Steam, under properties.  (< taken from the OUAK setup guide, currently untested for WLKRR)
5. Launch the game. If everything worked, some logs should be displayed by the plugin in the console window. This will also create a config file for you to put your connection info once the multiworld is created.

### Joining a Multiworld
1. Open the config file located at `...\WLKRR\BepInEx\config\com.Zachamari.WeLoveArchipelago.cfg` and input the server port, your slot name, and (optionally) password into their respective fields.
2. Launch the game. The game should attempt to connect to the server on startup, and this should be shown in the console window that opens alongside the game.
   - If no console window opens automatically, you can make it appear on startup by setting `[Logging.Console] Enabled = true` in the `BepInEx.cfg` file in the same config folder as before.
3. If this is your first time playing on this slot, start a new file. Otherwise, continue playing whichever file you connected with previously.
   - Note: This plugin may mess up existing save files if you load into them with the plugin active. If you have any saves that you care about, uninstall the plugin before opening them (and if you accidentally open an existing save with the plugin installed, you should force-close the game before it has a chance to save).
- If you get disconnected mid-game, there is currently no way to reconnect while the game is running, so you'll have to close and reopen the game to reconnect to the server.

## Known Issues
// Fill Out Later

# We Love Archipelago Reroll
An Archipelago implementation for We Love Katamari Reroll + Royal Reverie
   APworld created by @sapphisylph
   Plugin created by @zachamari

### Items
- Fans
- Cousins
- Presents
- Stardust

### Locations
- Level Clears
- Present and Cousin roll-ups
- Super Clears (optional)
- Shooting Stars (optional)


## Setup Guide
If you're new to Archipelago, refer to [the Archipelago setup guide](https://archipelago.gg/tutorial/Archipelago/setup_en) for how to generate and host a randomized multiworld. 
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
- The Select Meadow only updates received fans and cousins upon reload. If you receive an item while in the Select Meadow, you can reload it by entering and exiting the Favorite Music menu, the Collection, or any level.
- The fan selection menu in the Select Meadow doesn't update with receiving fans, so you will be automatically booted out of the menu if you try to open it to prevent being able to play the entire game out-of-logic
- Roll Up the Sun is currently impossible to clear on the first playthrough. The first failure is scripted to happen even if you already completed every level and rolled up enough stars to beat the level, so you'll have to play the level twice in order to goal.
- Super Clear and Shooting Star checks are sent on the results screen, while the level clear check is sent immediately upon winning, so don't panic if all the checks don't send immediately!

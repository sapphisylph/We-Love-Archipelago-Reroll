from BaseClasses import Tutorial
from worlds.AutoWorld import WebWorld

from .options import option_groups

class WeLoveKatamariRerollWebWorld(WebWorld):
    game = "We Love Katamari Reroll"
    theme = "grassFlowers"

    setup_en = Tutorial(
        "Multiworld Setup Guide",
        "A guide for setting up We Love Katamari Reroll for MultiWorld.",
        "English",
        "setup_en.md",
        "setup/en",
        ["sapphisylph", "Zachamari"],
    )

    tutorials = [setup_en]

    option_groups = option_groups
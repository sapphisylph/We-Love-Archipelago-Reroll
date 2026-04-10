from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import CollectionState
from worlds.generic.Rules import add_rule, set_rule

from . import data

if TYPE_CHECKING:
    from .world import WeLoveKatamariRerollWorld

def set_all_rules(world: WeLoveKatamariRerollWorld) -> None:
    set_completion_condition(world)
    set_all_entrance_rules(world)
    set_all_location_rules(world)

def set_all_entrance_rules(world: WeLoveKatamariRerollWorld) -> None:
    # I can't for the life of me figure out what the problem was with me using a for loop for this so we're DOING THINGS THE LONG WAY
    set_rule(world.get_entrance("Rainbow Girl Open in Select Meadow"), lambda state: state.has("Rainbow Girl", world.player))
    set_rule(world.get_entrance("Lazybones Open in Select Meadow"), lambda state: state.has("Lazybones", world.player))
    set_rule(world.get_entrance("Grandpa Open in Select Meadow"), lambda state: state.has("Grandpa", world.player))
    set_rule(world.get_entrance("Grandma Open in Select Meadow"), lambda state: state.has("Grandma", world.player))
    set_rule(world.get_entrance("Bird & Elephant Open in Select Meadow"), lambda state: state.has("Bird & Elephant", world.player))
    set_rule(world.get_entrance("Dog Open in Select Meadow"), lambda state: state.has("Dog", world.player))
    set_rule(world.get_entrance("Soccer Kid Open in Select Meadow"), lambda state: state.has("Soccer Kid", world.player))
    set_rule(world.get_entrance("Ikebana Teacher Open in Select Meadow"), lambda state: state.has("Ikebana Teacher", world.player))
    set_rule(world.get_entrance("Substitute Teacher Open in Select Meadow"), lambda state: state.has("Substitute Teacher", world.player))
    set_rule(world.get_entrance("F1 Racer Open in Select Meadow"), lambda state: state.has("F1 Racer", world.player))
    set_rule(world.get_entrance("Rain Coat Girl Open in Select Meadow"), lambda state: state.has("Rain Coat Girl", world.player))
    set_rule(world.get_entrance("Dr. Katamari Open in Select Meadow"), lambda state: state.has("Dr. Katamari", world.player))
    set_rule(world.get_entrance("Crane Hat Boy Open in Select Meadow"), lambda state: state.has("Crane Hat Boy", world.player))
    set_rule(world.get_entrance("Just-Right Girl Open in Select Meadow"), lambda state: state.has("Just-Right Girl", world.player))
    set_rule(world.get_entrance("Cowbear Farmer Open in Select Meadow"), lambda state: state.has("Cowbear Farmer", world.player))
    set_rule(world.get_entrance("Excited Baby Open in Select Meadow"), lambda state: state.has("Excited Baby", world.player))
    set_rule(world.get_entrance("Cleanly Mom Open in Select Meadow"), lambda state: state.has("Cleanly Mom", world.player))
    set_rule(world.get_entrance("Fund-raiser Open in Select Meadow"), lambda state: state.has("Fund-raiser", world.player))
    set_rule(world.get_entrance("Hansel & Gretel Open in Select Meadow"), lambda state: state.has("Hansel & Gretel", world.player))
    set_rule(world.get_entrance("Float Boy Open in Select Meadow"), lambda state: state.has("Float Boy", world.player))
    set_rule(world.get_entrance("Camper Man Open in Select Meadow"), lambda state: state.has("Camper Man", world.player))
    set_rule(world.get_entrance("Mini-Sumo Open in Select Meadow"), lambda state: state.has("Mini-Sumo", world.player))
    set_rule(world.get_entrance("Snow Child Open in Select Meadow"), lambda state: state.has("Snow Child", world.player))
    set_rule(world.get_entrance("Book Worm Open in Select Meadow"), lambda state: state.has("Book Worm", world.player))
    set_rule(world.get_entrance("Tomio Hoshino Open in Select Meadow"), lambda state: state.has("Tomio Hoshino", world.player))
#    set_rule(world.get_entrance("Mutsuo Hoshino Open in Select Meadow"), lambda state: state.has("Mutsuo Hoshino", world.player))

    set_rule(world.get_entrance("Collect Slip in Soccer Kid"), lambda state: state.has_from_list(["Ace", "Fund-raiser", "Mini-Sumo"], world.player, 1))

def set_all_location_rules(world: WeLoveKatamariRerollWorld) -> None:
    if bool(world.options.enable_alternative_cousin_logic.value):
        prince_rollup = world.get_location("Cousin: The Prince")
        set_rule(prince_rollup, lambda state: state.has_from_list(data.list_of_cousins, world.player, 1))

def set_completion_condition(world: WeLoveKatamariRerollWorld) -> None:
    world.multiworld.completion_condition[world.player] = lambda state: (state.count("Level Unlocked", world.player) >= 23 and state.has("Dog", world.player))
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
    def can_play_rainbow_girl(state: CollectionState) -> bool:
        return state.has("Rainbow Girl", world.player)
    set_rule(world.get_entrance("Rainbow Girl Open in Select Meadow"), can_play_rainbow_girl)

    def can_play_lazybones(state: CollectionState) -> bool:
        return state.has("Lazybones", world.player)
    set_rule(world.get_entrance("Lazybones Open in Select Meadow"), can_play_lazybones)

    def can_play_grandpa(state: CollectionState) -> bool:
        return state.has("Grandpa", world.player)
    set_rule(world.get_entrance("Grandpa Open in Select Meadow"), can_play_grandpa)

    def can_play_grandma(state: CollectionState) -> bool:
        return state.has("Grandma", world.player)
    set_rule(world.get_entrance("Grandma Open in Select Meadow"), can_play_grandma)

    def can_play_bird_and_elephant(state: CollectionState) -> bool:
        return state.has("Bird & Elephant", world.player)
    set_rule(world.get_entrance("Bird & Elephant Open in Select Meadow"), can_play_bird_and_elephant)

    def can_play_dog(state: CollectionState) -> bool:
        return state.has("Dog", world.player)
    set_rule(world.get_entrance("Dog Open in Select Meadow"), can_play_dog)

    def can_play_soccer_kid(state: CollectionState) -> bool:
        return state.has("Soccer Kid", world.player)
    set_rule(world.get_entrance("Soccer Kid Open in Select Meadow"), can_play_soccer_kid)

    def can_play_ikebana_teacher(state: CollectionState) -> bool:
        return state.has("Ikebana Teacher", world.player)
    set_rule(world.get_entrance("Ikebana Teacher Open in Select Meadow"), can_play_ikebana_teacher)

    def can_play_substitute_teacher(state: CollectionState) -> bool:
        return state.has("Substitute Teacher", world.player)
    set_rule(world.get_entrance("Substitute Teacher Open in Select Meadow"), can_play_substitute_teacher)

    def can_play_f1_racer(state: CollectionState) -> bool:
        return state.has("F1 Racer", world.player)
    set_rule(world.get_entrance("F1 Racer Open in Select Meadow"), can_play_f1_racer)

    def can_play_rain_coat_girl(state: CollectionState) -> bool:
        return state.has("Rain Coat Girl", world.player)
    set_rule(world.get_entrance("Rain Coat Girl Open in Select Meadow"), can_play_rain_coat_girl)

    def can_play_dr_katamari(state: CollectionState) -> bool:
        return state.has("Dr. Katamari", world.player)
    set_rule(world.get_entrance("Dr. Katamari Open in Select Meadow"), can_play_dr_katamari)

    def can_play_crane_hat_boy(state: CollectionState) -> bool:
        return state.has("Crane Hat Boy", world.player)
    set_rule(world.get_entrance("Crane Hat Boy Open in Select Meadow"), can_play_crane_hat_boy)

    def can_play_just_right_girl(state: CollectionState) -> bool:
        return state.has("Just-Right Girl", world.player)
    set_rule(world.get_entrance("Just-Right Girl Open in Select Meadow"), can_play_just_right_girl)

    def can_play_cowbear_farmer(state: CollectionState) -> bool:
        return state.has("Cowbear Farmer", world.player)
    set_rule(world.get_entrance("Cowbear Farmer Open in Select Meadow"), can_play_cowbear_farmer)

    def can_play_excited_baby(state: CollectionState) -> bool:
        return state.has("Excited Baby", world.player)
    set_rule(world.get_entrance("Excited Baby Open in Select Meadow"), can_play_excited_baby)

    def can_play_cleanly_mom(state: CollectionState) -> bool:
        return state.has("Cleanly Mom", world.player)
    set_rule(world.get_entrance("Cleanly Mom Open in Select Meadow"), can_play_cleanly_mom)

    def can_play_fund_raiser(state: CollectionState) -> bool:
        return state.has("Fund-raiser", world.player)
    set_rule(world.get_entrance("Fund-raiser Open in Select Meadow"), can_play_fund_raiser)

    def can_play_hansel_and_gretel(state: CollectionState) -> bool:
        return state.has("Hansel & Gretel", world.player)
    set_rule(world.get_entrance("Hansel & Gretel Open in Select Meadow"), can_play_hansel_and_gretel)

    def can_play_float_boy(state: CollectionState) -> bool:
        return state.has("Float Boy", world.player)
    set_rule(world.get_entrance("Float Boy Open in Select Meadow"), can_play_float_boy)

    def can_play_camper_man(state: CollectionState) -> bool:
        return state.has("Camper Man", world.player)
    set_rule(world.get_entrance("Camper Man Open in Select Meadow"), can_play_camper_man)

    def can_play_mini_sumo(state: CollectionState) -> bool:
        return state.has("Mini-Sumo", world.player)
    set_rule(world.get_entrance("Mini-Sumo Open in Select Meadow"), can_play_mini_sumo)

    def can_play_snow_child(state: CollectionState) -> bool:
        return state.has("Snow Child", world.player)
    set_rule(world.get_entrance("Snow Child Open in Select Meadow"), can_play_snow_child)

    def can_play_book_worm(state: CollectionState) -> bool:
        return state.has("Book Worm", world.player)
    set_rule(world.get_entrance("Book Worm Open in Select Meadow"), can_play_book_worm)

    def can_play_tomio_hoshino(state: CollectionState) -> bool:
        return state.has("Tomio Hoshino", world.player)
    set_rule(world.get_entrance("Tomio Hoshino Open in Select Meadow"), can_play_tomio_hoshino)

    def can_play_mutsuo_hoshino(state: CollectionState) -> bool:
        return state.has("Mutsuo Hoshino", world.player)
    set_rule(world.get_entrance("Mutsuo Hoshino Open in Select Meadow"), can_play_mutsuo_hoshino)

    def collect_slip_in_tutorial(state: CollectionState) -> bool:
        return state.has("Ace", world.player)
    set_rule(world.get_entrance("Collect Slip in Soccer Kid"), collect_slip_in_tutorial)

def set_all_location_rules(world: WeLoveKatamariRerollWorld) -> None:
    if bool(world.options.enable_alternative_cousin_logic.value):
        prince_rollup = world.get_location("Cousin: The Prince")
        set_rule(prince_rollup, lambda state: state.has_from_list(data.list_of_cousins, world.player, 1))

def set_completion_condition(world: WeLoveKatamariRerollWorld) -> None:
    world.multiworld.completion_condition[world.player] = lambda state: state.count("Level Unlocked", world.player) >= 23
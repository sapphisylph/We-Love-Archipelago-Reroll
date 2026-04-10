from __future__ import annotations

from logging import exception
from typing import TYPE_CHECKING

from BaseClasses import Item, ItemClassification

from . import data

if TYPE_CHECKING:
    from .world import WeLoveKatamariRerollWorld

class WeLoveKatamariRerollItem(Item):
    game = "We Love Katamari Reroll"

def get_random_filler_item_name(world: WeLoveKatamariRerollWorld) -> str:
#    if world.random.randint(0, 99) < world.options.trap_chance:
#        trap_item_id = world.random.randint(0, len(data.traps_items) - 1) + data.traps_offset
#        for trap_item in data.traps_items:
#            if trap_item_id == data.traps_items[trap_item]["ID"]:
#                return trap_item

    filler_item_id = world.random.randint(0, len(data.filler_items) - 1) + data.filler_offset
    for filler_item in data.filler_items:
        if filler_item_id == data.filler_items[filler_item]["ID"]:
            return filler_item

    return "Stardust"

def create_item_with_correct_classification(world: WeLoveKatamariRerollWorld, name: str) -> WeLoveKatamariRerollItem:
    classification = data.all_items[name]["classification"]
    if name != "Ace" and name in data.list_of_cousins and bool(world.options.enable_alternative_cousin_logic.value):
        classification = ItemClassification.progression_deprioritized_skip_balancing
    return WeLoveKatamariRerollItem(name, classification, data.all_items[name]["ID"], world.player)

def create_all_items(world: WeLoveKatamariRerollItem) -> None:
    itempool: list[Item] = []
    starting_fan = ""

    for fan in data.fans_items:
        if data.fans_items[fan]["ID"] == world.options.starting_level.value + data.fans_offset:
            starting_fan = fan
            continue
        itempool.append(world.create_item(fan))

    cousins_to_add = world.random.sample(list(data.cousins_items), world.options.cousin_amount)
    presents_to_add = world.random.sample(list(data.list_of_presents), world.options.present_amount)
    unfilled_locations = len(world.multiworld.get_unfilled_locations(world.player))

    if "Ace" not in cousins_to_add:
        cousins_to_add[world.random.randint(0, len(cousins_to_add) - 1)] = "Ace"
    for cousin in cousins_to_add:
        itempool.append(world.create_item(cousin))

    for present in presents_to_add:
        itempool.append(world.create_item(present))

    total_items = len(itempool)
    needed_filler = unfilled_locations - total_items

    itempool += [world.create_filler() for _ in range(needed_filler)]

    world.multiworld.itempool += itempool

    world.push_precollected(world.create_item(starting_fan))

def generate_item_groups() -> dict[str, list[str]]:
    item_groups: dict[str, list[str]] = {
        "Fans": list(data.fans_items.keys()),
        "Cousins": list(data.cousins_items.keys()),
        "Presents": list(data.presents_items.keys()),
        "Stars": list(data.filler_items.keys()),
        "Traps": list(data.traps_items.keys()),
    }

    return item_groups


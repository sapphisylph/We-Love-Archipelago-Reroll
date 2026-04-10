from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import Location

from . import data, items
if TYPE_CHECKING:
    from .world import WeLoveKatamariRerollWorld

class WeLoveKatamariRerollLocation(Location):
    game = "We Love Katamari Reroll"

def get_location_names_with_ids(location_names: list[str]) -> dict[str, int | None]:
    return {location_name: data.all_locations[location_name]["ID"] for location_name in location_names}

def create_all_locations(world: WeLoveKatamariRerollWorld) -> None:
    create_regular_locations(world)
    create_events(world)

def create_regular_locations(world: WeLoveKatamariRerollWorld) -> None:
    for location_name in list(data.base_locations):
        if location_name == "Cousin: The Prince" and not bool(world.options.enable_alternative_cousin_logic.value):
            continue
        if location_name == "Level Complete: Roll Up The Sun" or location_name == "Level Complete: Cousins":
            continue
        current_region = world.get_region(data.all_locations[location_name]["region"])
        location_to_add = get_location_names_with_ids([location_name])
        current_region.add_locations(location_to_add, WeLoveKatamariRerollLocation)

    if bool(world.options.enable_shooting_stars):
        for location_name in list(data.shooting_star_locations):
            current_region = world.get_region(data.shooting_star_locations[location_name]["region"])
            location_to_add = get_location_names_with_ids([location_name])
            current_region.add_locations(location_to_add, WeLoveKatamariRerollLocation)

    if world.options.enable_super_clears.value > 0:
        super_clears_to_add = data.super_clear_tier_1_locations

        if world.options.enable_super_clears.value > 1:
            super_clears_to_add.update(data.super_clear_tier_2_locations)

            if world.options.enable_super_clears.value > 2:
                super_clears_to_add.update(data.super_clear_tier_3_locations)

        for location_name in list(super_clears_to_add):
            current_region = world.get_region(super_clears_to_add[location_name]["region"])
            location_to_add = get_location_names_with_ids([location_name])
            current_region.add_locations(location_to_add, WeLoveKatamariRerollLocation)

def create_events(world: WeLoveKatamariRerollWorld) -> None:
    for fan in data.list_of_fans:
        if fan in ["Dog", "Mutsuo Hoshino", "Tomio Hoshino"]:
            continue
        fan_region = world.get_region(fan)
        fan_region.add_event(
            f"{fan} accessible", "Level Unlocked", location_type=WeLoveKatamariRerollLocation, item_type=items.WeLoveKatamariRerollItem
        )

def generate_location_groups() -> dict[str, list[str]]:
    location_groups: dict[str, list[str]] = {
        "Rainbow Girl": [name for name in list(data.all_locations) if "Rainbow Girl" in data.all_locations[name]["region"]],
        "Lazybones": [name for name in list(data.all_locations) if "Lazybones" in data.all_locations[name]["region"]],
        "Grandpa": [name for name in list(data.all_locations) if "Grandpa" in data.all_locations[name]["region"]],
        "Grandma": [name for name in list(data.all_locations) if "Grandma" in data.all_locations[name]["region"]],
        "Bird & Elephant": [name for name in list(data.all_locations) if "Bird & Elephant" in data.all_locations[name]["region"]],
        "Dog": [name for name in list(data.all_locations) if "Dog" in data.all_locations[name]["region"]],
        "Soccer Kid": [name for name in list(data.all_locations) if "Soccer Kid" in data.all_locations[name]["region"]],
        "Ikebana Teacher": [name for name in list(data.all_locations) if "Ikebana Teacher" in data.all_locations[name]["region"]],
        "Substitute Teacher": [name for name in list(data.all_locations) if "Substitute Teacher" in data.all_locations[name]["region"]],
        "F1 Racer": [name for name in list(data.all_locations) if "F1 Racer" in data.all_locations[name]["region"]],
        "Rain Coat Girl": [name for name in list(data.all_locations) if "Rain Coat Girl" in data.all_locations[name]["region"]],
        "Dr. Katamari": [name for name in list(data.all_locations) if "Dr. Katamari" in data.all_locations[name]["region"]],
        "Crane Hat Boy": [name for name in list(data.all_locations) if "Crane Hat Boy" in data.all_locations[name]["region"]],
        "Just-Right Girl": [name for name in list(data.all_locations) if "Just-Right Girl" in data.all_locations[name]["region"]],
        "Cowbear Farmer": [name for name in list(data.all_locations) if "Cowbear Farmer" in data.all_locations[name]["region"]],
        "Excited Baby": [name for name in list(data.all_locations) if "Excited Baby" in data.all_locations[name]["region"]],
        "Cleanly Mom": [name for name in list(data.all_locations) if "Cleanly Mom" in data.all_locations[name]["region"]],
        "Fund-raiser": [name for name in list(data.all_locations) if "Fund-raiser" in data.all_locations[name]["region"]],
        "Hansel & Gretel": [name for name in list(data.all_locations) if "Hansel & Gretel" in data.all_locations[name]["region"]],
        "Float Boy": [name for name in list(data.all_locations) if "Float Boy" in data.all_locations[name]["region"]],
        "Camper Man": [name for name in list(data.all_locations) if "Camper Man" in data.all_locations[name]["region"]],
        "Mini-Sumo": [name for name in list(data.all_locations) if "Mini-Sumo" in data.all_locations[name]["region"]],
        "Snow Child": [name for name in list(data.all_locations) if "Snow Child" in data.all_locations[name]["region"]],
        "Book Worm": [name for name in list(data.all_locations) if "Book Worm" in data.all_locations[name]["region"]],
        "Tomio Hoshino": [name for name in list(data.all_locations) if "Tomio Hoshino" in data.all_locations[name]["region"]],
        "Mutsuo Hoshino": [name for name in list(data.all_locations) if "Mutsuo Hoshino" in data.all_locations[name]["region"]],

        "Level Clears": list(data.level_clear_locations),
        "Presents": list(data.present_rollup_locations),
        "Cousins": list(data.cousin_rollup_locations),
        "Shooting Stars": list(data.shooting_star_locations),
        "Super Clears": list(data.super_clears_locations),

        "Challenging Super Clears": list(data.super_clear_tier_2_locations | data.super_clear_tier_3_locations),
        "Difficult Super Clears": list(data.super_clear_tier_3_locations),
    }

    for fan in list(data.fans_and_cousins_logic):
        location_groups[fan].extend(f"Cousin: {cousin}" for cousin in data.fans_and_cousins_logic[fan])

    return location_groups
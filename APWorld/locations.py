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
        if location_name == "Cousin: The Prince" and not data.FUTURE_YAML_OPTION_FOR_ALTERNATIVE_COUSIN_LOGIC:
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

    if bool(world.options.enable_super_clears):
        super_clears_to_add = data.super_clear_tier_1_locations
        if bool(world.options.enable_challenging_super_clears):
            super_clears_to_add.update(data.super_clear_tier_2_locations)
            if bool(world.options.enable_difficult_super_clears):
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
from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import Location

from . import data, items
if TYPE_CHECKING:
    from .world import WeLoveKatamariRerollWorld

class WeLoveKatamariRerollLocation(Location):
    game = "WeLoveKatamariReroll"

def get_location_names_with_ids(location_names: list[str]) -> dict[str, int | None]:
    return {location_name: data.all_locations[location_name]["ID"] for location_name in location_names}

def create_all_locations(world: WeLoveKatamariRerollWorld) -> None:
    create_regular_locations(world)
    create_events(world)

def create_regular_locations(world: WeLoveKatamariRerollWorld) -> None:
    for location_name in data.all_locations:
        if location_name == "Roll Up The Prince" and not data.FUTURE_YAML_OPTION_FOR_ALTERNATIVE_COUSIN_LOGIC:
            continue
        current_region = world.get_region(data.all_locations[location_name]["region"])
        location_to_add = get_location_names_with_ids([location_name])
        current_region.add_locations(location_to_add, WeLoveKatamariRerollLocation)

def create_events(world: WeLoveKatamariRerollWorld) -> None:
    for fan in data.list_of_fans:
        fan_region = world.get_region(fan)
        fan_region.add_event(
            f"{fan} accessible", "Level Unlocked", location_type=WeLoveKatamariRerollLocation, item_type=items.WeLoveKatamariRerollItem
        )
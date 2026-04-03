from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import Entrance, Region

from . import data
if TYPE_CHECKING:
    from .world import WeLoveKatamariRerollWorld

def create_and_connect_regions(world: WeLoveKatamariRerollWorld) -> None:
    create_all_regions(world)
    connect_regions(world)

def create_all_regions(world: WeLoveKatamariRerollWorld) -> None:
    regions = [Region("Select Meadow", world.player, world.multiworld)]

    for fan in data.list_of_fans:
        regions.append(Region(fan, world.player, world.multiworld))

    for cousin in data.list_of_cousins:
        regions.append(Region(f"{cousin} Collect", world.player, world.multiworld))

    world.multiworld.regions += regions


def connect_regions(world: WeLoveKatamariRerollWorld) -> None:
    select_meadow = world.get_region("Select Meadow")
    cousins_logic = data.get_cousins_logic(world)

    for fan in cousins_logic:
        fan_region = world.get_region(fan)
        select_meadow.connect(fan_region, f"{fan} Open in Select Meadow")
        for cousin in set(cousins_logic[fan]):
            fan_region.connect(world.get_region(f"{cousin} Collect"), f"Collect {cousin} in {fan}")

    if not bool(world.options.enable_alternative_cousin_logic.value):
        select_meadow.connect(world.get_region("The Prince Collect"), "Collect The Prince in Select Meadow")

from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import CollectionState
from worlds.generic.Rules import add_rule, set_rule

if TYPE_CHECKING:
    from .world import WeLoveKatamariRerollWorld

def set_all_rules(world: WeLoveKatamariRerollWorld):
    set_completion_condition(world)

def set_completion_condition(world: WeLoveKatamariRerollWorld):
    world.multiworld.completion_condition[world.player] = lambda state: state.has_all("Level Unlocked", world.player)
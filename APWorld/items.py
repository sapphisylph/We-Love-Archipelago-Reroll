from __future__ import annotations

from typing import TYPE_CHECKING

from BaseClasses import Item, ItemClassification

from . import data

if TYPE_CHECKING:
    from .world import WeLoveKatamariRerollWorld

class WeLoveKatamariRerollItem(Item):
    game = "WeLoveKatamariReroll"


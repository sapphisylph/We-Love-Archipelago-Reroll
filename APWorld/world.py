from worlds.AutoWorld import World

from . import locations, items, regions, rules, webworld, data
from . import options as wlk_options

class WeLoveKatamariRerollWorld(World):
    """
    We Love Katamari Reroll
    """
    game = "We Love Katamari Reroll"
    web = webworld.WeLoveKatamariRerollWebWorld()
    options_dataclass = wlk_options.WeLoveKatamariRerollOptions
    options: wlk_options.WeLoveKatamariRerollOptions

    location_name_to_id = {location_name: data.all_locations[location_name]["ID"] for location_name in data.all_locations}
    location_name_groups = locations.generate_location_groups()
    item_name_to_id = {item_name: data.all_items[item_name]["ID"] for item_name in data.all_items}
    item_name_groups = items.generate_item_groups()

    origin_region_name = "Select Meadow"

    def create_regions(self) -> None:
        regions.create_and_connect_regions(self)
        locations.create_all_locations(self)

    def set_rules(self) -> None:
        rules.set_all_rules(self)

    def create_items(self) -> None:
        items.create_all_items(self)

    def create_item(self, name: str) -> items.WeLoveKatamariRerollItem:
        return items.create_item_with_correct_classification(self, name)

    def get_filler_item_name(self) -> str:
        return items.get_random_filler_item_name(self)

    def fill_slot_data(self):
        return {
            "enable_alternative_cousin_logic": self.options.enable_alternative_cousin_logic.value,
        }
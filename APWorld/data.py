from BaseClasses import ItemClassification

list_of_fans = [
    "Rainbow Girl",
    "Lazybones",
    "Grandpa",
    "Grandma",
    "Bird & Elephant",
    "Dog",
    "Soccer Kid",
    "Ikebana Teacher",
    "Substitute Teacher",
    "F1 Racer",
    "Rain Coat Girl",
    "Dr. Katamari",
    "Crane Hat Boy",
    "Just-Right Girl",
    "Cowbear Farmer",
    "Excited Baby",
    "Cleanly Mom",
    "Fund-raiser",
    "Hansel & Gretel",
    "Float Boy",
    "Camper Man",
    "Mini-Sumo",
    "Snow Child",
    "Book Worm",
    "Tomio Hoshino",
    "Mutsuo Hoshino"
]

list_of_cousins = [
    "The Prince",
    "Lalala",
    "Nik",
    "Ace",
    "Johnson",
    "Velvet",
    "Fujio",
    "Havana",
    "Peso",
    "Shikao",
    "Odeko",
    "Honey",
    "Marny",
    "Kuro",
    "Foomin",
    "June",
    "Ichigo",
    "Marcy",
    "Njamo",
    "Dipp",
    "Opeo",
    "Nickel",
    "Jungle",
    "Miso",
    "Twinkle",
    "Huey",
    "Nutsuo",
    "Beyond",
    "Kinoko",
    "Macho",
    "L'Amour",
    "Daisy",
    "Lucha",
    "Miki",
    "Odeon",
    "Can-Can",
    "Shy",
    "Slip",
    "Drooby",
    "Signolo"
]

list_of_presents = [
    "Headphones",
    "Crown",
    "Giraffe Hat",
    "Ribbons",
    "Flower",
    "Little Buddy",
    "Mt. Fuji",
    "Mushroom",
    "Antenna",
    "Peacock Feathers",
    "Horsey",
    "Bikini",
    "Camera",
    "Pochette",
    "Scarf",
    "Guitar",
    "Cool Belt",
    "Moustache",
    "Trumpet",
    "Glasses",
    "Wig",
    "Royal Mask",
    "Mask",
    "Bunny Ears",
    "Long Nose",
    "Pencil",
    "Tutu",
    "Tire",
    "Singlet",
    "Note Glasses",
    "Little Prince",
    "Little King"
]

fans_and_cousins_logic = {
    "Rainbow Girl": ["Macho"],
    "Lazybones": ["Can-Can", "Havana"],
    "Grandpa": ["Johnson", "Twinkle", "Kuro"],
    "Grandma": ["Foomin", "Miki", "Velvet"],
    "Bird & Elephant": ["Shikao", "Lucha", "Nutsuo", "Drooby"],
    "Dog": [],
    "Soccer Kid": ["Ace", "Slip"],
    "Ikebana Teacher": ["Ichigo"],
    "Substitute Teacher": ["Miso", "Huey"],
    "F1 Racer": ["Odeko", "Shy", "Nickel"],
    "Rain Coat Girl": ["June", "Fujio"],
    "Dr. Katamari": ["Beyond", "Njamo"],
    "Crane Hat Boy": ["Opeo"],
    "Just-Right Girl": ["Marcy", "Peso", "Signolo"],
    "Cowbear Farmer": ["Daisy"],
    "Excited Baby": ["Jungle"],
    "Cleanly Mom": ["L'Amour"],
    "Fund-raiser": ["Odeon"],
    "Hansel & Gretel": ["Honey"],
    "Float Boy": ["Marny"],
    "Camper Man": ["Kinoko"],
    "Mini-Sumo": ["Nik"],
    "Snow Child": ["Lalala"],
    "Book Worm": ["Dipp"],
    "Tomio Hoshino": [],
    "Mutsuo Hoshino": []
}

FUTURE_YAML_OPTION_FOR_ALTERNATIVE_COUSIN_LOGIC = False
if FUTURE_YAML_OPTION_FOR_ALTERNATIVE_COUSIN_LOGIC:
    fans_and_cousins_logic["Rainbow Girl"].append("The Prince")
    fans_and_cousins_logic["Bird & Elephant"] = list_of_cousins
    fans_and_cousins_logic["Ikebana Teacher"].append("L'Amour")
    fans_and_cousins_logic["Crane Hat Boy"].append("Fujio")
    fans_and_cousins_logic["Just-Right Girl"].append("Nutsuo")
    fans_and_cousins_logic["Cowbear Farmer"].append("Njamo")
    fans_and_cousins_logic["Excited Baby"].append("June")
    fans_and_cousins_logic["Cleanly Mom"].extend(["Kuro", "Havana", "Nickel"])
    fans_and_cousins_logic["Fund-raiser"].extend(["Twinkle", "Opeo", "Ichigo", "Can-Can", "Slip"])
    fans_and_cousins_logic["Float Boy"].append("Fujio")
    fans_and_cousins_logic["Mini-Sumo"].extend(["Kinoko", "Miso", "The Prince", "Slip"])
    fans_and_cousins_logic["Snow Child"].append("L'Amour")
    fans_and_cousins_logic["Book Worm"].extend(["Marny"])
    fans_and_cousins_logic["Mutsuo Hoshino"] = list_of_cousins

location_offset_ids = {
    "Level Clears": 1,
    "Cousins": 100,
    "Presents": 200,
    "Shooting Stars": 300,
    "Super Clears": 400,
    "Cousinsanity": 500,
    "Presentsanity": 600
}

item_offset_ids = {
    "Fans": 1,
    "Cousins": 100,
    "Presents": 200,
    "Filler": 1000,
    "Traps": 1100
}

level_clear_offset = location_offset_ids["Level Clears"]

level_clear_locations = {
    "Roll Up The Sun Complete": {
        "ID": 1 + level_clear_offset,
        "region": "Dog"
    },
    "Tutorial Complete": {
        "ID": 3 + level_clear_offset,
        "region": "Soccer Kid"
    },
    "ALAP1 Complete": {
        "ID": 4 + level_clear_offset,
        "region": "Rainbow Girl"
    },
    "ALAP2 Complete": {
        "ID": 5 + level_clear_offset,
        "region": "Lazybones"
    },
    "ALAP3 Complete": {
        "ID": 6 + level_clear_offset,
        "region": "Grandpa"
    },
    "ALAP4 Complete": {
        "ID": 7 + level_clear_offset,
        "region": "Grandma"
    },
    "ALAP5 Complete": {
        "ID": 8 + level_clear_offset,
        "region": "Bird & Elephant"
    },
    "Flowers Complete": {
        "ID": 9 + level_clear_offset,
        "region": "Ikebana Teacher"
    },
    "School Complete": {
        "ID": 10 + level_clear_offset,
        "region": "Substitute Teacher"
    },
    "Race Complete": {
        "ID": 11 + level_clear_offset,
        "region": "F1 Racer"
    },
    "Clouds Complete": {
        "ID": 12 + level_clear_offset,
        "region": "Rain Coat Girl"
    },
    "Friends Complete": {
        "ID": 13 + level_clear_offset,
        "region": "Dr. Katamari"
    },
    "Cranes Complete": {
        "ID": 14 + level_clear_offset,
        "region": "Crane Hat Boy"
    },
    "Small Just-Right Complete": {
        "ID": 15 + level_clear_offset,
        "region": "Just-Right Girl"
    },
    "Cowbear Complete": {
        "ID": 16 + level_clear_offset,
        "region": "Cowbear Farmer"
    },
    "50 Items Complete": {
        "ID": 17 + level_clear_offset,
        "region": "Excited Baby"
    },
    "Cleaning Complete": {
        "ID": 18 + level_clear_offset,
        "region": "Cleanly Mom"
    },
    "Money Complete": {
        "ID": 19 + level_clear_offset,
        "region": "Fund-raiser"
    },
    "Sweets Complete": {
        "ID": 20 + level_clear_offset,
        "region": "Hansel & Gretel"
    },
    "Underwater Complete": {
        "ID": 21 + level_clear_offset,
        "region": "Float Boy"
    },
    "Small Fire Complete": {
        "ID": 22 + level_clear_offset,
        "region": "Camper Man"
    },
    "Sumo 1 Complete": {
        "ID": 23 + level_clear_offset,
        "region": "Mini-Sumo"
    },
    "Snowman Complete": {
        "ID": 24 + level_clear_offset,
        "region": "Snow Child"
    },
    "Fireflies Complete": {
        "ID": 25 + level_clear_offset,
        "region": "Book Worm"
    },
    "Countries Complete": {
        "ID": 28 + level_clear_offset,
        "region": "Tomio Hoshino"
    },
    "Cousins Complete": {
        "ID": 29 + level_clear_offset,
        "region": "Mutsuo Hoshino"
    },
    "AFAP1 Complete": {
        "ID": 30 + level_clear_offset,
        "region": "Rainbow Girl"
    },
    "AFAP2 Complete": {
        "ID": 31 + level_clear_offset,
        "region": "Lazybones"
    },
    "AFAP3 Complete": {
        "ID": 32 + level_clear_offset,
        "region": "Grandpa"
    },
    "AFAP4 Complete": {
        "ID": 33 + level_clear_offset,
        "region": "Grandma"
    },
    "AFAP5 Complete": {
        "ID": 34 + level_clear_offset,
        "region": "Bird & Elephant"
    },
    "Fast Flowers Complete": {
        "ID": 35 + level_clear_offset,
        "region": "Ikebana Teacher"
    },
    "Students Complete": {
        "ID": 36 + level_clear_offset,
        "region": "Substitute Teacher"
    },
    "Fast Race Complete": {
        "ID": 37 + level_clear_offset,
        "region": "F1 Racer"
    },
    "Fast Friends Complete": {
        "ID": 38 + level_clear_offset,
        "region": "Dr. Katamari"
    },
    "Medium Just-Right Complete": {
        "ID": 39 + level_clear_offset,
        "region": "Just-Right Girl"
    },
    "Large Just-Right Complete": {
        "ID": 40 + level_clear_offset,
        "region": "Just-Right Girl"
    },
    "Sweetsville Complete": {
        "ID": 41 + level_clear_offset,
        "region": "Hansel & Gretel"
    },
    "Fast Underwater Complete": {
        "ID": 42 + level_clear_offset,
        "region": "Float Boy"
    },
    "Medium Fire Complete": {
        "ID": 43 + level_clear_offset,
        "region": "Camper Man"
    },
    "Large Fire Complete": {
        "ID": 44 + level_clear_offset,
        "region": "Camper Man"
    },
    "Sumo 2 Complete": {
        "ID": 45 + level_clear_offset,
        "region": "Mini-Sumo"
    },
    "Sumo 3 Complete": {
        "ID": 46 + level_clear_offset,
        "region": "Mini-Sumo"
    }
}

cousin_rollup_offset = location_offset_ids["Cousins"]

cousin_rollup_locations = {
    "Roll Up The Prince": {
        "ID": 0 + cousin_rollup_offset,
        "region": "The Prince Collect"
    },
    "Roll Up Lalala": {
        "ID": 1 + cousin_rollup_offset,
        "region": "Lalala Collect"
    },
    "Roll Up Nik": {
        "ID": 2 + cousin_rollup_offset,
        "region": "Nik Collect"
    },
    "Roll Up Ace": {
        "ID": 3 + cousin_rollup_offset,
        "region": "Ace Collect"
    },
    "Roll Up Johnson": {
        "ID": 4 + cousin_rollup_offset,
        "region": "Johnson Collect"
    },
    "Roll Up Velvet": {
        "ID": 5 + cousin_rollup_offset,
        "region": "Velvet Collect"
    },
    "Roll Up Fujio": {
        "ID": 6 + cousin_rollup_offset,
        "region": "Fujio Collect"
    },
    "Roll Up Havana": {
        "ID": 7 + cousin_rollup_offset,
        "region": "Havana Collect"
    },
    "Roll Up Peso": {
        "ID": 8 + cousin_rollup_offset,
        "region": "Peso Collect"
    },
    "Roll Up Shikao": {
        "ID": 9 + cousin_rollup_offset,
        "region": "Shikao Collect"
    },
    "Roll Up Odeko": {
        "ID": 10 + cousin_rollup_offset,
        "region": "Odeko Collect"
    },
    "Roll Up Honey": {
        "ID": 11 + cousin_rollup_offset,
        "region": "Honey Collect"
    },
    "Roll Up Marny": {
        "ID": 12 + cousin_rollup_offset,
        "region": "Marny Collect"
    },
    "Roll Up Kuro": {
        "ID": 13 + cousin_rollup_offset,
        "region": "Kuro Collect"
    },
    "Roll Up Foomin": {
        "ID": 14 + cousin_rollup_offset,
        "region": "Foomin Collect"
    },
    "Roll Up June": {
        "ID": 15 + cousin_rollup_offset,
        "region": "June Collect"
    },
    "Roll Up Ichigo": {
        "ID": 16 + cousin_rollup_offset,
        "region": "Ichigo Collect"
    },
    "Roll Up Marcy": {
        "ID": 17 + cousin_rollup_offset,
        "region": "Marcy Collect"
    },
    "Roll Up Njamo": {
        "ID": 18 + cousin_rollup_offset,
        "region": "Njamo Collect"
    },
    "Roll Up Dipp": {
        "ID": 19 + cousin_rollup_offset,
        "region": "Dipp Collect"
    },
    "Roll Up Opeo": {
        "ID": 20 + cousin_rollup_offset,
        "region": "Opeo Collect"
    },
    "Roll Up Nickel": {
        "ID": 21 + cousin_rollup_offset,
        "region": "Nickel Collect"
    },
    "Roll Up Jungle": {
        "ID": 22 + cousin_rollup_offset,
        "region": "Jungle Collect"
    },
    "Roll Up Miso": {
        "ID": 23 + cousin_rollup_offset,
        "region": "Miso Collect"
    },
    "Roll Up Twinkle": {
        "ID": 24 + cousin_rollup_offset,
        "region": "Twinkle Collect"
    },
    "Roll Up Huey": {
        "ID": 25 + cousin_rollup_offset,
        "region": "Huey Collect"
    },
    "Roll Up Nutsuo": {
        "ID": 26 + cousin_rollup_offset,
        "region": "Nutsuo Collect"
    },
    "Roll Up Beyond": {
        "ID": 27 + cousin_rollup_offset,
        "region": "Beyond Collect"
    },
    "Roll Up Kinoko": {
        "ID": 28 + cousin_rollup_offset,
        "region": "Kinoko Collect"
    },
    "Roll Up Macho": {
        "ID": 29 + cousin_rollup_offset,
        "region": "Macho Collect"
    },
    "Roll Up L'Amour": {
        "ID": 30 + cousin_rollup_offset,
        "region": "L'Amour Collect"
    },
    "Roll Up Daisy": {
        "ID": 31 + cousin_rollup_offset,
        "region": "Daisy Collect"
    },
    "Roll Up Lucha": {
        "ID": 32 + cousin_rollup_offset,
        "region": "Lucha Collect"
    },
    "Roll Up Miki": {
        "ID": 33 + cousin_rollup_offset,
        "region": "Miki Collect"
    },
    "Roll Up Odeon": {
        "ID": 34 + cousin_rollup_offset,
        "region": "Odeon Collect"
    },
    "Roll Up Can-Can": {
        "ID": 35 + cousin_rollup_offset,
        "region": "Can-Can Collect"
    },
    "Roll Up Shy": {
        "ID": 36 + cousin_rollup_offset,
        "region": "Shy Collect"
    },
    "Roll Up Slip": {
        "ID": 37 + cousin_rollup_offset,
        "region": "Slip Collect"
    },
    "Roll Up Drooby": {
        "ID": 38 + cousin_rollup_offset,
        "region": "Drooby Collect"
    },
    "Roll Up Signolo": {
        "ID": 39 + cousin_rollup_offset,
        "region": "Signolo Collect"
    }
}

present_rollup_offset = location_offset_ids["Presents"]

present_rollup_locations = {
    "Roll up Present: Headphones": {
        "ID": 4 + present_rollup_offset,
        "region": "Rainbow Girl"
    },
    "Roll up Present: Camera": {
        "ID": 5 + present_rollup_offset,
        "region": "Lazybones"
    },
    "Roll up Present: Crown": {
        "ID": 6 + present_rollup_offset,
        "region": "Grandpa"
    },
    "Roll up Present: Little Buddy": {
        "ID": 7 + present_rollup_offset,
        "region": "Grandma"
    },
    "Roll up Present: Mt. Fuji": {
        "ID": 8 + present_rollup_offset,
        "region": "Bird & Elephant"
    },
    "Roll up Present: Flower": {
        "ID": 9 + present_rollup_offset,
        "region": "Ikebana Teacher"
    },
    "Roll up Present: Ribbons": {
        "ID": 10 + present_rollup_offset,
        "region": "Substitute Teacher"
    },
    "Roll up Present: Horsey": {
        "ID": 11 + present_rollup_offset,
        "region": "F1 Racer"
    },
    "Roll up Present: Trumpet": {
        "ID": 12 + present_rollup_offset,
        "region": "Rain Coat Girl"
    },
    "Roll up Present: Giraffe Hat": {
        "ID": 13 + present_rollup_offset,
        "region": "Dr. Katamari"
    },
    "Roll up Present: Pochette": {
        "ID": 14 + present_rollup_offset,
        "region": "Crane Hat Boy"
    },
    "Roll up Present: Scarf": {
        "ID": 15 + present_rollup_offset,
        "region": "Just-Right Girl"
    },
    "Roll up Present: Moustache": {
        "ID": 16 + present_rollup_offset,
        "region": "Cowbear Farmer"
    },
    "Roll up Present: Peacock Feathers": {
        "ID": 17 + present_rollup_offset,
        "region": "Excited Baby"
    },
    "Roll up Present: Antenna": {
        "ID": 18 + present_rollup_offset,
        "region": "Cleanly Mom"
    },
    "Roll up Present: Wig": {
        "ID": 19 + present_rollup_offset,
        "region": "Fund-raiser"
    },
    "Roll up Present: Long Nose": {
        "ID": 20 + present_rollup_offset,
        "region": "Hansel & Gretel"
    },
    "Roll up Present: Bikini": {
        "ID": 21 + present_rollup_offset,
        "region": "Float Boy"
    },
    "Roll up Present: Mushroom": {
        "ID": 22 + present_rollup_offset,
        "region": "Camper Man"
    },
    "Roll up Present: Cool Belt": {
        "ID": 23 + present_rollup_offset,
        "region": "Mini-Sumo"
    },
    "Roll up Present: Royal Mask": {
        "ID": 24 + present_rollup_offset,
        "region": "Snow Child"
    },
    "Roll up Present: Glasses": {
        "ID": 25 + present_rollup_offset,
        "region": "Book Worm"
    },
    "Roll up Present: Guitar": {
        "ID": 39 + present_rollup_offset,
        "region": "Just-Right Girl"
    },
    "Roll up Present: Mask": {
        "ID": 40 + present_rollup_offset,
        "region": "Just-Right Girl"
    }
}

all_locations = level_clear_locations | cousin_rollup_locations | present_rollup_locations

fans_offset = item_offset_ids["Fans"]

fans_items = {
    "Rainbow Girl": {
        "ID": 0 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Lazybones": {
        "ID": 1 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Grandpa": {
        "ID": 2 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Grandma": {
        "ID": 3 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Bird & Elephant": {
        "ID": 4 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Dog": {
        "ID": 5 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Soccer Kid": {
        "ID": 6 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Ikebana Teacher": {
        "ID": 7 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Substitute Teacher": {
        "ID": 8 + fans_offset,
        "classification": ItemClassification.progression
    },
    "F1 Racer": {
        "ID": 9 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Rain Coat Girl": {
        "ID": 10 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Dr. Katamari": {
        "ID": 11 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Crane Hat Boy": {
        "ID": 12 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Just-Right Girl": {
        "ID": 13 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Cowbear Farmer": {
        "ID": 14 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Excited Baby": {
        "ID": 15 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Cleanly Mom": {
        "ID": 16 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Fund-raiser": {
        "ID": 17 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Hansel & Gretel": {
        "ID": 18 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Float Boy": {
        "ID": 19 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Camper Man": {
        "ID": 20 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Mini-Sumo": {
        "ID": 21 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Snow Child": {
        "ID": 22 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Book Worm": {
        "ID": 23 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Tomio Hoshino": {
        "ID": 26 + fans_offset,
        "classification": ItemClassification.progression
    },
    "Mutsuo Hoshino": {
        "ID": 27 + fans_offset,
        "classification": ItemClassification.progression
    }
}

cousins_offset = item_offset_ids["Cousins"]

cousins_items = {}
cousin_id = 0
for cousin in list_of_cousins:
    cousins_items.update({cousin: {"ID": cousin_id + cousins_offset, "classification": ItemClassification.filler}})
    cousin_id += 1

presents_offset = item_offset_ids["Presents"]

presents_items = {}
present_id = 0
for present in list_of_presents:
    presents_items.update({present: {"ID": present_id + presents_offset, "classification": ItemClassification.filler}})
    present_id += 1

filler_offset = item_offset_ids["Filler"]

filler_items = {
    "Stardust": {
        "ID": 0 + filler_offset,
        "classification": ItemClassification.filler
    },
    "Shining Stars": {
        "ID": 1 + filler_offset,
        "classification": ItemClassification.filler
    },
    "Bright Shining Stars": {
        "ID": 2 + filler_offset,
        "classification": ItemClassification.filler
    }
}

traps_offset = item_offset_ids["Traps"]

traps_items = {
    "King's Dialogue Trap": {
        "ID": 0 + traps_offset,
        "classification": ItemClassification.trap
    },
    "Wish You Were Here Trap": {
        "ID": 1 + traps_offset,
        "classification": ItemClassification.trap
    },
    "Time Stop Trap": {
        "ID": 2 + traps_offset,
        "classification": ItemClassification.trap
    },
    "Loss of UI Trap": {
        "ID": 3 + traps_offset,
        "classification": ItemClassification.trap
    }
}

all_items = fans_items | cousins_items | presents_items | traps_items | filler_items
from __future__ import annotations

from BaseClasses import ItemClassification
from typing import TYPE_CHECKING
if TYPE_CHECKING:
    from .world import WeLoveKatamariRerollWorld

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
    "Level Complete: Roll Up The Sun": {
        "ID": 1 + level_clear_offset,
        "region": "Dog"
    },
    "Level Complete: Tutorial": {
        "ID": 3 + level_clear_offset,
        "region": "Soccer Kid"
    },
    "Level Complete: ALAP1": {
        "ID": 4 + level_clear_offset,
        "region": "Rainbow Girl"
    },
    "Level Complete: ALAP2": {
        "ID": 5 + level_clear_offset,
        "region": "Lazybones"
    },
    "Level Complete: ALAP3": {
        "ID": 6 + level_clear_offset,
        "region": "Grandpa"
    },
    "Level Complete: ALAP4": {
        "ID": 7 + level_clear_offset,
        "region": "Grandma"
    },
    "Level Complete: ALAP5": {
        "ID": 8 + level_clear_offset,
        "region": "Bird & Elephant"
    },
    "Level Complete: Flowers": {
        "ID": 9 + level_clear_offset,
        "region": "Ikebana Teacher"
    },
    "Level Complete: School": {
        "ID": 10 + level_clear_offset,
        "region": "Substitute Teacher"
    },
    "Level Complete: Race": {
        "ID": 11 + level_clear_offset,
        "region": "F1 Racer"
    },
    "Level Complete: Clouds": {
        "ID": 12 + level_clear_offset,
        "region": "Rain Coat Girl"
    },
    "Level Complete: Friends": {
        "ID": 13 + level_clear_offset,
        "region": "Dr. Katamari"
    },
    "Level Complete: Cranes": {
        "ID": 14 + level_clear_offset,
        "region": "Crane Hat Boy"
    },
    "Level Complete: Small Just-Right": {
        "ID": 15 + level_clear_offset,
        "region": "Just-Right Girl"
    },
    "Level Complete: Cowbear": {
        "ID": 16 + level_clear_offset,
        "region": "Cowbear Farmer"
    },
    "Level Complete: 50 Items": {
        "ID": 17 + level_clear_offset,
        "region": "Excited Baby"
    },
    "Level Complete: Cleaning": {
        "ID": 18 + level_clear_offset,
        "region": "Cleanly Mom"
    },
    "Level Complete: Money": {
        "ID": 19 + level_clear_offset,
        "region": "Fund-raiser"
    },
    "Level Complete: Sweets": {
        "ID": 20 + level_clear_offset,
        "region": "Hansel & Gretel"
    },
    "Level Complete: Underwater": {
        "ID": 21 + level_clear_offset,
        "region": "Float Boy"
    },
    "Level Complete: Small Fire": {
        "ID": 22 + level_clear_offset,
        "region": "Camper Man"
    },
    "Level Complete: Sumo 1": {
        "ID": 23 + level_clear_offset,
        "region": "Mini-Sumo"
    },
    "Level Complete: Snowman": {
        "ID": 24 + level_clear_offset,
        "region": "Snow Child"
    },
    "Level Complete: Fireflies": {
        "ID": 25 + level_clear_offset,
        "region": "Book Worm"
    },
    "Level Complete: Countries": {
        "ID": 28 + level_clear_offset,
        "region": "Tomio Hoshino"
    },
    "Level Complete: Cousins": {
        "ID": 29 + level_clear_offset,
        "region": "Mutsuo Hoshino"
    },
    "Level Complete: AFAP1": {
        "ID": 30 + level_clear_offset,
        "region": "Rainbow Girl"
    },
    "Level Complete: AFAP2": {
        "ID": 31 + level_clear_offset,
        "region": "Lazybones"
    },
    "Level Complete: AFAP3": {
        "ID": 32 + level_clear_offset,
        "region": "Grandpa"
    },
    "Level Complete: AFAP4": {
        "ID": 33 + level_clear_offset,
        "region": "Grandma"
    },
    "Level Complete: AFAP5": {
        "ID": 34 + level_clear_offset,
        "region": "Bird & Elephant"
    },
    "Level Complete: Fast Flowers": {
        "ID": 35 + level_clear_offset,
        "region": "Ikebana Teacher"
    },
    "Level Complete: Students": {
        "ID": 36 + level_clear_offset,
        "region": "Substitute Teacher"
    },
    "Level Complete: Fast Race": {
        "ID": 37 + level_clear_offset,
        "region": "F1 Racer"
    },
    "Level Complete: Fast Friends": {
        "ID": 38 + level_clear_offset,
        "region": "Dr. Katamari"
    },
    "Level Complete: Medium Just-Right": {
        "ID": 39 + level_clear_offset,
        "region": "Just-Right Girl"
    },
    "Level Complete: Large Just-Right": {
        "ID": 40 + level_clear_offset,
        "region": "Just-Right Girl"
    },
    "Level Complete: Sweetsville": {
        "ID": 41 + level_clear_offset,
        "region": "Hansel & Gretel"
    },
    "Level Complete: Fast Underwater": {
        "ID": 42 + level_clear_offset,
        "region": "Float Boy"
    },
    "Level Complete: Medium Fire": {
        "ID": 43 + level_clear_offset,
        "region": "Camper Man"
    },
    "Level Complete: Large Fire": {
        "ID": 44 + level_clear_offset,
        "region": "Camper Man"
    },
    "Level Complete: Sumo 2": {
        "ID": 45 + level_clear_offset,
        "region": "Mini-Sumo"
    },
    "Level Complete: Sumo 3": {
        "ID": 46 + level_clear_offset,
        "region": "Mini-Sumo"
    },
}

shooting_star_offset = location_offset_ids["Shooting Stars"]

shooting_star_locations = {
    "Shooting Star: ALAP1 (<1:20)": {
        "ID": 4 + shooting_star_offset,
        "region": "Rainbow Girl"
    },
    "Shooting Star: ALAP2 (<4:00)": {
        "ID": 5 + shooting_star_offset,
        "region": "Lazybones"
    },
    "Shooting Star: ALAP3 (<7:00)": {
        "ID": 6 + shooting_star_offset,
        "region": "Grandpa"
    },
    "Shooting Star: ALAP4 (<10:00)": {
        "ID": 7 + shooting_star_offset,
        "region": "Grandma"
    },
    "Shooting Star: ALAP5 (<13:00)": {
        "ID": 8 + shooting_star_offset,
        "region": "Bird & Elephant"
    },
    "Shooting Star: AFAP1 (<1:20)": {
        "ID": 30 + shooting_star_offset,
        "region": "Rainbow Girl"
    },
    "Shooting Star: AFAP2 (<1:20)": {
        "ID": 31 + shooting_star_offset,
        "region": "Lazybones"
    },
    "Shooting Star: AFAP3 (<1:20)": {
        "ID": 32 + shooting_star_offset,
        "region": "Grandpa"
    },
    "Shooting Star: AFAP4 (<1:20)": {
        "ID": 33 + shooting_star_offset,
        "region": "Grandma"
    },
    "Shooting Star: AFAP5 (<13:00)": {
        "ID": 34 + shooting_star_offset,
        "region": "Bird & Elephant"
    },
}

super_clear_offset = location_offset_ids["Super Clears"]

super_clear_tier_1_locations = {
    "Super Clear: Tutorial (Complete Level)": {
        "ID": 3 + super_clear_offset,
        "region": "Soccer Kid"
    },
    "Super Clear: ALAP1 (30cm)": {
        "ID": 4 + super_clear_offset,
        "region": "Rainbow Girl"
    },
    "Super Clear: ALAP2 (90cm)": {
        "ID": 5 + super_clear_offset,
        "region": "Lazybones"
    },
    "Super Clear: ALAP3 (5m)": {
        "ID": 6 + super_clear_offset,
        "region": "Grandpa"
    },
    "Super Clear: ALAP4 (25m)": {
        "ID": 7 + super_clear_offset,
        "region": "Grandma"
    },
    "Super Clear: ALAP5 (2500m)": {
        "ID": 8 + super_clear_offset,
        "region": "Bird & Elephant"
    },
    "Super Clear: Flowers (1111)": {
        "ID": 9 + super_clear_offset,
        "region": "Ikebana Teacher"
    },
    "Super Clear: School (3m80cm)": {
        "ID": 10 + super_clear_offset,
        "region": "Substitute Teacher"
    },
    "Super Clear: Race (20m)": {
        "ID": 11 + super_clear_offset,
        "region": "F1 Racer"
    },
    "Super Clear: Clouds (475)": {
        "ID": 12 + super_clear_offset,
        "region": "Rain Coat Girl"
    },
    "Super Clear: Sweets (1344)": {
        "ID": 20 + super_clear_offset,
        "region": "Hansel & Gretel"
    },
    "Super Clear: Underwater (2m)": {
        "ID": 21 + super_clear_offset,
        "region": "Float Boy"
    },
    "Super Clear: Small Fire (3m)": {
        "ID": 22 + super_clear_offset,
        "region": "Camper Man"
    },
    "Super Clear: Sumo 1 (157kg)": {
        "ID": 23 + super_clear_offset,
        "region": "Mini-Sumo"
    },
    "Super Clear: Snowman (Complete Level)": {
        "ID": 24 + super_clear_offset,
        "region": "Snow Child"
    },
    "Super Clear: Sweetsville (1308)": {
        "ID": 41 + super_clear_offset,
        "region": "Hansel & Gretel"
    },
    "Super Clear: Medium Fire (4m20cm)": {
        "ID": 43 + super_clear_offset,
        "region": "Camper Man"
    },
    "Super Clear: Large Fire (7m40cm)": {
        "ID": 44 + super_clear_offset,
        "region": "Camper Man"
    },
    "Super Clear: Sumo 2 (241kg)": {
        "ID": 45 + super_clear_offset,
        "region": "Mini-Sumo"
    },
    "Super Clear: Sumo 3 (348kg)": {
        "ID": 46 + super_clear_offset,
        "region": "Mini-Sumo"
    },
}

super_clear_tier_2_locations = {
    "Super Clear: Friends (270)": {
        "ID": 13 + super_clear_offset,
        "region": "Dr. Katamari"
    },
    "Super Clear: 50 Items (5m50cm)": {
        "ID": 17 + super_clear_offset,
        "region": "Excited Baby"
    },
    "Super Clear: Money ($100,000)": {
        "ID": 19 + super_clear_offset,
        "region": "Fund-raiser"
    },
    "Super Clear: Fireflies (Dazzling)": {
        "ID": 25 + super_clear_offset,
        "region": "Book Worm"
    },
    "Super Clear: Countries (195)": {
        "ID": 28 + super_clear_offset,
        "region": "Tomio Hoshino"
    },
    "Super Clear: AFAP1 (<0:55)": {
        "ID": 30 + super_clear_offset,
        "region": "Rainbow Girl"
    },
    "Super Clear: AFAP2 (<0:55)": {
        "ID": 31 + super_clear_offset,
        "region": "Lazybones"
    },
    "Super Clear: AFAP3 (<0:55)": {
        "ID": 32 + super_clear_offset,
        "region": "Grandpa"
    },
    "Super Clear: AFAP4 (<0:55)": {
        "ID": 33 + super_clear_offset,
        "region": "Grandma"
    },
    "Super Clear: AFAP5 (<9:30)": {
        "ID": 34 + super_clear_offset,
        "region": "Bird & Elephant"
    },
    "Super Clear: Fast Flowers (<1:00)": {
        "ID": 35 + super_clear_offset,
        "region": "Ikebana Teacher"
    },
    "Super Clear: Students (<1:30)": {
        "ID": 36 + super_clear_offset,
        "region": "Substitute Teacher"
    },
    "Super Clear: Fast Race (<1:00)": {
        "ID": 37 + super_clear_offset,
        "region": "F1 Racer"
    },
}

super_clear_tier_3_locations = {
    "Super Clear: Cranes (1000)": {
        "ID": 14 + super_clear_offset,
        "region": "Crane Hat Boy"
    },
    "Super Clear: Small Just-Right (20cm Exactly)": {
        "ID": 15 + super_clear_offset,
        "region": "Just-Right Girl"
    },
    "Super Clear: Cowbear (Cowbear)": {
        "ID": 16 + super_clear_offset,
        "region": "Cowbear Farmer"
    },
    "Super Clear: Cleaning (<1:00)": {
        "ID": 18 + super_clear_offset,
        "region": "Cleanly Mom"
    },
    "Super Clear: Fast Friends (<0:50)": {
        "ID": 38 + super_clear_offset,
        "region": "Dr. Katamari"
    },
    "Super Clear: Medium Just-Right (2m Exactly)": {
        "ID": 39 + super_clear_offset,
        "region": "Just-Right Girl"
    },
    "Super Clear: Large Just-Right (50m Exactly)": {
        "ID": 40 + super_clear_offset,
        "region": "Just-Right Girl"
    },
    "Super Clear: Fast Underwater (<1:00)": {
        "ID": 42 + super_clear_offset,
        "region": "Float Boy"
    },
}

cousin_rollup_offset = location_offset_ids["Cousins"]

cousin_rollup_locations = {
    "Cousin: The Prince": {
        "ID": 0 + cousin_rollup_offset,
        "region": "The Prince Collect"
    },
    "Cousin: Lalala": {
        "ID": 1 + cousin_rollup_offset,
        "region": "Lalala Collect"
    },
    "Cousin: Nik": {
        "ID": 2 + cousin_rollup_offset,
        "region": "Nik Collect"
    },
    "Cousin: Ace": {
        "ID": 3 + cousin_rollup_offset,
        "region": "Ace Collect"
    },
    "Cousin: Johnson": {
        "ID": 4 + cousin_rollup_offset,
        "region": "Johnson Collect"
    },
    "Cousin: Velvet": {
        "ID": 5 + cousin_rollup_offset,
        "region": "Velvet Collect"
    },
    "Cousin: Fujio": {
        "ID": 6 + cousin_rollup_offset,
        "region": "Fujio Collect"
    },
    "Cousin: Havana": {
        "ID": 7 + cousin_rollup_offset,
        "region": "Havana Collect"
    },
    "Cousin: Peso": {
        "ID": 8 + cousin_rollup_offset,
        "region": "Peso Collect"
    },
    "Cousin: Shikao": {
        "ID": 9 + cousin_rollup_offset,
        "region": "Shikao Collect"
    },
    "Cousin: Odeko": {
        "ID": 10 + cousin_rollup_offset,
        "region": "Odeko Collect"
    },
    "Cousin: Honey": {
        "ID": 11 + cousin_rollup_offset,
        "region": "Honey Collect"
    },
    "Cousin: Marny": {
        "ID": 12 + cousin_rollup_offset,
        "region": "Marny Collect"
    },
    "Cousin: Kuro": {
        "ID": 13 + cousin_rollup_offset,
        "region": "Kuro Collect"
    },
    "Cousin: Foomin": {
        "ID": 14 + cousin_rollup_offset,
        "region": "Foomin Collect"
    },
    "Cousin: June": {
        "ID": 15 + cousin_rollup_offset,
        "region": "June Collect"
    },
    "Cousin: Ichigo": {
        "ID": 16 + cousin_rollup_offset,
        "region": "Ichigo Collect"
    },
    "Cousin: Marcy": {
        "ID": 17 + cousin_rollup_offset,
        "region": "Marcy Collect"
    },
    "Cousin: Njamo": {
        "ID": 18 + cousin_rollup_offset,
        "region": "Njamo Collect"
    },
    "Cousin: Dipp": {
        "ID": 19 + cousin_rollup_offset,
        "region": "Dipp Collect"
    },
    "Cousin: Opeo": {
        "ID": 20 + cousin_rollup_offset,
        "region": "Opeo Collect"
    },
    "Cousin: Nickel": {
        "ID": 21 + cousin_rollup_offset,
        "region": "Nickel Collect"
    },
    "Cousin: Jungle": {
        "ID": 22 + cousin_rollup_offset,
        "region": "Jungle Collect"
    },
    "Cousin: Miso": {
        "ID": 23 + cousin_rollup_offset,
        "region": "Miso Collect"
    },
    "Cousin: Twinkle": {
        "ID": 24 + cousin_rollup_offset,
        "region": "Twinkle Collect"
    },
    "Cousin: Huey": {
        "ID": 25 + cousin_rollup_offset,
        "region": "Huey Collect"
    },
    "Cousin: Nutsuo": {
        "ID": 26 + cousin_rollup_offset,
        "region": "Nutsuo Collect"
    },
    "Cousin: Beyond": {
        "ID": 27 + cousin_rollup_offset,
        "region": "Beyond Collect"
    },
    "Cousin: Kinoko": {
        "ID": 28 + cousin_rollup_offset,
        "region": "Kinoko Collect"
    },
    "Cousin: Macho": {
        "ID": 29 + cousin_rollup_offset,
        "region": "Macho Collect"
    },
    "Cousin: L'Amour": {
        "ID": 30 + cousin_rollup_offset,
        "region": "L'Amour Collect"
    },
    "Cousin: Daisy": {
        "ID": 31 + cousin_rollup_offset,
        "region": "Daisy Collect"
    },
    "Cousin: Lucha": {
        "ID": 32 + cousin_rollup_offset,
        "region": "Lucha Collect"
    },
    "Cousin: Miki": {
        "ID": 33 + cousin_rollup_offset,
        "region": "Miki Collect"
    },
    "Cousin: Odeon": {
        "ID": 34 + cousin_rollup_offset,
        "region": "Odeon Collect"
    },
    "Cousin: Can-Can": {
        "ID": 35 + cousin_rollup_offset,
        "region": "Can-Can Collect"
    },
    "Cousin: Shy": {
        "ID": 36 + cousin_rollup_offset,
        "region": "Shy Collect"
    },
    "Cousin: Slip": {
        "ID": 37 + cousin_rollup_offset,
        "region": "Slip Collect"
    },
    "Cousin: Drooby": {
        "ID": 38 + cousin_rollup_offset,
        "region": "Drooby Collect"
    },
    "Cousin: Signolo": {
        "ID": 39 + cousin_rollup_offset,
        "region": "Signolo Collect"
    }
}

present_rollup_offset = location_offset_ids["Presents"]

present_rollup_locations = {
    "Present: Headphones (Rainbow Girl)": {
        "ID": 4 + present_rollup_offset,
        "region": "Rainbow Girl"
    },
    "Present: Camera (Lazybones)": {
        "ID": 5 + present_rollup_offset,
        "region": "Lazybones"
    },
    "Present: Crown (Grandpa)": {
        "ID": 6 + present_rollup_offset,
        "region": "Grandpa"
    },
    "Present: Little Buddy (Grandma)": {
        "ID": 7 + present_rollup_offset,
        "region": "Grandma"
    },
    "Present: Mt. Fuji (Bird & Elephant)": {
        "ID": 8 + present_rollup_offset,
        "region": "Bird & Elephant"
    },
    "Present: Flower (Ikebana Teacher)": {
        "ID": 9 + present_rollup_offset,
        "region": "Ikebana Teacher"
    },
    "Present: Ribbons (Substitute Teacher)": {
        "ID": 10 + present_rollup_offset,
        "region": "Substitute Teacher"
    },
    "Present: Horsey (F1 Racer)": {
        "ID": 11 + present_rollup_offset,
        "region": "F1 Racer"
    },
    "Present: Trumpet (Rain Coat Girl)": {
        "ID": 12 + present_rollup_offset,
        "region": "Rain Coat Girl"
    },
    "Present: Giraffe Hat (Dr. Katamari)": {
        "ID": 13 + present_rollup_offset,
        "region": "Dr. Katamari"
    },
    "Present: Pochette (Crane Hat Boy)": {
        "ID": 14 + present_rollup_offset,
        "region": "Crane Hat Boy"
    },
    "Present: Scarf (Just-Right Girl (Small))": {
        "ID": 15 + present_rollup_offset,
        "region": "Just-Right Girl"
    },
    "Present: Moustache (Cowbear Farmer)": {
        "ID": 16 + present_rollup_offset,
        "region": "Cowbear Farmer"
    },
    "Present: Peacock Feathers (Excited Baby)": {
        "ID": 17 + present_rollup_offset,
        "region": "Excited Baby"
    },
    "Present: Antenna (Cleanly Mom)": {
        "ID": 18 + present_rollup_offset,
        "region": "Cleanly Mom"
    },
    "Present: Wig (Fund-raiser)": {
        "ID": 19 + present_rollup_offset,
        "region": "Fund-raiser"
    },
    "Present: Long Nose (Hansel & Gretel)": {
        "ID": 20 + present_rollup_offset,
        "region": "Hansel & Gretel"
    },
    "Present: Bikini (Float Boy)": {
        "ID": 21 + present_rollup_offset,
        "region": "Float Boy"
    },
    "Present: Mushroom (Camper Man)": {
        "ID": 22 + present_rollup_offset,
        "region": "Camper Man"
    },
    "Present: Cool Belt (Mini-Sumo)": {
        "ID": 23 + present_rollup_offset,
        "region": "Mini-Sumo"
    },
    "Present: Royal Mask (Snow Child)": {
        "ID": 24 + present_rollup_offset,
        "region": "Snow Child"
    },
    "Present: Glasses (Book Worm)": {
        "ID": 25 + present_rollup_offset,
        "region": "Book Worm"
    },
    "Present: Guitar (Just-Right Girl (Medium))": {
        "ID": 39 + present_rollup_offset,
        "region": "Just-Right Girl"
    },
    "Present: Mask (Just-Right Girl (Large))": {
        "ID": 40 + present_rollup_offset,
        "region": "Just-Right Girl"
    }
}

all_locations = {}
all_locations.update(level_clear_locations)
all_locations.update(cousin_rollup_locations)
all_locations.update(present_rollup_locations)
all_locations.update(shooting_star_locations)
all_locations.update(super_clear_tier_1_locations)
all_locations.update(super_clear_tier_2_locations)
all_locations.update(super_clear_tier_3_locations)

base_locations = {}
base_locations.update(level_clear_locations)
base_locations.update(cousin_rollup_locations)
base_locations.update(present_rollup_locations)

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
    if cousin == "The Prince":
        pass
    elif cousin == "Ace":
        cousins_items.update({cousin: {"ID": cousin_id + cousins_offset, "classification": ItemClassification.progression}})
    else:
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
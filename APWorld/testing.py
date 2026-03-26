
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

cousins_items = {}
cousin_id = 0
for cousin in list_of_cousins:
    cousins_items.update({cousin: {"ID": cousin_id, "classification": "something"}})
    cousin_id += 1

print(cousins_items)
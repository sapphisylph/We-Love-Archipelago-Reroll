from dataclasses import dataclass

from Options import Range

class TrapChance(Range):
    """
    Percentage chance that any filler Stardust item is replaced by a random trap.
    """

    display_name = "Trap Chance"

    range_start = 0
    range_end = 100
    default = 0

class StartingLevel(Choice):
    """
    What fan do you start the game with?
    """

    display_name = "Starting Level"

    option_rainbow_girl = 0
    option_lazybones = 1
    option_grandpa = 2
    option_grandma = 3
    option_bird_and_elephant = 4
    option_soccer_kid = 6
    option_ikebana_teacher = 7
    option_substitute_teacher = 8


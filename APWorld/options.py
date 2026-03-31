from dataclasses import dataclass

from Options import Range, Choice, PerGameCommonOptions, Toggle

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
    option_f1_racer = 9
    option_rain_coat_girl = 10
    option_dr_katamari = 11
    option_crane_hat_boy = 12
    option_just_right_girl = 13
    option_cowbear_farmer = 14
    option_excited_baby = 15
    option_cleanly_mom = 16
    option_fund_raiser = 17
    option_hansel_and_gretel = 18
    option_float_boy = 19
    option_camper_man = 20
    option_mini_sumo = 21
    option_snow_child = 22
    option_book_worm = 23
    option_tomio_hoshino = 26

    default = option_soccer_kid

class CousinAmount(Range):
    """
    How many cousins should be randomized into the item pool?
    """

    display_name = "Cousin Amount"

    range_start = 1
    range_end = 39
    default = 15

class PresentAmount(Range):
    """
    How many presents should be randomized into the item pool?
    """

    display_name = "Present Amount"

    range_start = 1
    range_end = 32
    default = 10

class EnableShootingStars(Toggle):
    """
    Enables the ability for shooting stars to send checks.
    Adds 10 checks.
    """

    display_name = "Enable Shooting Stars"

class EnableSuperClears(Toggle):
    """
    Exceeding the fans expectations counts as a super clear!
    Receiving this praise from the King and the Fans sends out a check.
    Adds 20, 34, or 41 more checks to the game, depending on the next two options.
    """

    display_name = "Enable Super Clears"

class EnableChallengingSuperClears(Toggle):
    """
    Certain super clears are more challenging, requiring knowledge of the layout of items, or a specific route to take.
    These include, but are not limited to: As Fast As Possible levels and more difficult collection levels.
    Adds 14 checks

    NOTE: Requires Super Clears to be enabled.
    """

    display_name = "Enable Challenging Super Clears"

class EnableDifficultSuperClears(Toggle):
    """
    Some super clears are incredibly difficult, being inconsistent for even skilled players
    These include: Cowbear, 1000 cranes, Fast Friends, Fast Underwater, Cleaning, and the Just-Right stages.
    Adds 7 checks

    NOTE: Requires Super Clears and Challenging Super Clears to be enabled.
    """

    display_name = "Enable Difficult Super Clears"

@dataclass
class WeLoveKatamariRerollOptions(PerGameCommonOptions):
    trap_chance: TrapChance
    starting_level: StartingLevel
    cousin_amount: CousinAmount
    present_amount: PresentAmount
    enable_shooting_stars: EnableShootingStars
    enable_super_clears: EnableSuperClears
    enable_challenging_super_clears: EnableChallengingSuperClears
    enable_difficult_super_clears: EnableDifficultSuperClears
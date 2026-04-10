from dataclasses import dataclass

from Options import Range, Choice, PerGameCommonOptions, Toggle, OptionGroup, Visibility

# class TrapChance(Range):
#    """
#    Percentage chance that any filler Stardust item is replaced by a random trap.
#    """
#
#    display_name = "Trap Chance"
#
#    range_start = 0
#    range_end = 100
#    default = 0

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

class EnableSuperClears(Choice):
    """
    Exceeding the fans expectations counts as a super clear!
    Receiving this praise from the King and the Fans sends out a check.

    'disabled' turns off super clears

    'base_level' enables the easiest base super clears in the game.
     This includes size requirements, simple collection levels, and automatic successes. Adds 19 checks

    'challenging' enables base_level checks as well as more challenging super clears.
    This includes speed requirements and more difficult and complex collection level requirements. Adds 32 checks

    'difficult' enables base_level checks, challenging checks, as well as the most difficult super clears, requiring the most dedication to complete.
    This includes the Cowbear, Cranes, the Just-Right stages, and very hard speed challenges. Adds 40 Checks
    """

    display_name = "Enable Super Clears"

    option_disabled = 0
    option_base_level = 1
    option_challenging = 2
    option_difficult = 3

    default = option_disabled

class EnableAlternativeCousinLogic(Toggle):
    """
    Some cousins, when collected, appear in other stages aside from the stage they were collected.
    Like Kinoko and Miso in the Sumo levels, or The Prince in As Large As Possible 1.
    Enabling this option allows for those cousins to be considered in logic, and opens them all up throughout the game!

    NOTE: Due to every cousin technically being collectible in ALAP5/AFAP5, the checks will be disabled within those levels specifically.
    """

    display_name = "Enable Alternative Cousin Logic"


@dataclass
class WeLoveKatamariRerollOptions(PerGameCommonOptions):
#    trap_chance: TrapChance
    starting_level: StartingLevel
    cousin_amount: CousinAmount
    present_amount: PresentAmount
    enable_shooting_stars: EnableShootingStars
    enable_super_clears: EnableSuperClears
    enable_alternative_cousin_logic: EnableAlternativeCousinLogic

option_groups = [
    OptionGroup(
        "Gameplay Options", [
            StartingLevel,
#            TrapChance,
            EnableAlternativeCousinLogic,
            EnableShootingStars,
            EnableSuperClears,
        ],
    ),
    OptionGroup(
        "Cosmetic Options", [
            CousinAmount,
            PresentAmount,
        ]
    )
]
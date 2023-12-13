using System.Text;
using System.Text.RegularExpressions;
using Alba.CsConsoleFormat;

namespace WarhammerAssistant;
public class ConsoleMenu
{
    public static string Title { get; } = "********* WARHAMMER ASSISTANT *********";
    public static void BuildStatusMenu(Game game)
    {
        Console.Clear();
        var player1 = game.PlayerOrder[0];
        var player2 = game.PlayerOrder[1];
        var headerThickness = new LineThickness(LineWidth.Single, LineWidth.Single, LineWidth.Single, LineWidth.Single);

        Cell createPlayerUnitCell(Unit? unit)
        {
            if (unit == null)
            {
                return new Cell(new List<Element>(){
                    new Span(
                        String.Format("")
                    ){Color = ConsoleColor.Gray}
                });
            }


            byte[] bytes = Encoding.Default.GetBytes("\uD83C\uDF96");



            var isWarlord = unit.IsWarlord ? "\u2b50  " : "";
            var deathStatus = unit.IsDestroyed ? "\u2620  " : "";
            var battleShocked = unit.IsBattleShocked ? "\u26A1  " : "";
            var isLockedInCombat = unit.IsLockedInCombat != null ? "\u2694  " : "";
            var isInReserves = unit.IsInReserves ? "\u2699" : "";

            return new Cell(new List<Element>(){
                    new Span(
                        String.Format("{0}{1}{2}{3}{4}{5}\n", isWarlord, deathStatus, unit.UnitName,battleShocked,isLockedInCombat,isInReserves)
                    ){ Color = unit.IsDestroyed ? ConsoleColor.Red : ConsoleColor.Gray, },
                    // new Span(deathStatus+ "\n"){},
                        new Span(
                        String.Format("> Models Remaining: {0}\n", unit.RemainingModels)
                        ) {Color = unit.RemainingModels > 0 ? ConsoleColor.Yellow : ConsoleColor.Red},
                    new Span(unit.RemainingModels == 1 && unit.WoundsPerModel > 1 || unit.AdditionalCurrentWounds > 0 && unit.WoundsPerModel > 1 ?
                        String.Format("> Wounds : {0}/{1}",  unit.AdditionalCurrentWounds, unit.WoundsPerModel) :
                        ""
                    ) {Color = ConsoleColor.Red, }
            })
            { Margin = new Thickness(1, 0, 1, 0) };
        }


        IEnumerable<Cell[]> GridChildren;
        if (player1.Units.Count > player2.Units.Count)
        {
            GridChildren = player1.Units.Select((item, index) => new[] {
                createPlayerUnitCell(item), createPlayerUnitCell(player2.Units.Count > index ? player2.Units[index] : null)

            });
        }
        else
        {

            GridChildren = player2.Units.Select((item, index) => new[] {
                 createPlayerUnitCell(player1.Units.Count > index ? player1.Units[index] : null), createPlayerUnitCell(item)
            });
        }


        var currentRound = String.Format("=== Current Round: {0} ===", game.CurrentRound);
        var currentPhase = String.Format("=== Current Phase: {0} ===", game.Phase);
        var whosTurn = String.Format("{0}'s turn", game.PlayerOnesTurn ? player1.PlayerName : player2.PlayerName);
        var doc = new Document(
        new Grid()
        {
            Color = ConsoleColor.Gray,
            Columns = { GridLength.Star(1) },
            Children = {
                new Div(Title) { TextAlign = TextAlign.Center},
                new Div(currentRound) { TextAlign = TextAlign.Center},
                new Div(currentPhase) { TextAlign = TextAlign.Center},
                new Div(whosTurn) { TextAlign = TextAlign.Center}
            },
            Margin = new Thickness(0, 0, 0, 0)
        },
        new Grid
        {
            Color = ConsoleColor.Gray,
            Columns = { GridLength.Star(1), GridLength.Star(1) },
            Children = {
                    new Cell("++++ " + player1.PlayerName + " ++++") { Stroke = headerThickness ,Color = ConsoleColor.Yellow, TextAlign = TextAlign.Center},
                    new Cell("++++ " + player2.PlayerName + " ++++") { Stroke = headerThickness,Color = ConsoleColor.Yellow, TextAlign = TextAlign.Center},
                    GridChildren
                }
        },
        new Div(" ") { Padding = new Thickness(0, 0, 0, 0) }
        );
        doc.MaxWidth = Console.WindowWidth;

        ConsoleRenderer.RenderDocument(doc);
    }

    public static void BuildMovementMenu(Unit unit, List<string> options, int selectedIndex)
    {

        var doc = new Document(
            new Div(
                new Span("Select a movement option for "),
                new Span(String.Format("{0}", unit.UnitName)) { Color = ConsoleColor.Green },
                new Span(":\n"),
                options.Select((option, index) => new Span(String.Format("{0} {1}\n", index == selectedIndex ? ">" : "", option)) { Color = index == selectedIndex ? ConsoleColor.Green : ConsoleColor.Gray }),
                new Div(" ") { Padding = new Thickness(0, 0, 0, 1) }
            )
            { Margin = new Thickness(1, 0, 1, 0) }
        );
        ConsoleRenderer.RenderDocument(doc);
    }
    public static bool BuildMovementOptionDirections(Unit unit, string selectedMovementOption)
    {
        var options = new List<Element>();

        var needsToMakeDesperateEscapeCheck = false;

        if (selectedMovementOption == MoveOptions.Move)
        {
            options.AddRange(new List<Element>() {
            new Span(String.Format("{0} ", unit.UnitName)){Color = ConsoleColor.Green},
            new Span("has a movement of "),
            new Span(String.Format("{0}\"", unit.Movement)){Color = ConsoleColor.Yellow},
            new Span(".\nYou may move the unit anywhere as long as they do not end movement\nwithin "),
            new Span("engagement range (3\") "){Color = ConsoleColor.Red},
            new Span("of an enemy unit.")
            });
            unit.Moved();
        }
        else if (selectedMovementOption == MoveOptions.Advance)
        {
            options.AddRange(new List<Element>() {
            new Span(String.Format("{0} ", unit.UnitName)){Color = ConsoleColor.Green},
            new Span("has a advance of "),
            new Span(String.Format("1 D6 + {0}\"", unit.Movement)){Color = ConsoleColor.Yellow},
            new Span(".\nYou may advance the unit anywhere as long as they do not end movement\nwithin "),
            new Span("engagement range (3\") "){Color = ConsoleColor.Red},
            new Span("of an enemy unit. "),
            new Span("This unit will not be allowed to shoot or charge this turn.")
            });
            unit.Advanced();
        }
        else if (selectedMovementOption == MoveOptions.FallBack)
        {
            options.AddRange(new List<Element>() {
            new Span(String.Format("{0} ", unit.UnitName)){Color = ConsoleColor.Green},
            new Span("has a move of "),
            new Span(String.Format("{0}\"", unit.Movement)){Color = ConsoleColor.Yellow},
            new Span(".\nThe unit may fall back anywhere as long as they do not end movement\nwithin "),
            new Span("engagement range (3\") "){Color = ConsoleColor.Red},
            new Span("of an enemy unit.\n"),
            new Span("The "),
            new Span(String.Format("{0} ", unit.UnitName)){Color = ConsoleColor.Green},
            new Span("will not be allowed to "),
            new Span("shoot "){Color = ConsoleColor.Yellow},
            new Span("or "),
            new Span("charge "){Color = ConsoleColor.Yellow},
            new Span("this turn."),
            });
            unit.FellBack();
            // IsLockedInCombat = false
            // CanShoot = false
            // unit.cancharge = false
        }
        else if (selectedMovementOption == MoveOptions.DesperateEscape)
        {
            options.AddRange(new List<Element>() {
            new Span(String.Format("{0} ", unit.UnitName)){Color = ConsoleColor.Green},
            new Span("must make a desperate escape check for each model in it's unit.\n"),
            new Span("Roll "),
            new Span("1 D3 ") {Color = ConsoleColor.Yellow},
            new Span("for each model,if the result is "),
            new Span("more than one ") {Color = ConsoleColor.Yellow},
            new Span("it moves successfully.\n"),
            new Span(String.Format("{0} ", unit.UnitName)){Color = ConsoleColor.Green},
            new Span("has a move of "),
            new Span(String.Format("{0}\"", unit.Movement)){Color = ConsoleColor.Yellow},
            new Span(".\nThe unit may fall back anywhere as long as they do not end movement within "),
            new Span("engagement range (3\") "){Color = ConsoleColor.Red},
            new Span("of an enemy unit.\n"),
            new Span("The "),
            new Span(String.Format("{0} ", unit.UnitName)){Color = ConsoleColor.Green},
            new Span("will not be allowed to "),
            new Span("shoot "){Color = ConsoleColor.Yellow},
            new Span("or "),
            new Span("charge "){Color = ConsoleColor.Yellow},
            new Span("this turn."),
            });
            needsToMakeDesperateEscapeCheck = true;
            unit.FellBack();

        }
        else if (selectedMovementOption == MoveOptions.RemainStationary)
        {
            options.Add(new Span(String.Format("{0} remains stationary", unit.UnitName)));

            unit.RemainedStationary();
        }
        else
        {
            options.Add(new Span("Please select a valid movement option"));
        }


        var doc = new Document(
            new Div(
                options,
                new Div(" ") { Padding = new Thickness(0, 0, 0, 1) }
            )
            { Margin = new Thickness(1, 0, 1, 0) }
        );
        ConsoleRenderer.RenderDocument(doc);

        return needsToMakeDesperateEscapeCheck;


    }
    public static (int, bool) DesperateEscapeListener(Unit unit, int modelsKilled)
    {
        var doc = new Document(
              new Div(
                new Span(String.Format("Please enter the number of models that failed the desperate escape check: {0}\n", modelsKilled)),
                new Div(" ") { Padding = new Thickness(0, 0, 0, 1) }
              )
              { Margin = new Thickness(1, 0, 1, 0) }
        );
        ConsoleRenderer.RenderDocument(doc);
        var key = Console.ReadKey();
        if (Int32.TryParse(key.KeyChar.ToString(), out int result))
        {
            modelsKilled = Utility.FormatAddNumberEntry(modelsKilled, result);
            return (modelsKilled, false);

        }
        else if (key.Key == ConsoleKey.Enter) return (modelsKilled, true);
        else if (key.Key == ConsoleKey.Backspace)
        {
            modelsKilled = Utility.FormatRemoveNumberEntry(modelsKilled);
            return (modelsKilled, false);
        }
        else return (modelsKilled, false);

    }
    public static void BuildHitMenu(Unit attackingUnit, Weapon attackingWeapon, int numOfModelsAttacking, Unit defendingUnit, int hitsSucceeded)
    {
        var attackRolls = attackingWeapon.Attacks.Contains('D') ? String.Format("[{0}-{1}] attack rolls", numOfModelsAttacking, attackingWeapon.Attacks) : String.Format("{0} attack rolls ({1} attacks per model)", numOfModelsAttacking * Int32.Parse(attackingWeapon.Attacks), attackingWeapon.Attacks);

        var doc = new Document(
            new Div(
            new Span(attackingUnit.UnitName) { Color = ConsoleColor.Green },
            " is attacking defending unit ", new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red },
            String.Format(" with {0} models\n", numOfModelsAttacking),
            "Weapon Selected: ", new Span(attackingWeapon.WeaponName) { Color = ConsoleColor.Yellow }, "\n",
            "The ", new Span(attackingUnit.UnitName) { Color = ConsoleColor.Green },
            String.Format(" will make {0} and must roll {1}+ to hit\n", attackRolls, attackingWeapon.Skill),
            new Span("Please enter the number of hits that succeeded: \n") { Color = ConsoleColor.Yellow },
            new Span("Total Hits: ") { Color = ConsoleColor.Yellow },
            new Span(String.Format("{0}\n", hitsSucceeded)) { Color = ConsoleColor.Green },
          new Div(" ") { Padding = new Thickness(0, 0, 0, 1) }
        )
            { Margin = new Thickness(1, 0, 1, 0) }
        );
        ConsoleRenderer.RenderDocument(doc);
    }
    public static void BuildAttackMenu(Unit attackingUnit, Weapon attackingWeapon, int numOfModelsAttacking, Unit defendingUnit, int hitsSucceeded, int modelsKilled, int woundsTaken, string selectedItem)
    {

        var isDefenderPlural = Regex.IsMatch(defendingUnit.UnitName, "^.+s$");

        int calculateToWound()
        {
            if (attackingWeapon.Strength / 2 >= defendingUnit.Toughness)
            {
                return 2;
            }
            else if (attackingWeapon.Strength > defendingUnit.Toughness)
            {
                return 3;
            }
            else if (attackingWeapon.Strength == defendingUnit.Toughness)
            {
                return 4;
            }
            else if (attackingWeapon.Strength < defendingUnit.Toughness)
            {
                return 5;
            }
            else if (attackingWeapon.Strength * 2 <= defendingUnit.Toughness)
            {
                return 6;
            }
            else
            {
                return 0;
            }
        }

        var attackingWeaponHasTorrent = attackingWeapon.Keywords.Contains("Torrent");

        var woundRolls = String.Format("{0} wound rolls{1}", hitsSucceeded, attackingWeaponHasTorrent ? " (because the attacking weapon has the torrent keyword hit rolls automatically succeed)" : "");


        var shouldShowModelsDestroyedPrompt = defendingUnit.RemainingModels > 1 || defendingUnit.RemainingModels == 1 && defendingUnit.WoundsPerModel == 1 ? new List<Element>(){ new Span("Models killed: ") { Color = ConsoleColor.Yellow },
            new Span(String.Format("{0}\n", modelsKilled)) { Color = selectedItem == "models" ? ConsoleColor.Green : ConsoleColor.Gray }} : new List<Element>() { };

        var shouldShowAdditionalWoundsPrompt = defendingUnit.WoundsPerModel > 1 ? new List<Element>(){
            new Span("Additional Wounds taken: ") { Color = ConsoleColor.Yellow },
            new Span(String.Format("{0}\n", woundsTaken)) { Color = selectedItem == "wounds" ? ConsoleColor.Green : ConsoleColor.Gray }} : new List<Element>() { };

        var doc = new Document(new Div(
            new Span(attackingUnit.UnitName) { Color = ConsoleColor.Green },
            " is attacking defending unit ", new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red },
            String.Format(" with {0} models\n", numOfModelsAttacking),
            "Weapon Selected: ", new Span(attackingWeapon.WeaponName) { Color = ConsoleColor.Yellow }, "\n",
            "The ",
            new Span(attackingUnit.UnitName) { Color = ConsoleColor.Green },
            new Span(String.Format(" will make {0} with a strength of {1}.\n", woundRolls, attackingWeapon.Strength)),
            "The ",
            new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red },
            new Span(String.Format(" {2} {0} toughness and will require a roll of {1}+ to wound.\n", defendingUnit.Toughness, calculateToWound(), isDefenderPlural ? "have" : "has")),
            "The ",
            new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red },
            new Span(String.Format(" {0} a save of {1}+ minus the ", isDefenderPlural ? "have" : "has", defendingUnit.Save)),
            new Span(attackingWeapon.WeaponName + "'s") { Color = ConsoleColor.Yellow },
            new Span(String.Format(" AP {0} and will take {1} damage per failed save.\n", attackingWeapon.ArmorPenetration, attackingWeapon.Damage)),
            "The ",
            new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red }, new Span(String.Format(" {0} a total of {1} wounds per model\n", isDefenderPlural ? "have" : "has", defendingUnit.WoundsPerModel)),
            new Span("Please enter the results of the attack in terms of models killed and/or wounds taken: \n") { Color = ConsoleColor.Yellow },
            shouldShowModelsDestroyedPrompt,
            shouldShowAdditionalWoundsPrompt,
          new Div(" ") { Padding = new Thickness(0, 0, 0, 1) }
        )
        { Margin = new Thickness(1, 0, 1, 0) }
        );
        ConsoleRenderer.RenderDocument(doc);
    }
    public static void ListUnits(List<Unit> unitList, int selectedIndex, string message)
    {
        var doc = new Document(
            new Div(
            new Span(String.Format("{0}\n", message)),
            unitList.Select((unit, index) => new Span(String.Format("{0} {1}\n", index == selectedIndex ? ">" : "", unit.UnitName)) { Color = index == selectedIndex ? ConsoleColor.Green : ConsoleColor.Gray }),
          new Div(" ") { Padding = new Thickness(0, 0, 0, 1) }
            )
            { Margin = new Thickness(1, 0, 1, 0) }
        );
        ConsoleRenderer.RenderDocument(doc);

    }
    public static void ListWeapons(List<Weapon> weaponList, int selectedIndex, string unitName)
    {
        var doc = new Document(
              new Div(
            new Span(String.Format("Select a weapon for {0}:\n", unitName)),
            weaponList.Select((weapon, index) => new Span(String.Format("{0} {1}\n", index == selectedIndex ? ">" : "", weapon.WeaponName)) { Color = index == selectedIndex ? ConsoleColor.Green : ConsoleColor.Gray }),
          new Div(" ") { Padding = new Thickness(0, 0, 0, 1) }
        )
              { Margin = new Thickness(1, 0, 1, 0) }
        );
        ConsoleRenderer.RenderDocument(doc);
    }
    public static bool ContinueListener()
    {
        var doc = new Document(new Span("Press Enter to continue...")) { Margin = new Thickness(1, 0, 1, 0) };
        ConsoleRenderer.RenderDocument(doc);
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.Enter)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public static (int, bool, ConsoleKey) ListKeyListener(int currentIndex, int maxIndex)
    {
        var key = Console.ReadKey();

        if (key.Key == ConsoleKey.UpArrow)
        {

            if (currentIndex > 0) return (currentIndex -= 1, false, key.Key);
            return (maxIndex, false, key.Key);
        }
        else if (key.Key == ConsoleKey.DownArrow)
        {

            if (currentIndex < maxIndex) return (currentIndex += 1, false, key.Key);
            return (0, false, key.Key);
        }
        else if (key.Key == ConsoleKey.Enter)
        {
            return (currentIndex, true, key.Key);
        }
        else
        {
            return (currentIndex, false, key.Key);
        }
    }
}

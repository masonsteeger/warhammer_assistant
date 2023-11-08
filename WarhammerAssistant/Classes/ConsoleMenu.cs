using System.Text.RegularExpressions;
using Alba.CsConsoleFormat;

namespace WarhammerAssistant;
public class ConsoleMenu
{
    public static void BuildStartMenu(Player player1, Player player2)
    {
        Console.Clear();
        var headerThickness = new LineThickness(LineWidth.Single, LineWidth.Single, LineWidth.Single, LineWidth.Single);

        IEnumerable<Cell[]> GridChildren;

        if (player1.Units.Count > player2.Units.Count)
        {
            GridChildren = player1.Units.Select((item, index) => new[] {
                        new Cell(new List<Element>(){
                            new Span(
                                String.Format("{0}\n", item.UnitName )
                            ){Color = item.IsDestroyed ? ConsoleColor.Red : ConsoleColor.Gray},

                            new Span(item.RemainingModels == 1 ?
                                String.Format("> Wounds : {0}/{1}",  item.AdditionalCurrentWounds, item.WoundsPerModel) :
                                String.Format("> Models Remaining: {0}\n", item.RemainingModels)
                            ) {Color = item.RemainingModels > 1 ? ConsoleColor.Yellow : ConsoleColor.Red},
                            }),
                        new Cell(index<player2.Units.Count ? new List<Element>(){
                            new Span(
                                String.Format("{0}\n", player2.Units[index].UnitName )
                            ){Color = player2.Units[index].IsDestroyed ? ConsoleColor.Red :  ConsoleColor.Gray},

                            new Span(player2.Units[index].RemainingModels == 1 ?
                                String.Format("> Wounds : {0}/{1}",  player2.Units[index].AdditionalCurrentWounds, player2.Units[index].WoundsPerModel) :
                                String.Format("> Models Remaining: {0}\n", player2.Units[index].RemainingModels)
                            ) {Color = player2.Units[index].RemainingModels > 1 ? ConsoleColor.Yellow : ConsoleColor.Red},
                            }: ""),



            });
        }
        else
        {

            GridChildren = player2.Units.Select((item, index) => new[] {
                        new Cell(index < player1.Units.Count ? new List<Element>(){
                            new Span(
                                String.Format("{0}\n", player1.Units[index].UnitName )
                            ){Color = player1.Units[index].IsDestroyed ? ConsoleColor.Red :  ConsoleColor.Gray},
                            new Span(player1.Units[index].RemainingModels == 1 ?
                                String.Format("> Wounds : {0}/{1}",  player1.Units[index].AdditionalCurrentWounds, player1.Units[index].WoundsPerModel) :
                                String.Format("> Models Remaining: {0}\n", player1.Units[index].RemainingModels)
                            ) {Color = player1.Units[index].RemainingModels > 1 ? ConsoleColor.Yellow : ConsoleColor.Red},
                            }: ""),
                        new Cell(new List<Element>(){
                            new Span(
                                String.Format("{0}\n", item.UnitName)
                            ){Color = item.IsDestroyed ? ConsoleColor.Red :  ConsoleColor.Gray},

                            new Span(item.RemainingModels == 1 ?
                                String.Format("> Wounds : {0}/{1}",  item.AdditionalCurrentWounds, item.WoundsPerModel) :
                                String.Format("> Models Remaining: {0}\n", item.RemainingModels)
                            ) {Color = item.RemainingModels > 1 ? ConsoleColor.Yellow : ConsoleColor.Red},
                            })

            });
        }

        var doc = new Document(

        new Grid()
        {
            Color = ConsoleColor.Gray,
            Columns = { GridLength.Star(1) },
            Children = { new Div("****WARHAMMER ASSISTANT****") { Padding = new Thickness(Console.WindowWidth / 2 - 14, 0, Console.WindowWidth / 2 - 14, 0) } }
        },
        new Grid
        {
            Color = ConsoleColor.Gray,
            Columns = { GridLength.Star(1), GridLength.Star(1) },
            Children = {
                    new Cell("Player 1: " + player1.PlayerName) { Stroke = headerThickness ,Color = ConsoleColor.Yellow},
                    new Cell("Player 2: " + player2.PlayerName) { Stroke = headerThickness,Color = ConsoleColor.Yellow },
                    GridChildren
                }
        },
         new Div(" ") { Padding = new Thickness(5, 0, 5, 0) }
        );

        ConsoleRenderer.RenderDocument(doc);
    }
    public static void ListUnits(List<Unit> unitList, int selectedIndex, string message = "Select a unit: ")
    {
        var doc = new Document(
            new Span(String.Format("{0}", message)),
            unitList.Select((unit, index) => new Div(String.Format("{0} {1}", index == selectedIndex ? ">" : "", unit.UnitName)) { Color = index == selectedIndex ? ConsoleColor.Green : ConsoleColor.Gray })
        );
        ConsoleRenderer.RenderDocument(doc);
    }
    public static void ListWeapons(List<Weapon> weaponList, int selectedIndex, string unitName)
    {
        var doc = new Document(
            new Span(String.Format("Select a weapon for {0}: ", unitName)),
            weaponList.Select((weapon, index) => new Div(String.Format("{0} {1}", index == selectedIndex ? ">" : "", weapon.WeaponName)) { Color = index == selectedIndex ? ConsoleColor.Green : ConsoleColor.Gray })
        );
        ConsoleRenderer.RenderDocument(doc);
    }
    public static (int, bool) ListKeyListener(int currentIndex, int maxIndex)
    {
        var key = Console.ReadKey();
        if (key.Key == ConsoleKey.UpArrow)
        {

            if (currentIndex > 0) return (currentIndex -= 1, false);
            return (currentIndex, false);
        }
        else if (key.Key == ConsoleKey.DownArrow)
        {

            if (currentIndex < maxIndex) return (currentIndex += 1, false);
            return (currentIndex, false);
        }
        else if (key.Key == ConsoleKey.Enter)
        {
            return (currentIndex, true);
        }
        else
        {
            return (currentIndex, false);
        }
    }
    public static void BuildHitMenu(Unit attackingUnit, Weapon attackingWeapon, int numOfModelsAttacking, Unit defendingUnit, int hitsSucceeded)
    {
        var attackRolls = attackingWeapon.Attacks.Contains('D') ? String.Format("{0} {1} attack rolls", numOfModelsAttacking, attackingWeapon.Attacks) : String.Format("{0} attack rolls ({1} attacks per model)", numOfModelsAttacking * Int32.Parse(attackingWeapon.Attacks), attackingWeapon.Attacks);

        var doc = new Document(
            new Span(attackingUnit.UnitName) { Color = ConsoleColor.Green },
            " is attacking defending unit ", new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red },
            String.Format(" with {0} models\n", numOfModelsAttacking),
            "Weapon Selected: ", new Span(attackingWeapon.WeaponName) { Color = ConsoleColor.Yellow }, "\n",
            "The ", new Span(attackingUnit.UnitName) { Color = ConsoleColor.Green },
            String.Format(" will make {0} and must roll {1}+ to hit\n", attackRolls, attackingWeapon.Skill),
            new Span("Please enter the number of hits that succeeded: \n") { Color = ConsoleColor.Yellow },
            new Span("Total Hits: ") { Color = ConsoleColor.Yellow },
            new Span(String.Format("{0}\n", hitsSucceeded)) { Color = ConsoleColor.Green }
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

        var doc = new Document(
            new Span(attackingUnit.UnitName) { Color = ConsoleColor.Green },
            " is attacking defending unit ", new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red },
            String.Format(" with {0} models\n", numOfModelsAttacking),
            "Weapon Selected: ", new Span(attackingWeapon.WeaponName) { Color = ConsoleColor.Yellow }, "\n",
            "The ", new Span(attackingUnit.UnitName) { Color = ConsoleColor.Green },
            new Span(String.Format(" will make {0} with a strength of {1}.\n", woundRolls, attackingWeapon.Strength)),
            "The ", new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red }, new Span(String.Format(" {2} {0} toughness and will require a roll of {1}+ to wound.\n", defendingUnit.Toughness, calculateToWound(), isDefenderPlural ? "have" : "has")),
            "The ", new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red }, new Span(String.Format(" {0} a save of {1}+ minus the attacking weapon's AP {2} and will take {3} damage per failed save.\n", isDefenderPlural ? "have" : "has", defendingUnit.Save, attackingWeapon.ArmorPenetration, attackingWeapon.Damage)),
            "The ", new Span(defendingUnit.UnitName) { Color = ConsoleColor.Red }, new Span(String.Format(" {0} a total of {1} wounds per model\n", isDefenderPlural ? "have" : "has", defendingUnit.WoundsPerModel)),
            new Span("Please enter the results of the attack in terms of models killed and wounds taken: \n") { Color = ConsoleColor.Yellow },
            new Span("Models killed: ") { Color = ConsoleColor.Yellow },
            new Span(String.Format("{0}\n", modelsKilled)) { Color = selectedItem == "models" ? ConsoleColor.Green : ConsoleColor.Gray },
            new Span("Additional Wounds taken: ") { Color = ConsoleColor.Yellow },
            new Span(String.Format("{0}\n", woundsTaken)) { Color = selectedItem == "wounds" ? ConsoleColor.Green : ConsoleColor.Gray }
        );
        ConsoleRenderer.RenderDocument(doc);
    }
}

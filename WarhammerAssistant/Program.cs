namespace WarhammerAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            var units = LoadUnits();

            var player1 = new Player("Mason", units[0]);
            var player2 = new Player("Kylie", units[1]);


            // var player1 = new Player("Mason", new List<Unit> { units[0][0] });
            // var player2 = new Player("Kylie", new List<Unit> { units[1][0] });
            Console.CursorVisible = false;
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            // Console.Language = System.Globalization.CultureInfo.CurrentCulture;
            var game = new Game(player1, player2);

            var playShouldContinue = true;

            while (playShouldContinue)
            {
                var attacker = game.PlayerOrder[game.PlayerOnesTurn ? 0 : 1];
                var defender = game.PlayerOrder[game.PlayerOnesTurn ? 1 : 0];
                CommandLoop(game);
                MovementLoop(game);
                ShootingLoop(game);
                ChargeLoop(game, attacker, defender);
                FightLoop(game, attacker, defender);
                ChargeLoop(game, defender, attacker);
                FightLoop(game, defender, attacker);
                playShouldContinue = ChangeRound(game);
            }

            Console.WriteLine("Game Over {0} wins!", player1.Units.FindAll((unit) => unit.RemainingModels > 0).Count != 0 ? player1.PlayerName : player2.PlayerName);
            Console.CursorVisible = true;
            // Console.OutputEncoding = System.Text.Encoding.Default;
        }
        static List<List<Unit>> LoadUnits()
        {
            ///////////////////////////////Dummy Data////////////////////////////////
            /// This should eventually be loaded in via CSV or battlescribe files ///
            //////////////////////////////////////////////////////////////////////////


            /////////////////////////////Marine Units////////////////////////////////
            /// ranged weapons ///
            var stormBolterCap = new Weapon(
                "Storm Bolter",
                false,
                new List<string> { "Rapid Fire 2" },
                24, "2", 2, 4, 0, "1"

            );
            var combiWeaponLieu = new Weapon(
                "Combi-Weapon",
                false,
                new List<string> { "Anti-Infantry 4+", "Devastating Wounds", "Rapid Fire 1" },
                24, "1", 3, 4, 0, "1"
            );
            var smiteWitchfireLib = new Weapon(
                "Smite - witchfire",
                false,
                new List<string> { "Psychic" },
                24, "D6", 3, 5, -1, "D3"
            );
            var smiteWitchfireFocusedLib = new Weapon(
                "Smite - focused witchfire",
                false,
                new List<string> { "Devastating Wounds", "Hazardous", "Psychic" }, // Hazardous = roll 1d6 per weapon used and if any roll 1 that unit is destroyed
                24, "D6", 3, 5, -1, "D3"
            );
            var stormBolterLib = new Weapon(
                "Storm Bolter",
                false,
                new List<string> { "Rapid Fire 2" },
                24, "2", 3, 4, 0, "1"
            );
            var absolverBoltPistolApo = new Weapon(
                "Absolver Bolt Pistol",
                false,
                new List<string> { "Pistol" },
                18, "1", 3, 5, -1, "2"
            );
            var assaultCannonTerm = new Weapon(
                "Assault Cannon",
                false,
                new List<string> { "Devastating Wounds" },
                24, "6", 3, 6, 0, "1"
            );
            var stormBolterTerm = new Weapon(
                "Storm Bolter",
                false,
                new List<string> { "Rapid Fire 2" },
                24, "2", 3, 5, -2, "1"
            );
            var boltPistolStern = new Weapon(
                "Sternguard Bolt Pistol",
                false,
                new List<string> { "Devastating Wounds", "Pistol" },
                12, "1", 3, 4, 0, "1"
            );
            var boltRifleStern = new Weapon(
                "Sternguard Bolt Rifle",
                false,
                new List<string> { "Assault", "Heavy", "Devastating Wounds", "Rapid Fire 1" },
                24, "2", 3, 4, -1, "1"
            );
            var heavyBolterStern = new Weapon(
                "Sternguard Heavy Bolter",
                false,
                new List<string> { "Devastating Wounds", "Heavy", "Sustained Hits 1" },
                36, "3", 4, 5, -1, "2"
            );
            var boltPistolInf = new Weapon("Bolt Pistol", false, new List<string> { "Pistol" }, 12, "1", 3, 4, 0, "1");
            var pyreblasterInf = new Weapon("Pyreblaster", false, new List<string> { "Ignores Cover", "Torrent" }, 12, "D6", 6, 5, -1, "1");
            var missleLaunchFragBall = new Weapon(
                "Ballistus Missle Launcher - Frag",
                false,
                new List<string> { "Blast", },
                48, "2D6", 3, 5, 0, "1"
            );
            var missleLaunchKrakBall = new Weapon(
                "Ballistus Missle Launcher - Krak",
                false,
                new List<string>(),
                48, "2", 3, 10, -2, "D6"
            );
            var lascannonBall = new Weapon(
                "Ballistus Lascannon",
                false,
                new List<string>(),
                48, "2", 3, 12, -3, "D6 + 1"
            );
            var twinStormBolterBall = new Weapon(
                "Twin Storm Bolter",
                false,
                new List<string> { "Rapid Fire 2", "Twin Linked" },
                24, "2", 3, 4, 0, "1"
            );
            //////////////////////////////////////////////////////////////////////////
            /// melee weapons ///
            var relicWeaponCap = new Weapon(
                "Relic Weapon",
                true,
                new List<string>(),
                1, "6", 2, 8, -2, "2"
            );
            var pairedCombatBladesLieu = new Weapon(
                "Paired Combat Blades",
                true,
                new List<string> { "Anti-Tyranids 4+", "Sustained Hits 1" },
                1, "5", 2, 4, 0, "1"
            );
            var forceWeaponLib = new Weapon(
                "Force Weapon",
                true,
                new List<string> { "Psychic" },
                1, "4", 3, 6, -1, "D3"
            );
            var closeCombatWeaponApo = new Weapon(
                "Close Combat Weapon",
                true,
                new List<string>(),
                1, "4", 3, 4, 0, "1");
            var powerFistTerm = new Weapon(
                "Power Fist",
                true,
                new List<string> { "Devastating Wounds" },
                1, "3", 3, 8, -2, "2"
            );
            var powerWeaponTerm = new Weapon(
                "Power Weapon",
                true,
                new List<string>(),
                1, "4", 3, 5, -2, "1"
            );
            var closeCombatWeaponStern = new Weapon(
                "Close Combat Weapon",
                true,
                new List<string>(),
                1, "4", 3, 4, 0, "1"
            );
            var closeCombatWeaponInf = new Weapon(
                "Close Combat Weapon",
                true,
                new List<string>(),
                1, "3", 3, 4, 0, "1"
            );
            var armouredFeetBall = new Weapon(
                "Armoured Feet",
                true,
                new List<string>(),
                1, "5", 3, 7, 0, "1"
            );
            //////////////////////////////////////////////////////////////////////////
            /// units ///
            var captainInTerminatorArmour = new Unit(
                "Captain in Terminator Armour",
                1, new List<Weapon> { stormBolterCap }, new List<Weapon> { relicWeaponCap }, 5, 5, 2, 6, 6, 1, true, true, false
            );
            var lieutenantWithCombiWeapon = new Unit(
                "Lieutenant with Combi-Weapon",
                1, new List<Weapon> { combiWeaponLieu }, new List<Weapon> { pairedCombatBladesLieu }, 6, 4, 3, 4, 6, 1, false, false, false
            );

            var librarianInTerminatorArmour = new Unit(
                "Librarian in Terminator Armour",
                1, new List<Weapon> { smiteWitchfireLib, smiteWitchfireFocusedLib, stormBolterLib }, new List<Weapon> { forceWeaponLib }, 5, 5, 2, 5, 6, 1, false, true, false
            );
            var apothecaryBiologis = new Unit(
                "Apothecary Biologis",
                1, new List<Weapon> { absolverBoltPistolApo }, new List<Weapon> { closeCombatWeaponApo }, 5, 6, 3, 5, 6, 3, false, true, false
            );
            var terminators = new Unit(
                "Terminator Squad", 5, new List<Weapon> { stormBolterTerm, assaultCannonTerm }, new List<Weapon> { powerFistTerm, powerWeaponTerm }, 5, 5, 2, 3, 6, 1, false, false, false
            );
            var sternguardVeterans = new Unit(
                "Sternguard Veteran Squad", 5, new List<Weapon> { boltPistolStern, boltRifleStern, heavyBolterStern }, new List<Weapon> { closeCombatWeaponStern }, 6, 4, 3, 2, 6, 1, false, false, false
            );
            var infernusMarines = new Unit(
                "Infernus Squad", 10, new List<Weapon> { boltPistolInf, pyreblasterInf }, new List<Weapon> { closeCombatWeaponInf }, 6, 4, 3, 2, 6, 1, false, false, false
            );
            var ballistusDreadnought = new Unit(
                "Ballistus Dreadnought", 1, new List<Weapon> { missleLaunchFragBall, missleLaunchKrakBall, lascannonBall, twinStormBolterBall }, new List<Weapon> { armouredFeetBall }, 8, 10, 2, 12, 6, 4, false, false, false
            );
            //////////////////////////////////////////////////////////////////////////
            //////////////////////////////////////////////////////////////////////////

            /////////////////////////////Tyranid Units////////////////////////////////
            /// ranged weapons ///
            var fleshborerTermagant = new Weapon(
                "Fleshborer",
                false,
                new List<string> { "Assault" },
                18, "1", 4, 5, 0, "1"
            );
            var bioCannonBarb = new Weapon(
                "Bio-Cannon",
                false,
                new List<string> { "Blast", "Heavy" },
                24, "D6", 4, 5, 0, "1"
            );
            var spinemawsRip = new Weapon(
                "Spinemaws",
                false,
                new List<string> { "Pistol" },
                6, "4", 5, 3, 0, "1"
            );
            var bioPlasmicScream = new Weapon(
                "Bio-Plasmic Scream",
                false,
                new List<string> { "Assault", "Blast" },
                18, "D6+3", 4, 8, -2, "1"
            );
            var psychoclasticTorrent = new Weapon(
                "Psychoclastic Torrent",
                false,
                new List<string> { "Ignores Cover", "Torrent" },
                12, "D6", 6, 6, -1, "1"
            );
            var psychicScreamNeuro = new Weapon(
                "Psychic Scream",
                false,
                new List<string> { "Ignores Cover", "Psychic", "Torrent" },
                18, "2D6", 6, 5, -1, "2"
            );

            /// melee weapons ///
            var xenosCaTTermagant = new Weapon(
                "Xenos Claws and Teeth",
                true,
                new List<string>(),
                1, "1", 4, 3, 0, "1"
            );
            var xenosCaTNeurogaunt = new Weapon(
                "Xenos Claws and Teeth",
                true,
                new List<string>(),
                1, "1", 4, 3, 0, "1"
            );
            var xenosCaTBarb = new Weapon(
                "Xenos Claws and Teeth",
                true,
                new List<string>(),
                1, "1", 4, 4, 0, "1"
            );
            var xenosCaTRip = new Weapon(
                "Xenos Claws and Teeth",
                true,
                new List<string>(),
                1, "6", 5, 2, 0, "1"
            );
            var leapersTalons = new Weapon(
                "Leaper's Talons",
                true,
                new List<string>(),
                1, "6", 3, 5, -1, "1"
            );
            var screamerKillerTalons = new Weapon(
                "Screamer-Killer Talons",
                true,
                new List<string>(),
                1, "10", 3, 10, -2, "3"
            );
            var talonsTenticleMaw = new Weapon(
                "Talons and Betentacled Maw",
                true,
                new List<string> { "Anti-Psyker 2+", "Devastating Wounds" },
                1, "D6+1", 3, 6, -1, "2"
            );
            var primeTalons = new Weapon(
                "Prime Talons",
                true,
                new List<string>(),
                1, "6", 2, 6, -1, "2"
            );
            var neuroClawsAndLashes = new Weapon(
                "Neurotyrant Claws and Lashes",
                true,
                new List<string>(),
                1, "6", 3, 5, 0, "1"
            );

            /// units ///
            var termagants = new Unit(
                "Termagants", 20, new List<Weapon> { fleshborerTermagant }, new List<Weapon> { xenosCaTTermagant }, 6, 3, 5, 1, 8, 2, false, false, false
            );
            var neurogaunts = new Unit(
                "Neurogaunts", 11, new List<Weapon>(), new List<Weapon> { xenosCaTNeurogaunt }, 6, 3, 6, 1, 8, 1, false, false, false
            );
            var barbgaunts = new Unit(
                "Barbgaunts", 5, new List<Weapon> { bioCannonBarb }, new List<Weapon> { xenosCaTBarb }, 6, 4, 4, 2, 8, 1, false, false, false
            );
            var rippers = new Unit(
                "Ripper Swarms", 2, new List<Weapon> { spinemawsRip }, new List<Weapon> { xenosCaTRip }, 6, 2, 6, 4, 8, 0, false, false, false
            );
            var vonRyansLeapers = new Unit(
                "Von Ryan's Leapers", 3, new List<Weapon>(), new List<Weapon> { leapersTalons }, 10, 5, 4, 3, 8, 1, false, false, false
            );
            var screamerKiller = new Unit(
                "Screamer-Killer", 1, new List<Weapon> { bioPlasmicScream }, new List<Weapon> { screamerKillerTalons }, 8, 9, 2, 10, 8, 3, false, false, false
            );
            var psychophage = new Unit(
                "Psychophage", 1, new List<Weapon> { psychoclasticTorrent }, new List<Weapon> { talonsTenticleMaw }, 8, 9, 3, 10, 8, 3, false, false, false
            );
            var wingedTyranidPrime = new Unit(
                "Winged Tyranid Prime", 1, new List<Weapon>(), new List<Weapon> { primeTalons }, 12, 5, 4, 6, 7, 1, false, true, false
            );
            var neurotyrant = new Unit(
                "Neurotyrant", 1, new List<Weapon> { psychicScreamNeuro }, new List<Weapon> { neuroClawsAndLashes }, 6, 8, 4, 9, 7, 3, true, true, false
            );
            //////////////////////////////////////////////////////////////////////////
            /////////////////////////////////////////////////////////////////////////////

            var marineList = new List<Unit> {
                // captainInTerminatorArmour,
                // lieutenantWithCombiWeapon,
                // librarianInTerminatorArmour,
                // apothecaryBiologis,
                terminators,
                // sternguardVeterans,
                // infernusMarines,
                // ballistusDreadnought
            };

            var tyranidList = new List<Unit> {
                termagants,
                // neurogaunts,
                // barbgaunts,
                // rippers,
                // vonRyansLeapers,
                // screamerKiller,
                // psychophage,
                // wingedTyranidPrime,
                // neurotyrant
            };


            var playerOneUnits = marineList;
            var playerTwoUnits = tyranidList;

            return new List<List<Unit>> { playerOneUnits, playerTwoUnits };

            //////////////////////////////////////////////////////////////////////////
        }
        static bool ChangeRound(Game game)
        {
            var player1 = game.PlayerOrder[0];
            var player2 = game.PlayerOrder[1];
            game.CurrentRound++;
            game.PlayerOnesTurn = !game.PlayerOnesTurn;
            var armyNotEliminated =
                player1.Units.FindAll((unit) => unit.RemainingModels > 0).Count != 0 &&
                player2.Units.FindAll((unit) => unit.RemainingModels > 0).Count != 0;
            var maxRoundNotReached = game.CurrentRound < 6;

            return armyNotEliminated && maxRoundNotReached;
        }
        static void CommandLoop(Game game)
        {
            game.Phase = Phase.Movement;
        }
        static void MovementLoop(Game game)
        {
            var player = game.PlayerOrder[game.PlayerOnesTurn ? 0 : 1];
            player.AddUnitsToAct(player.Units.FindAll((unit) => unit.RemainingModels > 0));
            while (player.UnitsToAct.Count > 0)
            {
                var currentUnitIndex = 0;
                var hasSelectedUnit = false;
                while (!hasSelectedUnit)
                {
                    ConsoleMenu.BuildStatusMenu(game);
                    ConsoleMenu.ListUnits(player.UnitsToAct, currentUnitIndex, "Select a unit to move: ");
                    (currentUnitIndex, hasSelectedUnit, var keyPressed) = ConsoleMenu.ListKeyListener(currentUnitIndex, player.UnitsToAct.Count - 1);
                    if (keyPressed == ConsoleKey.End)
                    {
                        player.UnitsToAct.Clear();
                        game.Phase = Phase.Command;
                        return;
                    }
                }
                var selectedUnit = player.UnitsToAct[currentUnitIndex];
                player.UnitsToAct.Remove(selectedUnit);

                var movementOptions = new List<string>() { };
                if (selectedUnit.IsLockedInCombat != null)
                {
                    if (selectedUnit.IsBattleShocked)
                    {
                        movementOptions.AddRange(new List<string>() { MoveOptions.DesperateEscape, MoveOptions.RemainStationary });
                    }
                    else
                    {
                        movementOptions.AddRange(new List<string>() { MoveOptions.FallBack, MoveOptions.DesperateEscape, MoveOptions.RemainStationary });
                    }
                }
                else
                {
                    movementOptions.AddRange(new List<string>() { MoveOptions.Move, MoveOptions.Advance, MoveOptions.RemainStationary });
                }
                var hasMoved = false;
                var movementOptionIndex = 0;

                while (!hasMoved)
                {
                    ConsoleMenu.BuildStatusMenu(game);
                    ConsoleMenu.BuildMovementMenu(selectedUnit, movementOptions, movementOptionIndex);
                    (movementOptionIndex, hasMoved, var keyPressed) = ConsoleMenu.ListKeyListener(movementOptionIndex, movementOptions.Count - 1);

                };
                ConsoleMenu.BuildStatusMenu(game);
                var needsToMakeDesperateEscapeCheck = ConsoleMenu.BuildMovementOptionDirections(selectedUnit, movementOptions[movementOptionIndex]);

                if (needsToMakeDesperateEscapeCheck)
                {
                    var modelsKilled = 0;
                    var completedDesperateEscapeCheck = false;
                    while (!completedDesperateEscapeCheck)
                    {
                        ConsoleMenu.BuildStatusMenu(game);
                        ConsoleMenu.BuildMovementOptionDirections(selectedUnit, movementOptions[movementOptionIndex]);
                        (modelsKilled, completedDesperateEscapeCheck) = ConsoleMenu.DesperateEscapeListener(selectedUnit, modelsKilled);
                        continue;
                    }
                    selectedUnit.ResolveAttack(modelsKilled, 0);
                }
                else
                {
                    while (!ConsoleMenu.ContinueListener())
                    {
                        continue;
                    }
                }


            }
            game.Phase = Phase.Shooting;
        }
        static void ShootingLoop(Game game)
        {
            var attacker = game.PlayerOrder[game.PlayerOnesTurn ? 0 : 1];
            var defender = game.PlayerOrder[game.PlayerOnesTurn ? 1 : 0];

            attacker.AddUnitsToAct(attacker.Units.FindAll((unit) => unit.RemainingModels > 0 && unit.RangedWeapons.Count != 0));
            while (attacker.UnitsToAct.Count > 0 && defender.Units.FindAll((unit) => unit.RemainingModels > 0).Count > 0)
            {
                var currentUnitIndex = 0;
                var hasSelectedUnit = false;
                while (!hasSelectedUnit)
                {
                    ConsoleMenu.BuildStatusMenu(game);
                    ConsoleMenu.ListUnits(attacker.UnitsToAct, currentUnitIndex, "Select a unit to attack: ");
                    (currentUnitIndex, hasSelectedUnit, var KeyPressed) = ConsoleMenu.ListKeyListener(currentUnitIndex, attacker.UnitsToAct.Count - 1);
                }
                var selectedUnit = attacker.UnitsToAct[currentUnitIndex];
                attacker.UnitsToAct.Remove(selectedUnit);
                var rangeWeapons = new List<Weapon>();
                rangeWeapons.AddRange(selectedUnit.RangedWeapons);
                while (rangeWeapons.Count != 0 && defender.Units.FindAll((unit) => unit.RemainingModels > 0).Count > 0)
                {
                    var currentWeaponIndex = 0;
                    var hasSelectedWeapon = false;
                    while (!hasSelectedWeapon)
                    {
                        ConsoleMenu.BuildStatusMenu(game);
                        ConsoleMenu.ListWeapons(rangeWeapons, currentWeaponIndex, selectedUnit.UnitName);
                        (currentWeaponIndex, hasSelectedWeapon, var KeyPressed) = ConsoleMenu.ListKeyListener(currentWeaponIndex, rangeWeapons.Count - 1);
                    }
                    var selectedWeapon = rangeWeapons[currentWeaponIndex];
                    rangeWeapons.FindAll((wpn) => wpn.WeaponName.
                        StartsWith(selectedWeapon.WeaponName.
                        Split("-")[0])).
                        ForEach((wpn) => rangeWeapons.Remove(wpn));
                    var hasSelectedTarget = false;
                    var currentTargetIndex = 0;
                    var remainingDefenders = defender.Units.FindAll((unit) => unit.RemainingModels > 0);
                    while (!hasSelectedTarget && remainingDefenders.Count > 0)
                    {
                        ConsoleMenu.BuildStatusMenu(game);
                        ConsoleMenu.ListUnits(remainingDefenders, currentTargetIndex, message: "Select a target: ");
                        (currentTargetIndex, hasSelectedTarget, var KeyPressed) = ConsoleMenu.ListKeyListener(currentTargetIndex, remainingDefenders.Count - 1);
                    }
                    while (!CombatAssistant(game, selectedUnit, selectedWeapon, remainingDefenders[currentTargetIndex]))
                    {
                        continue;
                    }
                }

            }
            game.Phase = Phase.Charge;
        }
        static void ChargeLoop(Game game, Player actingPlayer, Player passivePlayer)
        {
            actingPlayer.AddUnitsToAct(actingPlayer.Units.FindAll((unit) => unit.RemainingModels > 0 && unit.CanCharge));
            while (actingPlayer.UnitsToAct.Count > 0 && passivePlayer.Units.FindAll((unit) => unit.RemainingModels > 0).Count > 0)
            {
                var currentUnitIndex = 0;
                var hasSelectedUnit = false;
                while (!hasSelectedUnit)
                {
                    ConsoleMenu.BuildStatusMenu(game);
                    ConsoleMenu.ListUnits(actingPlayer.UnitsToAct, currentUnitIndex, "Select a unit to charge: ");
                    (currentUnitIndex, hasSelectedUnit, var keyPressed) = ConsoleMenu.ListKeyListener(currentUnitIndex, actingPlayer.UnitsToAct.Count - 1);
                    if (keyPressed == ConsoleKey.End)
                    {
                        actingPlayer.UnitsToAct.Clear();
                        game.Phase = Phase.Command;
                        return;
                    }
                }
                var selectedUnit = actingPlayer.UnitsToAct[currentUnitIndex];
                actingPlayer.UnitsToAct.Remove(selectedUnit);

                var hasSelectedTarget = false;
                var currentTargetIndex = 0;
                var chargeableUnits = passivePlayer.Units.FindAll((unit) => unit.RemainingModels > 0);
                while (!hasSelectedTarget && chargeableUnits.Count > 0)
                {
                    ConsoleMenu.BuildStatusMenu(game);
                    ConsoleMenu.ListUnits(chargeableUnits, currentTargetIndex, message: "Select a target to charge: ");
                    (currentTargetIndex, hasSelectedTarget, var KeyPressed) = ConsoleMenu.ListKeyListener(currentTargetIndex, chargeableUnits.Count - 1);
                }
                while (!ChargeAssistant(game, selectedUnit, chargeableUnits[currentTargetIndex]))
            }
            game.Phase = Phase.Fight;
        }
        static void FightLoop(Game game, Player actingPlayer, Player passivePlayer)
        {


            if (actingPlayer.PlayerName == game.PlayerOrder[0].PlayerName) game.Phase = Phase.Charge;
            else game.Phase = Phase.Command;
        }
        static bool ChargeAssistant(Game game, Unit selectedUnit, Unit unitBeingCharged)
        {
            var hasRolledChargeDistance = false;
            var submit = false;
            while (!submit)
            {
                // render charge menu that instructs player to roll dice and select if they had a successful charge
                // if yes then engage both units in melee combat with each other
                // if no then exit while loop
            }
            return true;
        }
        static bool CombatAssistant(Game game, Unit attackingUnit, Weapon attackingWeapon, Unit defendingUnit)
        {
            var hits = 0;
            var modelsAttacking = 3;
            var haveHitsRolled = false;
            var modelsKilled = 0;
            var additionalWounds = 0;
            var selectedItem = defendingUnit.RemainingModels > 1 || defendingUnit.RemainingModels == 1 && defendingUnit.WoundsPerModel == 1 ? "models" : "wounds";
            var submit = false;
            while (!submit)
            {
                ConsoleMenu.BuildStatusMenu(game);
                if (!haveHitsRolled) ConsoleMenu.BuildHitMenu(attackingUnit, attackingWeapon, modelsAttacking, defendingUnit, hits);
                else if (haveHitsRolled && hits > 0) ConsoleMenu.BuildAttackMenu(attackingUnit, attackingWeapon, modelsAttacking, defendingUnit, hits, modelsKilled, additionalWounds, selectedItem);
                else break;
                var key = Console.ReadKey();
                if (Int32.TryParse(key.KeyChar.ToString(), out int result))
                {
                    if (haveHitsRolled)
                    {
                        if (selectedItem == "models") modelsKilled = Utility.FormatAddNumberEntry(modelsKilled, result);
                        else if (selectedItem == "wounds") additionalWounds = Utility.FormatAddNumberEntry(additionalWounds, result);
                    }
                    else hits = Utility.FormatAddNumberEntry(hits, result);
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (haveHitsRolled)
                    {
                        if (selectedItem == "models")
                        {
                            if (defendingUnit.WoundsPerModel > 1 &&
                                defendingUnit.RemainingModels > 1 &&
                                modelsKilled < defendingUnit.RemainingModels
                            )
                            {
                                selectedItem = "wounds";
                            }
                            else
                            {
                                submit = true;
                            }
                        }
                        else if (selectedItem == "wounds") { submit = true; }
                    }
                    else haveHitsRolled = true;
                }
                else if (key.Key == ConsoleKey.Escape)
                {
                    if (haveHitsRolled)
                    {
                        if (selectedItem == "models") continue;
                        else if (selectedItem == "wounds") selectedItem = "models";
                    }
                    else continue;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (haveHitsRolled)
                    {
                        if (selectedItem == "models") modelsKilled = Utility.FormatRemoveNumberEntry(modelsKilled);
                        else if (selectedItem == "wounds") additionalWounds = Utility.FormatRemoveNumberEntry(additionalWounds);
                    }
                    else hits = Utility.FormatRemoveNumberEntry(hits);
                }
                else
                {
                    continue;
                }
            }
            defendingUnit.ResolveAttack(modelsKilled, additionalWounds);
            return true;
        }
    }
}

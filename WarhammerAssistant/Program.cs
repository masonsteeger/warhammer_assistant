namespace WarhammerAssistant
{
    class Program
    {
        static void Main(string[] args)
        {
            var units = LoadUnits();

            var player1 = new Player("Mason", units[0]);
            var player2 = new Player("Kylie", units[1]);

            var game = new Game(player1, player2);
            ShootingLoop(player1, player2);


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
                captainInTerminatorArmour,
                lieutenantWithCombiWeapon,
                librarianInTerminatorArmour,
                apothecaryBiologis,
                terminators,
                sternguardVeterans,
                infernusMarines,
                ballistusDreadnought
            };

            var tyranidList = new List<Unit> {
                termagants,
                neurogaunts,
                barbgaunts,
                rippers,
                vonRyansLeapers,
                screamerKiller,
                psychophage,
                wingedTyranidPrime,
                neurotyrant
            };


            var playerOneUnits = marineList;
            var playerTwoUnits = tyranidList;

            return new List<List<Unit>> { playerOneUnits, playerTwoUnits };

            //////////////////////////////////////////////////////////////////////////
        }
        static void ShootingLoop(Player attacker, Player defender)
        {
            attacker.AddUnitsToAct(attacker.Units);
            while (attacker.UnitsToAct.Count != 0)
            {
                var currentUnitIndex = 0;
                var hasSelectedUnit = false;
                while (!hasSelectedUnit)
                {
                    ConsoleMenu.BuildStartMenu(attacker, defender);
                    ConsoleMenu.ListUnits(attacker.UnitsToAct, currentUnitIndex);
                    (currentUnitIndex, hasSelectedUnit) = ConsoleMenu.ListKeyListener(currentUnitIndex, attacker.UnitsToAct.Count - 1);
                }
                var selectedUnit = attacker.UnitsToAct[currentUnitIndex];
                attacker.UnitsToAct.Remove(selectedUnit);
                var rangeWeapons = selectedUnit.RangedWeapons;
                while (rangeWeapons.Count != 0)
                {
                    var currentWeaponIndex = 0;
                    var hasSelectedWeapon = false;
                    while (!hasSelectedWeapon)
                    {
                        ConsoleMenu.BuildStartMenu(attacker, defender);
                        ConsoleMenu.ListWeapons(rangeWeapons, currentWeaponIndex, selectedUnit.UnitName);
                        (currentWeaponIndex, hasSelectedWeapon) = ConsoleMenu.ListKeyListener(currentWeaponIndex, rangeWeapons.Count - 1);
                    }
                    var selectedWeapon = rangeWeapons[currentWeaponIndex];
                    rangeWeapons.Remove(rangeWeapons[currentWeaponIndex]);
                    var hasSelectedTarget = false;
                    var currentTargetIndex = 0;
                    while (!hasSelectedTarget)
                    {
                        ConsoleMenu.BuildStartMenu(attacker, defender);
                        ConsoleMenu.ListUnits(defender.Units, currentTargetIndex, message: "Select a target: ");
                        (currentTargetIndex, hasSelectedTarget) = ConsoleMenu.ListKeyListener(currentTargetIndex, defender.Units.Count - 1);
                    }
                    while (!CombatAssistant(attacker, defender, selectedUnit, selectedWeapon, defender.Units[currentTargetIndex]))
                    {
                        continue;
                    }
                }

            }
        }
        static bool CombatAssistant(Player attacker, Player defender, Unit attackingUnit, Weapon attackingWeapon, Unit defendingUnit)
        {
            var hits = 0;
            var modelsAttacking = 3;
            var haveHitsRolled = false;
            var modelsKilled = 0;
            var additionalWounds = 0;
            var selectedItem = "models";
            var submit = false;
            while (!submit)
            {
                ConsoleMenu.BuildStartMenu(attacker, defender);
                if (!haveHitsRolled) ConsoleMenu.BuildHitMenu(attackingUnit, attackingWeapon, modelsAttacking, defendingUnit, hits);
                else if (haveHitsRolled && hits > 0) ConsoleMenu.BuildAttackMenu(attackingUnit, attackingWeapon, modelsAttacking, defendingUnit, hits, modelsKilled, additionalWounds, selectedItem);
                else break;
                var key = Console.ReadKey();
                if (Int32.TryParse(key.KeyChar.ToString(), out int result))
                {
                    if (haveHitsRolled)
                    {
                        if (selectedItem == "models") modelsKilled = Int32.Parse(modelsKilled.ToString() + result.ToString());
                        else if (selectedItem == "wounds") additionalWounds = Int32.Parse(additionalWounds.ToString() + result.ToString());
                    }
                    else hits = Int32.Parse(hits.ToString() + result.ToString());
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (haveHitsRolled)
                    {
                        if (selectedItem == "models") selectedItem = "wounds";
                        else if (selectedItem == "wounds") submit = true;
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
                        if (selectedItem == "models") modelsKilled = Int32.Parse(modelsKilled.ToString().Substring(0, modelsKilled.ToString().Length - 1));
                        else if (selectedItem == "wounds") additionalWounds = Int32.Parse(additionalWounds.ToString().Substring(0, additionalWounds.ToString().Length - 1));
                    }
                    else hits = Int32.Parse(hits.ToString().Substring(0, hits.ToString().Length - 1));
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

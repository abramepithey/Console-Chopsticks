using System;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace Chopsticks
{
    public class ProgramUI
    {
        private Player _playerOne;
        private Player _playerTwo;

        public void Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                RenderTitle();
                RenderMainMenu();
                isRunning = MenuChoice();
            }
        }

        private static void RenderTitle()
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════════════════╗\n" +
                              "║    _____ _                     _   _      _          ║\n" + 
                              "║   / ____| |                   | | (_)    | |         ║\n" +
                              "║  | |    | |__   ___  _ __  ___| |_ _  ___| | _____   ║\n" + 
                              "║  | |    | '_ \\ / _ \\| '_ \\/ __| __| |/ __| |/ / __|  ║\n" + 
                              "║  | |____| | | | (_) | |_) \\__ \\ |_| | (__|   <\\__ \\  ║\n" + 
                              "║   \\_____|_| |_|\\___/| .__/|___/\\__|_|\\___|_|\\_\\___/  ║\n" + 
                              "║                     | |                              ║\n" + 
                              "║                     |_|                              ║\n" +
                              "╚══════════════════════════════════════════════════════╝");
            // ╗ ╣ ╝ ╩ ╦ ╠ ═ ╬
        }

        private static void RenderMainMenu()
        {
            Console.WriteLine("Select an option:\n" +
                              "1. VS Computer\n" +
                              "2. VS Human\n" +
                              "3. Rules\n" +
                              "4. Exit");
        }

        private bool MenuChoice()
        {
            var response = Console.ReadLine()?.ToLower();
            switch (response)
            {
                case "1":
                case "c":
                case "computer":
                    PlayComputer();
                    break;
                case "2":
                case "h":
                case "human":
                    PlayHuman();
                    break;
                case "3":
                case "r":
                case "rules":
                    Rules();
                    break;
                case "4":
                case "e":
                case "exit":
                    Exit();
                    return false;
                default:
                    Console.WriteLine("Please enter a valid selection.");
                    Thread.Sleep(2000);
                    break;
            }

            return true;
        }

        private static void Rules()
        {
            Console.WriteLine("Each player begins with one finger raised on each hand. After the first player, turns proceed clockwise.\n" +
                              "On a player's turn, they must either attack or split, but not both.\n" +
                              "To attack, a player uses one of their live hands to strike an opponent's live hand. The number of fingers on the opponent's struck hand will increase by the number of fingers on the hand used to strike.\n" +
                              "To split, a player strikes their own two hands together, and transfers raised fingers from one hand to the other as desired. A move is not allowed to simply reverse one's own hands and they must not kill one of their own hands.\n" +
                              "If any hand of any player reaches exactly five fingers, then the hand is killed, and this is indicated by raising zero fingers (i.e. a closed fist).\n" +
                              "A player may revive their own dead hand using a split, as long as they abide by the rules for splitting. However, players may not revive opponents' hands using an attack. Therefore, a player with two dead hands can no longer play and is eliminated from the game.\n" +
                              "If any hand of any player reaches more than five fingers, then five fingers are subtracted from that hand. For instance, if a 4-finger hand strikes a 2-finger hand, for a total of 6 fingers, then 5 fingers are automatically subtracted, leaving 1 finger.\n" +
                              "A player wins once all opponents are eliminated (by each having two dead hands at once).");
            Console.ReadKey();
            Console.Clear();
        }

        private static void Exit()
        {
            Console.WriteLine("Exiting program...");
            Thread.Sleep(2000);
        }

        public void PlayComputer()
        {
            
        }

        public void PlayHuman()
        {
            CreatePlayerOne();
            CreatePlayerTwo();
            ShowCurrentTotals();
            bool playing = true;
            while (playing)
            {
                CheckForAbilityToSplit(_playerOne, _playerTwo);
                playing = ShowCurrentTotals();
                if (!playing) continue;
                CheckForAbilityToSplit(_playerTwo, _playerOne);
                playing = ShowCurrentTotals();
            }
        }

        private void CreatePlayerOne()
        {
            Console.WriteLine("Enter a name for Player 1:");
            _playerOne = new Player(Console.ReadLine());
        }

        private void CreatePlayerTwo()
        {
            Console.WriteLine("Enter a name for Player 2:");
            _playerTwo = new Player(Console.ReadLine());
        }

        private bool ShowCurrentTotals()
        {
            Console.Clear();
            Console.WriteLine($"{_playerOne.Name}: Left - {_playerOne.LeftHand}   Right - {_playerOne.RightHand}\n\n" +
                              $"{_playerTwo.Name}: Left - {_playerTwo.LeftHand}   Right - {_playerTwo.RightHand}\n");
            if (_playerOne.LeftHand == 0 && _playerOne.RightHand == 0)
            {
                GameOver(_playerTwo);
                return false;
            }

            if (_playerTwo.LeftHand != 0 || _playerTwo.RightHand != 0) return true;
            GameOver(_playerOne);
            return false;

        }

        public void CheckForAbilityToSplit(Player attacker, Player defender)
        {
            if (attacker.LeftHand <= 1 && attacker.RightHand <= 1)
            {
                Console.WriteLine("You can only attack.");
                InitializeAttack(attacker, defender);
            }
            else
            {
                ChooseAction(attacker, defender);
            }
        }

        public void ChooseAction(Player playerAttacking, Player playerDefending)
        {
            Console.WriteLine($"{playerAttacking.Name}, press 1 to attack, or 2 to split.");
            var response = Console.ReadLine();
            var responding = true;
            while (responding)
            {
                responding = false;
                switch (response)
                {
                    case "1":
                        InitializeAttack(playerAttacking, playerDefending);
                        break;
                    case "2":
                        SplitHands(playerAttacking);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        Console.ReadKey();
                        responding = true;
                        break;
                }
            }
        }

        public void InitializeAttack(Player attacker, Player defender)
        {
            bool autoSelect = PickAttackingHandCheckForOptions(attacker, defender);
            if (autoSelect == false)
                HumanPickHandForAttack(attacker, defender);
        }

        private bool PickAttackingHandCheckForOptions(Player attacker, Player defender)
        {
            if (attacker.LeftHand == 0)
            {
                Console.WriteLine("You can only attack with your Right hand:");
                AttackerTarget(attacker.Name, attacker.RightHand, defender);
                return true;
            }
            if (attacker.RightHand == 0)
            {
                Console.WriteLine("You can only attack with your Left hand:");
                AttackerTarget(attacker.Name ,attacker.LeftHand, defender);
                return true;
            }

            return false;
        }

        public void HumanPickHandForAttack(Player attacker, Player defender)
        {
            Console.WriteLine($"{attacker.Name}, Choose your hand to attack with:\n" +
                              "Press 1 to Attack the Opponent with your left hand\n" +
                              "Press 2 to Attack the Opponent with your right hand");
            
            bool responding = true;
            while (responding)
            {
                string response = Console.ReadLine();
                responding = false;
                switch (response)
                {
                    case "1":
                        AttackerTarget(attacker.Name, attacker.LeftHand, defender);
                        break;
                    case "2":
                        AttackerTarget(attacker.Name, attacker.RightHand, defender);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        responding = true;
                        break;
                }
            }
        }

        public void AttackerTarget(string name, int attackingNumber, Player defender)
        {
            bool autoSelect = PickDefendingHandCheckForAutomaticTarget(name, attackingNumber, defender);
            if (autoSelect == false)
                AttackerPickTarget(name, attackingNumber, defender);
        }

        public bool PickDefendingHandCheckForAutomaticTarget(string name, int attackingNumber, Player defender)
        {
            if (defender.LeftHand == 0)
            {
                Console.WriteLine("You attack the right hand:\n" +
                                  "Press anything to continue...");
                Console.ReadKey();
                defender.RightHand = GameLogic.CalculateHandAfterAttack(attackingNumber, defender.RightHand);
                return true;
            }
            if (defender.RightHand == 0)
            {
                Console.WriteLine("You attack the left hand:\n" +
                                  "Press anything to continue...");
                Console.ReadKey();
                defender.LeftHand = GameLogic.CalculateHandAfterAttack(attackingNumber, defender.LeftHand);
                return true;
            }

            return false;
        }

        public void AttackerPickTarget(string name, int attackingNumber, Player defender)
        {
            Console.WriteLine($"{name}, Choose your target:\n" +
                              "Press 1 to Attack the Opponent's left hand\n" +
                              "Press 2 to Attack the Opponent's right hand");
            
            bool responding = true;
            while (responding)
            {
                string response = Console.ReadLine();
                responding = false;
                switch (response)
                {
                    case "1":
                        defender.LeftHand = GameLogic.CalculateHandAfterAttack(attackingNumber, defender.LeftHand);
                        break;
                    case "2":
                        defender.RightHand = GameLogic.CalculateHandAfterAttack(attackingNumber, defender.RightHand);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        responding = true;
                        break;
                }
            }
        }

        public void SplitHands(Player player)
        {
            int leftHand = player.LeftHand;
            int rightHand = player.RightHand;
            bool responding = true;
            while (responding)
            {
                Console.WriteLine("Input the total you want to allocate in your left hand:");
                var response = Convert.ToInt32(Console.ReadLine());
                if (response == leftHand || response == rightHand || response >= 4)
                {
                    Console.WriteLine("You can't kill a hand, or just swap the numbers.\n" +
                                      "Press anything to continue...");
                    Console.ReadKey();
                    continue;
                }
                player.LeftHand = response;
                player.RightHand = leftHand + rightHand - response;
                responding = false;
            }
        }

        private void GameOver(Player winner)
        {
            Console.WriteLine($"{winner.Name} Wins!");
            Console.ReadKey();
        }

        public bool ComputerTurn(Player computer, Player human)
        {
            var randomizedDecision = GameLogic.PickRandomNumberForComputer();
            switch (randomizedDecision)
            {
                case 1:
                    _playerOne.LeftHand = GameLogic.CalculateHandAfterAttack(_playerTwo.LeftHand, _playerOne.LeftHand);
                    break;
                case 2:
                    _playerOne.RightHand = GameLogic.CalculateHandAfterAttack(_playerTwo.LeftHand, _playerOne.RightHand);
                    break;
                case 3:
                    _playerOne.LeftHand = GameLogic.CalculateHandAfterAttack(_playerTwo.RightHand, _playerOne.LeftHand);
                    break;
                case 4:
                    _playerOne.RightHand = GameLogic.CalculateHandAfterAttack(_playerTwo.RightHand, _playerOne.RightHand);
                    break;
            }
            
            return _playerOne.LeftHand != 0 || _playerOne.RightHand != 0;
        }
    }
}
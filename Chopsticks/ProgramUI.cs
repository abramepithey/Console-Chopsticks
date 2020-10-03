using System;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace Chopsticks
{
    public class ProgramUI
    {
        private GameLogic _logic;
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
            _logic = new GameLogic(_playerOne, _playerTwo);
            bool playing = true;
            while (playing)
            {
                ShowCurrentTotals();
                playing = Attack(_playerOne, _playerTwo);
                if (!playing) continue;
                ShowCurrentTotals();
                playing = Attack(_playerTwo, _playerOne);
            }
        }

        public bool Attack(Player attacker, Player defender)
        {
            Console.WriteLine("Choose your attack:\n" +
                              "Press 1 to Attack the Opponent's left hand with your left hand\n" +
                              "Press 2 to Attack the Opponent's right hand with your left hand\n" +
                              "Press 3 to Attack the Opponent's left hand with your right hand\n" +
                              "Press 4 to Attack the Opponent's right hand with your right hand");
            
            bool responding = true;
            while (responding)
            {
                string response = Console.ReadLine();
                responding = false;
                switch (response)
                {
                    case "1":
                        defender.LeftHand = _logic.CalculateHand(attacker.LeftHand, defender.LeftHand);
                        break;
                    case "2":
                        defender.RightHand = _logic.CalculateHand(attacker.LeftHand, defender.RightHand);
                        break;
                    case "3":
                        defender.LeftHand = _logic.CalculateHand(attacker.RightHand, defender.LeftHand);
                        break;
                    case "4":
                        defender.RightHand = _logic.CalculateHand(attacker.RightHand, defender.RightHand);
                        break;
                    default:
                        Console.WriteLine("Please enter a valid selection.");
                        responding = true;
                        break;
                }
            }
            
            return defender.LeftHand != 0 || defender.RightHand != 0;
        }

        public void CreatePlayerOne()
        {
            Console.WriteLine("Enter a name for Player 1:");
            _playerOne = new Player(Console.ReadLine());
        }

        public void CreatePlayerTwo()
        {
            Console.WriteLine("Enter a name for Player 2:");
            _playerTwo = new Player(Console.ReadLine());
        }

        private void ShowCurrentTotals()
        {
            Console.Clear();
            Console.WriteLine($"Player 1: Left - {_playerOne.LeftHand}   Right - {_playerOne.RightHand}\n\n" +
                              $"Player 2: Left - {_playerTwo.LeftHand}   Right - {_playerTwo.RightHand}");
        }
        
        public void ComputerTurn(Player computer, Player human)
        {
            int randomizedDecision = _logic.PickRandomNumberForComputer();
            switch (randomizedDecision)
            {
                case 1:
                    _playerOne.LeftHand = _logic.CalculateHand(_playerTwo.LeftHand, _playerOne.LeftHand);
                    break;
                case 2:
                    _playerOne.RightHand = _logic.CalculateHand(_playerTwo.LeftHand, _playerOne.RightHand);
                    break;
                case 3:
                    _playerOne.LeftHand = _logic.CalculateHand(_playerTwo.RightHand, _playerOne.LeftHand);
                    break;
                case 4:
                    _playerOne.RightHand = _logic.CalculateHand(_playerTwo.RightHand, _playerOne.RightHand);
                    break;
            }
        }
    }
}
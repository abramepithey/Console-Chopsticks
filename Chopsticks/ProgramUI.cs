using System;
using System.Threading;
using Microsoft.VisualBasic.CompilerServices;

namespace Chopsticks
{
    public class ProgramUI
    {
        private GameLogic _logic;
        
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

        public bool MenuChoice()
        {
            string response = Console.ReadLine()?.ToLower();
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

        private void Rules()
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
        }

        private void Exit()
        {
            Console.WriteLine("Exiting program...");
            Thread.Sleep(2000);
        }

        public void PlayComputer()
        {
            
        }

        public void PlayHuman()
        {
            _logic = new GameLogic();
            Console.WriteLine("Enter the name for player one:");
            string firstNameResponse = Console.ReadLine();
            Console.WriteLine("Enter the name for player two:");
            string secondNameResponse = Console.ReadLine();
            _logic.NamePlayers(firstNameResponse, secondNameResponse);
            bool playing = true;
            while (playing)
            {
                
            }
        }
    }
}
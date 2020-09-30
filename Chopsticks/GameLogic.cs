using System;

namespace Chopsticks
{
    public class GameLogic
    {
        Player playerOne = new Player();
        Player playerTwo = new Player();
        
        public int CalculateHand(int attackHand, int defendHand)
        {
            int newDefendHand = defendHand + attackHand;
            if (newDefendHand == 5)
                return 0;
            if (newDefendHand > 5)
                return newDefendHand - 5;
            return newDefendHand;
        }

        public int PickRandomNumberForComputer()
        {
            Random randNum = new Random();
            return randNum.Next(1, 3);
        }

        public void NamePlayers(string nameOne, string nameTwo)
        {
            playerOne.Name = nameOne;
            playerTwo.Name = nameTwo;
        }
    }
}
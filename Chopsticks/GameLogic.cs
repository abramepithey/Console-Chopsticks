using System;

namespace Chopsticks
{
    public class GameLogic
    {
        public int CalculateHand(int attackHand, int defendHand)
        {
            int newDefendHand = defendHand + attackHand;
            if (newDefendHand >= 5)
                return newDefendHand - 5;
            return newDefendHand;
        }

        public int PickRandomNumberForComputer()
        {
            Random randNum = new Random();
            return randNum.Next(1, 3);
        }
    }
}
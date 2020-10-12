using System;

namespace Chopsticks
{
    public static class GameLogic
    {
        public static int CalculateHand(int attackHand, int defendHand)
        {
            int newDefendHand = defendHand + attackHand;
            if (newDefendHand >= 5)
                return newDefendHand - 5;
            return newDefendHand;
        }

        public static int PickRandomNumberForComputer()
        {
            Random randNum = new Random();
            return randNum.Next(1, 3);
        }
    }
}
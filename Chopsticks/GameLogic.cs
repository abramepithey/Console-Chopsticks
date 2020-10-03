using System;

namespace Chopsticks
{
    public class GameLogic
    {
        private Player _playerOne;
        private Player _playerTwo;

        public GameLogic(Player playerOne, Player playerTwo)
        {
            _playerOne = playerOne;
            _playerTwo = playerTwo;
        }

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
            return randNum.Next(1, 5);
        }
    }
}
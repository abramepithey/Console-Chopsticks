namespace Chopsticks
{
    public class Player
    {
        public string Name { get; set; } = "VGER";
        public int LeftHand { get; set; } = 1;
        public int RightHand { get; set; } = 4;

        public Player(string name)
        {
            Name = name;
        }
        
        public Player() {}
    }
}
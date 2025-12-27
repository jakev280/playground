namespace TinyDungeon
{
    class Player
    {
        public int Health = 100;
        public int MaxHealth = 100;
        public int Level = 1;
        public int XP = 0;

        public void GainXP(int amount)
        {
            XP += amount;
            if (XP >= Level * 50)
            {
                Level++;
                MaxHealth += 10;
                Health = MaxHealth;
                XP = 0;
                Console.WriteLine($"You leveled up! Now level {Level}");
            }
        }
    }
}

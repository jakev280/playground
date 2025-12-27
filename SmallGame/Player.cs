using System;

namespace TinyDungeon
{
    class Player
    {
        public string Name = "Hero";
        
        public int MaxHealth = 100;
        public int Health = 100;

        public int Strength = 10;
        public int Dexterity = 10;
        public int Constitution = 10;

        public int Level = 1;
        public int XP = 0;

        public int AttackModifier => (Strength - 10) / 2;
        public int Defense => 10 + (Dexterity - 10) / 2;

        public void GainXP(int amount)
        {
            XP += amount;
            if (XP >= Level * 50)
            {
                Level++;
                MaxHealth += 10;
                Health = MaxHealth;
                Strength++;
                Constitution++;
                XP = 0;
                Console.WriteLine("You feel more experienced. You level up!");
            }
        }
    }
}
 
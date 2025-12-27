using System;

namespace TinyDungeon
{
    class Enemy
    {
        public string Name;
        public int MinDamage;
        public int MaxDamage;
        public double AttackChance;

        public Enemy(string name, int minDamage, int maxDamage, double attackChance)
        {
            Name = name;
            MinDamage = minDamage;
            MaxDamage = maxDamage;
            AttackChance = attackChance;
        }

        public int Attack(Random rng)
        {
            return rng.Next(MinDamage, MaxDamage + 1);
        }
    }
}
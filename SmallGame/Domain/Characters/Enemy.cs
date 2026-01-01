using System;
using SmallGame.Core; // Needed to use the Dice class

namespace SmallGame.Domain.Characters
{
    public class Enemy
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int MaxHealth { get; set; }
        public int AttackBonus { get; set; }
        public double AttackChance { get; set; }

        public Enemy(string name, int healthMultiplier, int attackBonus, int dieSides, double attackChance)
        {
            Name = name;
            MaxHealth = healthMultiplier * 10;
            Health = MaxHealth;
            AttackBonus = attackBonus;
            AttackChance = attackChance;
        }

        public int RollDamage()
        {
            // Uses your static Dice class in the Core folder
            return Dice.Roll(1, 8) + AttackBonus;
        }
    }
}
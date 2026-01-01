using System;

namespace SmallGame.Domain.Characters
{
    public class Player
    {
        public string Name { get; set; } = "Hero";
        public int Health { get; set; } = 100;
        public int MaxHealth { get; set; } = 100;
        public int AttackModifier { get; set; } = 2;
        public int Defense { get; set; } = 12;

        public void TakeDamage(int damage)
        {
            Health -= damage;
            if (Health < 0) Health = 0;
        }

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth) Health = MaxHealth;
        }
    }
}
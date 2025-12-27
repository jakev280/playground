using System;

namespace TinyDungeon
{
    class Enemy
    {
        public string Name;

        public double AttackChance;
        public int AttackBonus;
        public int DamageDiceCount;
        public int DamageDiceSides;

        public Enemy(
            string name,
            int attackBonus,
            int damageDiceCount,
            int damageDiceSides,
            double attackChance)
        {
            Name = name;
            AttackBonus = attackBonus;
            DamageDiceCount = damageDiceCount;
            DamageDiceSides = damageDiceSides;
            AttackChance = attackChance;
        }

        public int RollDamage()
        {
            return Dice.Roll(DamageDiceCount, DamageDiceSides);
        }
    }
}

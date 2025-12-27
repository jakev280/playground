namespace TinyDungeon
{
    class Enemy
    {
        public string Name;
        public int MaxHealth;
        public int Health;
        public double AttackChance;
        public int AttackBonus;
        public int DamageDiceCount;
        public int DamageDiceSides;

        public Enemy(string name, int attackBonus, int diceCount, int diceSides, double chance)
        {
            Name = name;
            AttackBonus = attackBonus;
            DamageDiceCount = diceCount;
            DamageDiceSides = diceSides;
            AttackChance = chance;

            MaxHealth = diceCount * 5;
            Health = MaxHealth;
        }

        public int RollDamage()
        {
            return Dice.Roll(DamageDiceCount, DamageDiceSides);
        }
    }
}

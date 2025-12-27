namespace TinyDungeon
{
    class Player
    {
        public string Name = "Nameless Hero";
        public int MaxHealth = 100;
        public int Health = 100;

        public int AttackModifier = 2;
        public int Defense = 12;

        public void Heal(int amount)
        {
            Health += amount;
            if (Health > MaxHealth)
                Health = MaxHealth;
        }

        public void TakeDamage(int amount)
        {
            Health -= amount;
            if (Health < 0)
                Health = 0;
        }
    }
}

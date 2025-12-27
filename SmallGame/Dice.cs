using System;

namespace TinyDungeon
{
    static class Dice
    {
        static Random rng = new Random();

        public static int D(int sides)
        {
            return rng.Next(1, sides + 1);
        }

        public static int Roll(int count, int sides)
        {
            int total = 0;
            for (int i = 0; i < count; i++)
                total += D(sides);
            return total;
        }
    }
}

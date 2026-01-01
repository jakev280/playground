using System;

namespace SmallGame.Core
{
    public static class Dice
    {
        private static Random rng = new Random();

        public static int D(int sides)
        {
            return rng.Next(1, sides + 1);
        }

        public static int Roll(int number, int sides)
        {
            int total = 0;
            for (int i = 0; i < number; i++)
            {
                total += D(sides);
            }
            return total;
        }
    }
}
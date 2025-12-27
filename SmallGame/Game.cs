using System;
using System.Threading;

namespace TinyDungeon
{
    class Game
    {
        Player player = new Player();
        Random rng = new Random();

        int score = 0;
        int healCooldown = 5;
        int healTimer = 0;

        Enemy[] enemies =
        {
            new Enemy("Goblin", 4, 1, 6, 0.6),
            new Enemy("Orc", 6, 2, 6, 0.4),
            new Enemy("Skeleton", 5, 1, 8, 0.5)
        };

        public void Run()
        {
            CreateCharacter();

            bool running = true;
            while (running)
            {
                Console.Clear();
                Enemy current = enemies[rng.Next(enemies.Length)];

                DrawUI(current);

                Console.WriteLine("Choose your action:");
                Console.WriteLine("[A] Attack   [H] Heal   [Q] Quit");

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.Q)
                    break;

                if (key == ConsoleKey.H && healTimer == 0)
                {
                    int heal = 20;
                    player.Health = Math.Min(player.Health + heal, player.MaxHealth);
                    healTimer = healCooldown;
                    Console.WriteLine($"You regain {heal} HP.");
                }

                if (key == ConsoleKey.A)
                {
                    PlayerAttack(current);
                }

                EnemyTurn(current);

                if (healTimer > 0)
                    healTimer--;

                score++;
                player.GainXP(1);

                if (player.Health <= 0)
                {
                    Console.WriteLine("Your vision fades. You have fallen.");
                    break;
                }

                Console.WriteLine();
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }

            Console.Clear();
            Console.WriteLine($"Final Score: {score}");
            Console.WriteLine("Thanks for playing.");
            Console.ReadKey();
        }

        void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("Welcome, adventurer.");
            Console.Write("What is your name? ");

            string? input = Console.ReadLine();
            player.Name = string.IsNullOrWhiteSpace(input)
                ? "Nameless Hero"
                : input.Trim();

            Console.WriteLine();
            Console.WriteLine($"Very well, {player.Name}. Your journey begins...");
            Console.ReadKey(true);
        }

        void DrawUI(Enemy enemy)
        {
            Console.WriteLine("+---------------------------+");
            Console.WriteLine("|       TINY DUNGEON        |");
            Console.WriteLine("+---------------------------+");
            Console.WriteLine($"{player.Name}");
            Console.WriteLine($"HP: {player.Health}/{player.MaxHealth}  Level: {player.Level}  XP: {player.XP}");
            Console.WriteLine($"Score: {score}");
            Console.WriteLine();
            Console.WriteLine($"A {enemy.Name} blocks your path.");
            Console.WriteLine();
        }

        void PlayerAttack(Enemy enemy)
        {
            int roll = Dice.D(20) + player.AttackModifier;
            Console.WriteLine($"You attack! (Roll: {roll})");

            if (roll >= 10) // simple AC for now
            {
                int dmg = Dice.Roll(1, 8) + player.AttackModifier;
                Console.WriteLine($"You strike the {enemy.Name} for {dmg} damage!");
            }
            else
            {
                Console.WriteLine("Your attack misses.");
            }
        }

        void EnemyTurn(Enemy enemy)
        {
            if (rng.NextDouble() > enemy.AttackChance)
            {
                Console.WriteLine($"The {enemy.Name} hesitates.");
                return;
            }

            int roll = Dice.D(20) + enemy.AttackBonus;

            if (roll >= player.Defense)
            {
                int dmg = Dice.Roll(enemy.DamageDiceCount, enemy.DamageDiceSides);
                player.Health -= dmg;
                Console.WriteLine($"The {enemy.Name} hits you for {dmg} damage!");
            }
            else
            {
                Console.WriteLine($"The {enemy.Name} attacks but misses!");
            }
        }
    }
}

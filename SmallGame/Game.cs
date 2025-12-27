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

        Enemy[] enemies = {
            new Enemy("Goblin", 5, 10, 0.2),
            new Enemy("Orc", 10, 20, 0.1),
            new Enemy("Trap", 15, 25, 0.05)
        };

        public void Run()
        {
            bool running = true;
            while (running)
            {
                Console.Clear();
                        // Pick a random enemy for this frame
                Enemy current = enemies[rng.Next(enemies.Length)];

                // Display UI
                Console.WriteLine("+----------------+");
                Console.WriteLine("|  TINY DUNGEON  |");
                Console.WriteLine("+----------------+");
                Console.WriteLine($"Health: {player.Health}/{player.MaxHealth}  Level: {player.Level}  XP: {player.XP}");
                Console.WriteLine($"Score: {score}");
                Console.WriteLine();
                Console.WriteLine("Press D to take damage, H to heal, Q to quit");
                Console.WriteLine("Enemies ahead:");
                Console.WriteLine($"{current.Name}");
                Console.WriteLine();

                // Enemy attacks
                if (rng.NextDouble() < current.AttackChance)
                {
                    int dmg = current.Attack(rng);
                    player.Health -= dmg;
                    if (player.Health < 0) player.Health = 0;
                    Console.WriteLine($"{current.Name} hits you for {dmg} HP!");
                }

                // Random events
                if (rng.NextDouble() < 0.05) // 5% chance for treasure
                {
                    int heal = rng.Next(5, 16);
                    player.Health += heal;
                    if (player.Health > player.MaxHealth) player.Health = player.MaxHealth;
                    Console.WriteLine($"You found a potion! +{heal} HP");
                }

                if (rng.NextDouble() < 0.03) // 3% chance trap
                {
                    int trap = rng.Next(5, 20);
                    player.Health -= trap;
                    if (player.Health < 0) player.Health = 0;
                    Console.WriteLine($"A trap triggers! -{trap} HP");
                }

                // Input handling
                if (Console.KeyAvailable)
                {
                    ConsoleKey key = Console.ReadKey(true).Key;

                    if (key == ConsoleKey.D)
                    {
                        player.Health -= 10;
                        if (player.Health < 0) player.Health = 0;
                    }
                    else if (key == ConsoleKey.H && healTimer == 0)
                    {
                        int healAmount = 20;
                        player.Health += healAmount;
                        if (player.Health > player.MaxHealth) player.Health = player.MaxHealth;
                        healTimer = healCooldown;
                        Console.WriteLine($"You healed {healAmount} HP!");
                    }
                    else if (key == ConsoleKey.Q)
                    {
                        running = false;
                        continue;
                    }
                }

                // Update timers
                if (healTimer > 0) healTimer--;

                // Score and XP
                score++;
                player.GainXP(1);

                // Check death
                if (player.Health <= 0)
                {
                    Console.WriteLine("You died! Game Over.");
                    break;
                }

                Thread.Sleep(500); // frame timing
            }

            Console.WriteLine($"Final Score: {score}");
            Console.WriteLine("Thanks for playing.");
            Console.ReadKey();
        }
    }
}

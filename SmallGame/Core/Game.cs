using System;
using System.IO;
using SmallGame.Core; 
using SmallGame.Domain.Characters;

namespace SmallGame.Core
{
    class Game
    {
        Player player = new Player();
        Random rng = new Random();
        int healCooldown = 5;
        int healTimer = 0;
        int score = 0;

        public void Run()
        {
            CreateCharacter();

            // Phase 1
            StoryPhase1();
            BattlePhase(GetRandomEnemy(1));

            // Phase 2
            StoryPhase2();
            BattlePhase(GetRandomEnemy(2));

            // Phase 3
            StoryPhase3();
            BossBattle();

            Console.Clear();
            Console.WriteLine("Congratulations! You completed the dungeon!");
            Console.WriteLine($"Final Score: {score}");
            Console.ReadKey();
        }

        void CreateCharacter()
        {
            Console.Clear();
            Console.WriteLine("Welcome, adventurer!");
            Console.Write("Enter your name: ");
            string? input = Console.ReadLine();
            player.Name = string.IsNullOrWhiteSpace(input) ? "Nameless Hero" : input.Trim();
            Console.WriteLine($"Very well, {player.Name}. Your journey begins...");
            Console.ReadKey(true);
        }

        Enemy GetRandomEnemy(int difficulty)
        {
            if (difficulty == 1)
            {
                return new Enemy("Goblin", 4, 1, 6, 0.6);
            }
            else
            {
                return new Enemy("Orc", 6, 2, 6, 0.5);
            }
        }

        void BattlePhase(Enemy enemy)
        {
            Console.Clear();
            Console.WriteLine($"A {enemy.Name} appears!");
            Console.ReadKey(true);

            while (enemy.Health > 0 && player.Health > 0)
            {
                Console.Clear();
                Console.WriteLine($"{player.Name}: {player.Health}/{player.MaxHealth} HP");
                Console.WriteLine($"{enemy.Name}: {enemy.Health}/{enemy.MaxHealth} HP");
                Console.WriteLine("Choose your action: [A] Attack  [H] Heal  [R] Run");

                ConsoleKey key = Console.ReadKey(true).Key;

                if (key == ConsoleKey.A)
                    PlayerAttack(enemy);
                else if (key == ConsoleKey.H && healTimer == 0)
                {
                    player.Heal(20);
                    healTimer = healCooldown;
                    Console.WriteLine("You healed 20 HP!");
                    Console.ReadKey(true);
                }
                else if (key == ConsoleKey.R)
                {
                    Console.WriteLine("You flee!");
                    Console.ReadKey(true);
                    break;
                }

                if (enemy.Health > 0)
                    EnemyTurn(enemy);

                if (healTimer > 0)
                    healTimer--;

                Console.WriteLine("Press any key to continue...");
                Console.ReadKey(true);
            }

            if (enemy.Health <= 0)
            {
                Console.WriteLine($"You defeated the {enemy.Name}!");
                score += 10;
                Console.ReadKey(true);
            }
        }

        void BossBattle()
        {
            Enemy boss = new Enemy("Dragon", 8, 4, 8, 0.7);
            Console.Clear();
            
            // Replaced AsciiArt.ShowBoss() with your new dragon.txt
            PrintArt("dragon.txt");
            
            Console.ReadKey(true);
            BattlePhase(boss);
        }

        void PlayerAttack(Enemy enemy)
        {
            int roll = Dice.D(20) + player.AttackModifier;
            if (roll >= 10)
            {
                int dmg = Dice.Roll(1, 8) + player.AttackModifier;
                enemy.Health -= dmg;
                Console.WriteLine($"You hit the {enemy.Name} for {dmg} damage!");
            }
            else
                Console.WriteLine("Your attack misses!");
            Console.ReadKey(true);
        }

        void EnemyTurn(Enemy enemy)
        {
            if (rng.NextDouble() > enemy.AttackChance)
            {
                Console.WriteLine($"{enemy.Name} hesitates.");
                return;
            }

            int roll = Dice.D(20) + enemy.AttackBonus;
            if (roll >= player.Defense)
            {
                int dmg = enemy.RollDamage();
                player.TakeDamage(dmg);
                Console.WriteLine($"{enemy.Name} hits you for {dmg} damage!");
            }
            else
                Console.WriteLine($"{enemy.Name} misses!");
            Console.ReadKey(true);
        }

        void StoryPhase1()
        {
            Console.Clear();
            
            // Replaced AsciiArt.ShowDungeonEntrance() with your new title.txt
            PrintArt("title.txt");
            
            Console.WriteLine("You enter the dungeon and see three paths: Left, Right, Straight.");
            Console.WriteLine("[L] Left  [R] Right  [S] Straight");

            ConsoleKey key = Console.ReadKey(true).Key;
            Console.WriteLine(key switch
            {
                ConsoleKey.L => "You chose Left.",
                ConsoleKey.R => "You chose Right.",
                _ => "You go Straight."
            });

            Console.WriteLine("[H] Heal  [Enter] Continue");
            key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.H && healTimer == 0)
            {
                player.Heal(20);
                healTimer = healCooldown;
                Console.WriteLine("You healed 20 HP.");
            }

            Console.ReadKey(true);
        }

        void StoryPhase2()
        {
            Console.Clear();
            Console.WriteLine("You find a treasure chest containing a potion. +20 HP");
            
            // Note: ShowTreasureChest() was deleted; no chest.txt exists in your list.
            
            player.Heal(20);
            Console.ReadKey(true);
        }

        void StoryPhase3()
        {
            Console.Clear();
            Console.WriteLine("You hear a roar... The final boss awaits!");
            Console.WriteLine("[H] Heal  [Enter] Continue");

            ConsoleKey key = Console.ReadKey(true).Key;
            if (key == ConsoleKey.H && healTimer == 0)
            {
                player.Heal(20);
                healTimer = healCooldown;
                Console.WriteLine("You healed 20 HP.");
            }

            Console.ReadKey(true);
        }

        // Added this helper to handle reading from Assets/Ascii/
        void PrintArt(string fileName)
        {
            string path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Assets", "Ascii", fileName);
            if (File.Exists(path))
            {
                Console.WriteLine(File.ReadAllText(path));
            }
            else
            {
                Console.WriteLine($"[Art file {fileName} not found]");
            }
        }
    }
}
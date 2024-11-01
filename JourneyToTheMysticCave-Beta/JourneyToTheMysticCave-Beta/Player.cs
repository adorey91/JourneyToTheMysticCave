using System;
using System.Windows.Input;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JourneyToTheMysticCave_Beta.Items;

namespace JourneyToTheMysticCave_Beta
{
    internal class Player : GameEntity
    {
        int dirX;
        int dirY;
        bool inDeep = false;
        int moveCount;

        public bool attackedEnemy = false;
        public bool itemPickedUp = false;
        private Enemy lastEncountered;

        public int killCount = 0;
        public int moneyCount = 0;
        public bool bossIsDead = false;

        public bool poisonBoots = false;

        public Enemy GetLastEnountered()
        {
            return lastEncountered;
        }

        Map map;
        GameStats gameStats;
        EnemyManager enemyManager;
        LegendColors legendColors;
        LevelManager levelManager;
        ItemManager itemManager;
        Shop shop;


        public Player()
        {
            healthSystem = new HealthSystem();
        }

        public void Init(Map map, GameStats gameStats, LegendColors legendColors, EnemyManager enemyManager, LevelManager levelManager, ItemManager itemManager, Shop shop)
        {
            this.map = map;
            this.gameStats = gameStats;
            this.legendColors = legendColors;
            this.enemyManager = enemyManager;
            this.levelManager = levelManager;
            this.itemManager = itemManager;
            this.shop = shop;

            healthSystem.health = gameStats.PlayerHealth;
            character = gameStats.PlayerCharacter;
            pos = gameStats.PlayerPos;
            damage = gameStats.PlayerDamage;
            name = gameStats.PlayerName;
        }

        public void Update()
        {
            Movement();
            TrackKillCount();
        }

        public void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);

            legendColors.MapColor(character);
            if (map.GetCurrentMapContent()[pos.y, pos.x] == 'P')
                Console.BackgroundColor = ConsoleColor.DarkGreen;
            else if (map.GetCurrentMapContent()[pos.y, pos.x] == '~')
                Console.BackgroundColor = ConsoleColor.Blue;

            Console.Write(character);
            Console.ResetColor();
            Console.CursorVisible = false;
        }

        private void Movement()
        {
            if (!healthSystem.mapDead)
            {
                PlayerInput();

                int newX = pos.x + dirX;
                int newY = pos.y + dirY;

                if (CheckBoundaries(newX, newY))
                {
                    lastEncountered = GetEnemyAtPosition(newX, newY);
                    if (lastEncountered != null)
                        AttackEnemy(lastEncountered);
                    if(!attackedEnemy)
                    {
                        CheckFloor(newX, newY);
                    }
                    attackedEnemy = false;
                }
            }
        }

        private void PlayerInput()
        {
            ConsoleKeyInfo input = Console.ReadKey(true); // Read key without displaying it

            dirX = 0;
            dirY = 0;

            switch (input.Key)
            {
                case ConsoleKey.W: dirY = -1; break;
                case ConsoleKey.S: dirY = 1; break;
                case ConsoleKey.A: dirX = -1; break;
                case ConsoleKey.D: dirX = 1; break;
                case ConsoleKey.Q: dirY = -1; dirX = -1; break;
                case ConsoleKey.E: dirY = -1; dirX = 1; break;
                case ConsoleKey.Z: dirY = 1; dirX = -1; break;
                case ConsoleKey.C: dirY = 1; dirX = 1; break;
                case ConsoleKey.D1: shop.BuyItem(1); break;
                case ConsoleKey.D2: shop.BuyItem(2); break;
                case ConsoleKey.D3: shop.BuyItem(3); break;
                case ConsoleKey.Spacebar: return; // using for testing, player doesn't move
                case ConsoleKey.Escape: System.Environment.Exit(0); return;
            }
        }


        private Enemy GetEnemyAtPosition(int x, int y)
        {
            foreach (Enemy enemy in enemyManager.enemies)
            {
                if (enemy.pos.x == x && enemy.pos.y == y)
                {
                    if ((enemy is Ranger && levelManager.mapLevel == 0) ||
                        (enemy is Mage && levelManager.mapLevel == 1) ||
                        (enemy is Melee && levelManager.mapLevel == 2) ||
                        (enemy is Boss && enemyManager.AreAllMeleeDead()))
                    {
                        return enemy;
                    }
                }
            }
            return null;
        }

        //Tracks kill count
        private void TrackKillCount()
        {
            for (int i = 0; i < enemyManager.enemies.Count; i++)
            {
                if (!enemyManager.enemies[i].processed && enemyManager.enemies[i].healthSystem.dead)
                {
                    killCount++;
                }
            }
        }

        private void AttackEnemy(Enemy enemy)
        {
            enemy.healthSystem.TakeDamage(damage, "Attacked");
            attackedEnemy = true;
        }

        private bool CheckBoundaries(int x, int y)
        {
            return map.GetCurrentMapContent()[y, x] != '#' && map.GetCurrentMapContent()[y, x] != '^';
        }

        private void CheckFloor(int x, int y)
        {
            if (map.GetCurrentMapContent()[y, x] == '~' && inDeep == false)
            {
                inDeep = true;
                pos = new Point2D { x = x, y = y };
                moveCount = 0;
            }
            else if (map.GetCurrentMapContent()[y, x] == 'P' && poisonBoots == false)
                healthSystem.TakeDamage(gameStats.PoisonDamage, "Floor");

            if(!inDeep)
                pos = new Point2D { x = x, y = y };

            if (moveCount == 1)
            {
                moveCount = 0;
                inDeep = false;
            }
            moveCount++;

            //Player Hits Shop
            if (map.GetCurrentMapContent()[y, x] == '%')
            {
                shop.PlayerEnters();
            }
            else
            {
                shop.PlayerExits();
            }
        }
    }
}
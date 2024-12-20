﻿using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class GameManager
    {
        #region Declarations
        Map map = new Map();
        GameStats gameStats = new GameStats();
        Gamelog gamelog = new Gamelog();
        Player player = new Player();
        LevelManager levelManager = new LevelManager();
        LegendColors legendColors = new LegendColors();
        HUD hUD = new HUD();
        QuestManager questManager = new QuestManager();
        EnemyManager enemyManager = new EnemyManager();
        ItemManager itemManager = new ItemManager();
        Shop shop = new Shop();

        bool gameOver = false;
        bool playerWon = false;
        #endregion

        public GameManager()
        {
            Init();
        }

        public void Gameplay()
        {
            TutorialText();
            map.Update();
            hUD.Update();
            shop.Update();
            questManager.AddQuests();
            questManager.Update();

            legendColors.Update();
            Draw();

            while (!gameOver)
            {
                Update();
                Draw();
                CheckGameOver();
            }
            Console.SetCursorPosition(0, 35);
            EndGame();
        }

        private void Init()
        {
            levelManager.Init(player);
            map.Init(levelManager, legendColors, player, enemyManager, itemManager);
            gameStats.Init(levelManager, map);
            player.Init(map, gameStats, legendColors, enemyManager, levelManager, itemManager, shop);
            legendColors.Init(gameStats, map, levelManager);
            enemyManager.Init(gameStats, levelManager, legendColors, gamelog, player, map);
            itemManager.Init(gameStats, levelManager, legendColors, gamelog, player, map, enemyManager);
            gamelog.Init(player, enemyManager, itemManager, gameStats, map, shop);
            hUD.Init(player, enemyManager, itemManager, map, legendColors);
            questManager.Init(player, enemyManager, itemManager, map);
            shop.Init(player, map);
        }

        private void Update()
        {
            player.Update();
            levelManager.Update();
            map.Update();
            legendColors.Update();
            enemyManager.Update();
            itemManager.Update();
            hUD.Update();
            shop.Update();
            questManager.Update();
            gamelog.Update();
        }

        private void Draw()
        {
            map.Draw();
            player.Draw();
            legendColors.Draw();
            itemManager.Draw();
            enemyManager.Draw();
            hUD.Draw();
            questManager.Draw();
            gamelog.Draw();
        }

        void TutorialText()
        {
            Console.CursorVisible = false;

            Console.WriteLine("===========================================================");
            Console.WriteLine("                 Journey To The Mystic Cave                ");
            Console.WriteLine("                        JSON Attempt                     ");
            Console.WriteLine("===========================================================");
            Console.WriteLine();

            Console.WriteLine("                     Move Controls Guide");
            Console.WriteLine();
            DisplayCompactControls("Up", "W", "Up-Left", "Q", "Up-Right", "E");
            DisplayCompactControls("Down", "S", "Down-Left", "Z", "Down-Right", "C");
            DisplayCompactControls("Left", "A", "Right", "D", null, null);

            Console.WriteLine();
            Console.WriteLine("===========================================================");
            Console.WriteLine("        Press any key to embark on your journey...");
            Console.WriteLine("===========================================================");
            Console.ReadKey(true);
            Console.Clear();
        }

        private void DisplayCompactControls(string label1, string key1, string label2, string key2, string label3, string key3)
        {
            if(string.IsNullOrWhiteSpace(label3) && string.IsNullOrWhiteSpace(key3))
                Console.WriteLine($"  {label1,-10}: {key1,-3}    {label2,-10}: {key2,-3}");
            else
                Console.WriteLine($"  {label1,-10}: {key1,-3}    {label2,-10}: {key2,-3}    {label3,-10}: {key3,-3}");
        }

        private void DisplaySymbolsInColumns(string direction, string description)
        {
            Console.Write($"{direction} = {description}");

            for (int i = 0; i < 5; i++)
            {
                Console.Write(" ");
            }
        }

        void CheckGameOver()
        {
            if (enemyManager.AreAllEnemiesDead() && itemManager.IsMoneyCollected())
            {
                gameOver = true;
                playerWon = true;
            }
            if (player.healthSystem.dead)
            {
                gameOver = true;
                playerWon = false;
            }
        }

        void EndGame()
        {
            if (!player.healthSystem.dead)
                Console.WriteLine(player.name + " has won! Press enter to exit");
            else
                Console.WriteLine(player.name + " has died, press enter to exit");

            ConsoleKeyInfo input = Console.ReadKey();

            while (input.Key != ConsoleKey.Enter)
            {
                Console.WriteLine("Press Enter");
                input = Console.ReadKey();
            }
            System.Environment.Exit(0);
        }
    }
}

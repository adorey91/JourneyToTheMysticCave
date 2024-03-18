﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class HUD
    {
        Player player;
        EnemyManager enemyManager;
        ItemManager itemManager;
        Map map;
        int columnCount;
        int rowCount;
        string health;


        public void Init(Player player, EnemyManager enemyManager, ItemManager itemManager, Map map)
        {
            this.player = player;
            this.enemyManager = enemyManager;
            this.itemManager = itemManager;
            this.map = map;
        }

        public void Update()
        {
            rowCount = map.GetMapRowCount() + 1;
            columnCount = 0;

            // Clear only the area where the HUD is displayed
            for (int i = 0; i < 9; i++) // Assuming 9 lines for HUD display
            {
                Console.SetCursorPosition(columnCount, rowCount + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        public void Draw()
        {
            
            Console.SetCursorPosition(columnCount, rowCount);

            HUDInfo();
        }

        private void HUDInfo()
        {
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine($"{player.name} Health: {player.healthSystem.health}");
            Console.WriteLine($"{player.name} Damage: {player.damage}");
            Console.WriteLine($"Last Enemy Attacked - {EnemyName()}");
            if (EnemyHealth() == 0)
                health = $"{EnemyName()} is dead";
            else
                health = $"{EnemyHealth()}";
            Console.WriteLine($"Enemy Health - {health}");
            Console.Write("Money Picked Up:");
            // need to add each amount of money in the item manager
            Console.WriteLine();
            Console.WriteLine("+-------------------------------+");
        }

        private string EnemyName()
        {
            Enemy enemy = player.GetLastEnountered();
            if( enemy != null )
            {
                string fullTypeName = enemy.GetType().ToString();
                string[] parts = fullTypeName.Split('.');
                string enemyName = parts[parts.Length - 1];
                return enemyName;
            }
            else
                return string.Empty;
        }

        private int? EnemyHealth()
        {
            Enemy enemy = player.GetLastEnountered();
            if (enemy != null)
            {
                int enemyHealth = enemy.healthSystem.health;
                return enemyHealth;
            }
            else
                return null;
        }

    }
}

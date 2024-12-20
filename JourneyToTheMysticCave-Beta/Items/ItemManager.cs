﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace JourneyToTheMysticCave_Beta
{
    internal class ItemManager
    {
        public List<Item> items;
        public GameStats stats;
        LegendColors legendColors;
        LevelManager levelManager;
        EnemyManager enemyManager;
        Gamelog log;
        Player player;
        Map map;
        Random random = new Random();

        public int itemsLevel0;
        public int itemsLevel1;

        public ItemManager()
        {
            items = new List<Item>();
        }

        public void Init(GameStats stats, LevelManager levelManager, LegendColors legendColors, Gamelog log, Player player, Map map, EnemyManager enemyManager)
        {
            this.stats = stats;
            this.levelManager = levelManager;
            this.legendColors = legendColors;
            this.log = log;
            this.player = player;
            this.map = map;
            this.enemyManager = enemyManager;

            DistributeItems(1, 3, 1, 0, 0);
            DistributeItems(2, 3, 1, 5, 1);
            DistributeItems(3, 3, 1, 25, 2);
        }

        public void Update()
        {
            if (levelManager.mapLevel == 0)
            {
                for (int i = 0; i < itemsLevel0; i++)
                    items[i].Update();
            }
            else if (levelManager.mapLevel == 1)
            {
                for (int i = itemsLevel0; i < itemsLevel1; i++)
                    items[i].Update();
            }
            if (levelManager.mapLevel == 2)
            {
                for (int i = itemsLevel1; i < items.Count; i++)
                    items[i].Update();
            }
        }

        public void Draw()
        {
            if (levelManager.mapLevel == 0)
            {
                for (int i = 0; i < itemsLevel0; i++)
                    items[i].Draw();
            }
            else if (levelManager.mapLevel == 1)
            {
                for (int i = itemsLevel0; i < itemsLevel1; i++)
                    items[i].Draw();
            }
            if (levelManager.mapLevel == 2)
            {
                for (int i = itemsLevel1; i < items.Count; i++)
                    items[i].Draw();
            }
        }

        public bool IsMoneyCollected()
        {
            return items.Where(item => item is Money).All(money => money.collected);
        }

        private void DistributeItems(int potionCount, int moneyCount, int swordCount, int trapCount, int level)
        {
            int index = items.Count;

            // Distribute potions
            for (int i = 0; i < potionCount; i++)
            {
                var potion = new Potion(stats.Potion.Count, stats.Potion.Character, stats.Potion.Name, stats.Potion.Heal, legendColors, player);
                potion.pos = stats.PlaceCharacters(level, random);
                items.Add(potion);
            }

            // Distribute money
            for (int i = 0; i < moneyCount; i++)
            {
                var money = new Money(stats.Money.Count, stats.Money.Character, stats.Money.Name, legendColors, player);
                money.pos = stats.PlaceCharacters(level, random);
                items.Add(money);
            }

            // Distribute swords
            for (int i = 0; i < swordCount; i++)
            {
                var sword = new Sword(stats.Sword.Count, stats.Sword.Character, stats.Sword.Name, stats.Sword.Multiplier, legendColors, player);
                sword.pos = stats.PlaceCharacters(level, random);
                items.Add(sword);
            }

            // Distribute traps
            for (int i = 0; i < trapCount; i++)
            {
                var trap = new Trap(stats.Trap.Count, stats.Trap.Character, stats.Trap.Name, stats.Trap.Damage, legendColors, player, enemyManager, levelManager);
                trap.pos = stats.PlaceCharacters(level, random);
                items.Add(trap);
            }

            if (level == 0)
                itemsLevel0 = items.Count;
            else if (level == 1)
                itemsLevel1 = items.Count;

            // Update positions for newly added items
            for (int i = index; i < items.Count; i++)
            {
                while (items[i].pos.x == 0 && items[i].pos.y == 0)
                {
                    items[i].pos = stats.PlaceCharacters(level, random);
                }
            }
        }

    }
}

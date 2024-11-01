using System;
using System.Collections.Generic;
using System.Linq;

namespace JourneyToTheMysticCave_Beta
{
    internal class EnemyManager
    {
        private GameStats stats;
        private LevelManager levelManager;
        private LegendColors legendColors;
        private Gamelog log;
        private Player player;
        private Map map;
        
        Random random = new Random();
        public List<Enemy> enemies;
        public int meleeCount;
        
        bool firstDead = false;

        public EnemyManager()
        {
            enemies = new List<Enemy>();
        }

        public void Init(GameStats stats, LevelManager levelManager, LegendColors legendColors, Gamelog log, Player player, Map map)
        {
            this.stats = stats;
            this.levelManager = levelManager;
            this.legendColors = legendColors;
            this.log = log;
            this.player = player;
            this.map = map;

            for (int i = 0; i < stats.Ranger.Count; i++)
                enemies.Add(new Ranger(stats.Ranger.Count, stats.Ranger.Character, stats.Ranger.Name, stats.Ranger.Damage, stats.Ranger.Attack, legendColors, player, log, this, map, stats));
            for (int i = 0; i < stats.Mage.Count; i++)
                enemies.Add(new Mage(stats.Mage.Count, stats.Mage.Character, stats.Mage.Name, stats.Mage.Damage, stats.Mage.Attack, legendColors, player, log, map, this, stats));
            for (int i = 0; i < stats.Melee.Count; i++)
                enemies.Add(new Melee(stats.Melee.Count, stats.Melee.Character, stats.Melee.Name, stats.Melee.Damage, stats.Melee.Attack, legendColors, player, log, this, map, stats));
            for (int i = 0; i < stats.Boss.Count; i++)
                enemies.Add(new Boss(stats.Boss.Count, stats.Boss.Character, stats.Boss.Name, stats.Boss.Damage, stats.Boss.Attack, legendColors, player, log, this, map, stats));

            foreach (Enemy enemy in enemies)
            {
                switch (enemy.GetType().Name)
                {
                    case nameof(Ranger):
                        enemy.healthSystem = new HealthSystem();
                        enemy.healthSystem.health = stats.GiveHealth(random, "Ranger");
                        enemy.pos = stats.PlaceCharacters(0, random);
                        break;
                    case nameof(Mage):
                        enemy.healthSystem = new HealthSystem();
                        enemy.healthSystem.health = stats.GiveHealth(random, "Mage");
                        enemy.pos = stats.PlaceCharacters(1, random);
                        break;
                    case nameof(Melee):
                        enemy.healthSystem = new HealthSystem();
                        enemy.healthSystem.health = stats.GiveHealth(random, "Melee");
                        enemy.pos = stats.PlaceCharacters(2, random);
                        meleeCount++;
                        break;
                    case nameof(Boss):
                        enemy.healthSystem = new HealthSystem();
                        enemy.healthSystem.health = stats.Boss.Health;
                        break;
                }
            }
        }

        public void Update()
        {
            foreach (Enemy enemy in enemies)
            {
                if (levelManager.mapLevel == 0 && enemy.GetType().Name == nameof(Ranger))
                {
                    enemy.Update(random);
                }
                else if (levelManager.mapLevel == 1 && enemy.GetType().Name == nameof(Mage))
                {
                    enemy.Update(random);
                }
                else if (levelManager.mapLevel == 2)
                {
                    if (enemy.GetType().Name == nameof(Melee))
                        enemy.Update(random);
                    else if (enemy.GetType().Name == nameof(Boss) && AreAllMeleeDead())
                    {
                        if (AreAllMeleeDead() && firstDead == false)
                        {
                            enemy.pos = stats.PlaceCharacters(2, random);
                            firstDead = true;
                        }
                        enemy.Update(random);
                    }
                }
            }
        }

        public void Draw()
        {
            foreach (Enemy enemy in enemies)
            {
                if (levelManager.mapLevel == 0 && enemy.GetType().Name == nameof(Ranger))
                {
                    enemy.Draw();
                }
                else if (levelManager.mapLevel == 1 && enemy.GetType().Name == nameof(Mage))
                {
                    enemy.Draw();
                }
                else if (levelManager.mapLevel == 2)
                {
                    if (enemy.GetType().Name == nameof(Melee))
                        enemy.Draw();
                    else if (enemy.GetType().Name == nameof(Boss) && AreAllMeleeDead())
                    {
                        if (AreAllMeleeDead() && firstDead == false)
                        {
                            enemy.pos = stats.PlaceCharacters(2, random);
                            firstDead = true;
                        }
                        enemy.Draw();
                    }
                }
            }
        }

        public bool AreAllEnemiesDead()
        {
            return enemies.All(enemy => !enemy.IsAlive);
        }

        public bool AreAllMeleeDead()
        {
            return !enemies.Any(enemy => enemy is Melee && enemy.IsAlive);
        }
    }
}

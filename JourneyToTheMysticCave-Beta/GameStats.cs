using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;


namespace JourneyToTheMysticCave_Beta
{
    internal class GameStats
    {
        [JsonIgnore] LevelManager levelManager;
        [JsonIgnore] Random random = new Random();
        [JsonIgnore] Map map;

        // Properties for storing data
        public PlayerConfig Player { get; set; }
        public CharacterConfig Ranger { get; set; }
        public CharacterConfig Mage { get; set; }
        public CharacterConfig Melee { get; set; }
        public BossConfig Boss { get; set; }
        public ItemConfig Money { get; set; }
        public PotionConfig Potion { get; set; }
        public TrapConfig Trap { get; set; }
        public SwordConfig Sword { get; set; }
        public int PoisonDamage { get; set; }


        public void Init(LevelManager levelManager, Map map)
        {
            this.levelManager = levelManager;
            this.map = map;

            LoadGameConfig();
        }

        private void LoadGameConfig()
        {
            string jsonPath = "GameConfig.json";
            string jsonString = File.ReadAllText(jsonPath);
            GameConfig config = JsonSerializer.Deserialize<GameConfig>(jsonString);

            // Load into GameStats properties
            Player = config.Player;
            Ranger = config.Ranger;
            Mage = config.Mage;
            Melee = config.Melee;
            Boss = config.Boss;
            Money = config.Money;
            Potion = config.Potion;
            Trap = config.Trap;
            Sword = config.Sword;
            PoisonDamage = config.PoisonDamage;

            Debug.WriteLine($"Loaded player position: {Player.Pos.x}, {Player.Pos.y}");
        }


        public int GiveHealth(Random random, string type)
        {
            int health;
            switch (type)
            {
                case "Ranger":
                    health = random.Next(Ranger.MinHp, Ranger.MaxHp);
                    return health;
                case "Mage":
                    health = random.Next(Mage.MinHp, Mage.MaxHp);
                    return health;
                case "Melee":
                    health = random.Next(Melee.MinHp, Melee.MaxHp);
                    return health;
                default:
                    return 0;
            }
        }


        public Point2D PlaceCharacters(int levelNumber, Random random)
        {
            int x, y;

            do
            {
                x = random.Next(0, levelManager.AllMapContents[levelNumber].GetLength(1));
                y = random.Next(0, levelManager.AllMapContents[levelNumber].GetLength(0));
            } while (!CheckInitialPlacement(x, y, levelNumber));

            return new Point2D { x = x, y = y };
        }

        private bool CheckInitialPlacement(int x, int y, int levelNumber)
        {
            return levelManager.InitialBoundaries(x, y, levelNumber) && map.EmptySpace(x, y, levelNumber);
        }
    }
}
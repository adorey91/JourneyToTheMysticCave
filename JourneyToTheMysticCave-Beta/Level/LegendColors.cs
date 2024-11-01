using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class LegendColors
    {
        public GameStats gameStats;
        public Map map;
        public LevelManager levelManager;

        int columnCount;
        int rowCount = 0;
        int level;

        public void Init(GameStats gamestats, Map map, LevelManager levelManager)
        {
            this.gameStats = gamestats;
            this.map = map;
            this.levelManager = levelManager;
        }

        public void Update()
        {
            columnCount = map.GetMapColumnCount() + 2;
            rowCount = 0;
            level = levelManager.mapLevel;
        }

        public void Draw()
        {
            columnCount = map.GetMapColumnCount() + 2;
            rowCount = 0;
            Legend();
        }

        private void Legend() // displays legend on the bottom of the map.
        {
            Console.SetCursorPosition(columnCount, rowCount++);
            Console.WriteLine("+------------------------+");
            Console.SetCursorPosition(columnCount, rowCount++);
            Console.WriteLine("Map Legend:");
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar(gameStats.Player.Character, gameStats.Player.Name);
            Console.SetCursorPosition(columnCount, rowCount++);
            if (level == 0)
                DisplayChar(gameStats.Ranger.Character, gameStats.Ranger.Name);
            if (level == 1)
                DisplayChar(gameStats.Mage.Character, gameStats.Mage.Name);
            if (level == 2)
            {
                DisplayChar(gameStats.Melee.Character, gameStats.Melee.Name);
                Console.SetCursorPosition(columnCount, rowCount++);
                DisplayChar(gameStats.Boss.Character, gameStats.Boss.Name);
            }
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar(gameStats.Money.Character, gameStats.Money.Name);
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar(gameStats.Potion.Character, gameStats.Potion.Name);
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar(gameStats.Trap.Character, gameStats.Trap.Name);
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar(gameStats.Sword.Character, gameStats.Sword.Name);
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar('*', "Next Area");
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar('~', "Deep Water");
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar('P', "Poison Spill");
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar('^', "Mountains");
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar('#', "Walls");
            Console.SetCursorPosition(columnCount, rowCount++);
            DisplayChar('%', "Shop");
            Console.SetCursorPosition(columnCount, rowCount++);
            Console.WriteLine("+------------------------+");
        }

        private void DisplayChar(char symbol, string description)
        {
            MapColor(symbol);
            Console.Write(symbol);
            Console.ResetColor();
            Console.Write($" = {description}\n");
            Console.WriteLine();
        }

        public void MapColor(char c)    // handles map color
        {
            switch (c)
            {
                case '#': // Boundaries
                    Console.ForegroundColor = ConsoleColor.DarkGray;
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    break;
                case '%': // Boundaries
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    break;
                case '^': // Mountain
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.BackgroundColor = ConsoleColor.Gray;
                    break;
                case '~': // Water
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    Console.BackgroundColor = ConsoleColor.Blue;
                    break;
                case var _ when c == gameStats.Sword.Character: // Sword (item)
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
                case var _ when c == gameStats.Money.Character: // money (item)
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.BackgroundColor = ConsoleColor.Green;
                    break;
                case var _ when c == gameStats.Trap.Character: // trap (item)
                    Console.ForegroundColor = ConsoleColor.DarkCyan;
                    Console.BackgroundColor = ConsoleColor.Black;
                    break;
                case var _ when c == gameStats.Melee.Character: // Slime
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case var _ when c == gameStats.Ranger.Character: // Ranged enemy
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case var _ when c == gameStats.Mage.Character: // Mage
                    Console.ForegroundColor = ConsoleColor.Magenta;
                    break;
                case var _ when c == gameStats.Boss.Character: // boss - enemy
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    break;
                case var _ when c == gameStats.Player.Character: // (Player)
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    break;
                case var _ when c == gameStats.Potion.Character: // Potion (item)
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case '*': // next area
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case 'P': // poison floor
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                    break;
            }
        }
    }
}

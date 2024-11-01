using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class Program
    {
        static GameManager gameManager = new GameManager();

        static void Main(string[] args)
        {
            gameManager.Gameplay();
        }
    }
}

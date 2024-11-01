using System;
using System.Collections.Generic;

namespace JourneyToTheMysticCave_Beta
{
    internal class QuestManager
    {
        Player player;
        EnemyManager enemyManager;
        ItemManager itemManager;
        Map map;
        int columnCount;
        int rowCount;

        public List<Quest> quests;

        public void Init(Player player, EnemyManager enemyManager, ItemManager itemManager, Map map)
        {
            this.player = player;
            this.enemyManager = enemyManager;
            this.itemManager = itemManager;
            this.map = map;
            
            quests = new List<Quest>();
        }

        public void AddQuests()
        {
            Quest1 quest1 = new Quest1(player);
            quests.Add(quest1);

            Quest2 quest2 = new Quest2(player);
            quests.Add(quest2);

            Quest3 quest3 = new Quest3(player);
            quests.Add(quest3);
        }



        public void Update()
        {
            foreach (Quest quest in quests)
            {
                quest.Update();
            }

            rowCount = map.GetMapRowCount() + 13;
            columnCount = 0;

            for (int i = 0; i < 9; i++) 
            {
                Console.SetCursorPosition(columnCount, rowCount + i);
                Console.Write(new string(' ', Console.WindowWidth));
            }
        }

        public void Draw()
        {
            foreach (Quest quest in quests)
            {
                quest.Draw();
            }
            Console.SetCursorPosition(columnCount, rowCount);
            QuestInfo();
        }

        //Writes down quests and their completion status
        private void QuestInfo()
        {
            Console.WriteLine("+-------------------------------+");
            Console.WriteLine(quests[0].description);
            Console.WriteLine(quests[1].description);
            Console.WriteLine(quests[2].description);
            Console.WriteLine("+-------------------------------+");
        }


    }
}

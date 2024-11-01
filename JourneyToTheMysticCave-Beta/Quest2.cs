using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class Quest2 : Quest
    {

        public Quest2(Player player) :
        base(player)
        {
            this.player = player;
        }

        //See main quest class
        public override void Init()
        {
            completion = false;
        }


        //See main quest class
        public override void Update()
        {
            if (player.moneyCount >= 3)
            {
                completion = true;
            }
            else
            {
                //completion = false;
            }
        }

        //See main quest class
        public override void Draw()
        {
            if (completion == false)
            {
                description = $"Quest 2: Have 3 dollars in the bank - {player.moneyCount}/3";
            }
            else
            {
                description = "Quest 2: COMPLETED :D";
            }
        }

    }
}

using System;

namespace JourneyToTheMysticCave_Beta
{
    internal class Quest1 : Quest
    {
        public Quest1(Player player) :
        base(player)
        {
            this.player = player;
        }

        public int killAmount;


        //See main quest class
        public override void Init()
        {
            completion = false;
            killAmount = 10;
        }

        //See main quest class
        public override void Update()
        {
            if (player.killCount >= 10)
            {
                completion = true;
            }
            else
            {
                completion = false;
            }
        }

        //See main quest class
        public override void Draw()
        {
            if (completion == false)
            {
                description = $"Quest 1: Kill 10 Enemies - {player.killCount}/10";
            }
            else
            {
                description = "Quest 1: COMPLETED :D";
            }
        }



    }
}

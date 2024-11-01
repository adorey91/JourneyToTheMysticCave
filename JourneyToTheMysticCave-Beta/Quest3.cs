using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Quest3 : Quest
    {
        public Quest3(Player player) :
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
            if (player.bossIsDead == true)
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
                description = "Quest 3: Kill the Boss";
            }
            else
            {
                description = "Quest 3: COMPLETED :D";
            }
        }

    }
}

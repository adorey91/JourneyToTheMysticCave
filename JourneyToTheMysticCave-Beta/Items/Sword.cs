﻿using System;
using System.Linq;

namespace JourneyToTheMysticCave_Beta
{
    internal class Sword : Item
    {
        public int swordMultiplier;

        public Sword(int count, char character, string name, int swordMultiplier, LegendColors legendColors, Player player) : 
            base(count, character, name, legendColors, player)
        {
            this.swordMultiplier = swordMultiplier;
        }

        public override void Update()
        {
            if(player.pos.x == pos.x && player.pos.y == pos.y)
            {
                TryCollect();
                player.damage += swordMultiplier;
            }
        }
    }
}

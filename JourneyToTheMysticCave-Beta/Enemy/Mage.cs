﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    internal class Mage : Enemy
    {
        Random random = new Random();
        Player player;
        Gamelog log;
        LegendColors legendColors;

        public Mage(int count, char character, string name, int damage, int health, LegendColors legendColors, Player player, Gamelog log) : base(count, character, name, damage, player)
        {
            this.count = count;
            this.character = character;
            this.name = name;
            this.damage = damage;
            healthSystem = new HealthSystem();
            healthSystem.health = health;
            this.legendColors = legendColors;
            this.player = player;
            this.log = log;
        }

        public override void Update()
        {
            
        }

        public override void Draw()
        {
            Console.SetCursorPosition(pos.x, pos.y);
            legendColors.MapColor(character);
            Console.Write(character);
            Console.ResetColor();
            Console.CursorVisible = false;
        }
    }
}

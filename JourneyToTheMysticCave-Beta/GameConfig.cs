using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta
{
    public class GameConfig
    {
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
    }

    public class PlayerConfig
    {
        public string Name { get; set; }
        public char Character { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }
        public Point2D Pos { get; set; }
    }

    public class CharacterConfig
    {
        public int Count { get; set; }
        public char Character { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public int MinHp { get; set; }
        public int MaxHp { get; set; }
        public string Attack { get; set; }
    }

    public class BossConfig : CharacterConfig
    {
        public int Health { get; set; }
    }

    public class ItemConfig
    {
        public int Count { get; set; }
        public char Character { get; set; }
        public string Name { get; set; }
    }

    public class PotionConfig : ItemConfig
    {
        public int Heal { get; set; }
    }

    public class TrapConfig : ItemConfig
    {
        public int Damage { get; set; }
    }

    public class SwordConfig : ItemConfig
    {
        public int Multiplier { get; set; }
    }
}

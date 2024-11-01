using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace JourneyToTheMysticCave_Beta.Items
{
    internal class Shop
    {
        Player player;
        Map map;

        public bool isShopOpen = false;
        private int col;
        private int row;

        private int healthCost = 1;
        private int swordCost = 2;
        private int bootCost = 2;

        private string lastPurchase = "";

        public void Init(Player player, Map map)
        {
            this.player = player;
            this.map = map;
        }

        public void BuyItem(int itemNum)
        {
            col = map.GetMapColumnCount() + 2;
            row = map.GetMapRowCount() + 1;
            if (isShopOpen == false) return;

            if (itemNum == 1 && player.moneyCount >= healthCost)
            {
                player.healthSystem.Heal(10);
                player.moneyCount -= healthCost;
                lastPurchase = "Health potion(+10 health)";
            }
            else if (itemNum == 2 && player.moneyCount >= swordCost)
            {
                player.damage += 10;
                player.moneyCount -= swordCost;
                lastPurchase = "Sword upgrade(+10 damage)";
            }
            else if (itemNum == 3 && player.moneyCount >= bootCost)
            {
                if (player.poisonBoots == false)
                {
                    player.poisonBoots = true;
                    player.moneyCount -= bootCost;
                    lastPurchase = "Posion boots(poison immunity)";
                }
            }
        }

        public void PlayerEnters()
        {
            isShopOpen = true;
        }

        public void PlayerExits()
        {
            isShopOpen = false;
        }

        public void Update()
        {
            ShopInfo();
        }

        private void ShopInfo()
        {
            col = map.GetMapColumnCount() + 2;
            row = map.GetMapRowCount() + 1;

            if (isShopOpen == false)
            {
                Console.SetCursorPosition(col, row);
                Console.WriteLine("+--------------------------------------------------------+");
                Console.SetCursorPosition(col, row+1);
                Console.WriteLine("Shop closed");
                Console.SetCursorPosition(col, row+2);
                Console.WriteLine("+--------------------------------------------------------+");
            }
            else if (isShopOpen == true)
            {
                Console.SetCursorPosition(col, row);
                Console.WriteLine("+--------------------------------------------------------+");
                Console.SetCursorPosition(col, row+1);
                Console.WriteLine("Welcome to the shop!");
                Console.SetCursorPosition(col, row+2);
                Console.WriteLine("Click '1' to buy health potion(+10 Health): " + healthCost + "$");
                Console.SetCursorPosition(col, row+3);
                Console.WriteLine("Click '2' to upgrade sword(+10 Damage): " + swordCost + "$");
                Console.SetCursorPosition(col, row+4);
                Console.WriteLine("Click '3' to buy Poison Boots(No poison damage): " + bootCost + "$");
                Console.SetCursorPosition(col, row+5);
                Console.WriteLine("Last purchase: " + lastPurchase);
                Console.SetCursorPosition(col, row + 6);
                Console.WriteLine("+--------------------------------------------------------+");
            }
        }


    }
}

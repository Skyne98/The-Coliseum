using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class InventoryItem
    {
        //Inventory
        public InventorySystem Inventory;

        public InventoryItem(InventorySystem inventory)
        {
            Inventory = inventory;
        }

        public void SetInventory(InventorySystem inventory)
        {
            Inventory = inventory;
        }
    }
}

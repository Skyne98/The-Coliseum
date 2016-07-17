using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class InventorySystem
    {
        //TODO: Work on inventory
        //Owner
        public Character Character;

        //Equipment


        public InventorySystem(Character character)
        {
            Character = character;
        }

        //Inventory
        public List<InventoryItem> Items = new List<InventoryItem>();
    }
}

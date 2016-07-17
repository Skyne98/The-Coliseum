using Lidgren.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class Character
    {
        public string Name;
        public string PlayerName;
        public NetConnection Connection;

        public bool Ready = false; //For lobby

        public Character()
        {
            Inventory = new InventorySystem(this);
            ActionsSystem = new ActionsSystem(this);
            ActionsSystem.RegisterActions();
        }

        //Attributes
        public byte AttLuck = 1;

        //Inventory
        public InventorySystem Inventory;

        //Location
        public Location Location = null;
        public void SetLocation(Location location)
        {
            if (Location != null)
            {
                Location.Characters.Remove(this);
                location.Characters.Add(this);
            }

            Location = location;
            ServerMessageSender.SendString(ServerMessageSender.StringType.NewLocation, location.Name, this);
        }

        //Actions
        public ActionsSystem ActionsSystem;
    }
}

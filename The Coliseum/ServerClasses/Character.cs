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

        //Attributes
        public byte AttLuck = 1;

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
        public string GetActions()
        {
            return "";
        }
    }
}

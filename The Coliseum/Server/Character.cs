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
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class Game
    {
        public bool Started = false;

        //Characters
        public List<Character> Characters = new List<Character>();
        //Locations
        public List<Location> Locations = new List<Location>();
        public List<LocationLink> LocationLinks = new List<LocationLink>();

        public Game()
        {
            Server.MainServer.Game = this;
            Init();
        }

        public void Init()
        {
            //Create Locations
            InitLocations();
        }

        public void InitLocations()
        {
            Locations = new List<Location>();
            LocationLinks = new List<LocationLink>();

            Location.CreateLocation("Start");
            Location.CreateLocation("Forest");
            Location.CreateLocation("Swamp");

            Location.CreateLink("Start", "Forest");
            Location.CreateLink("Start", "Swamp");
        }

        public int GetReadyPlayers()
        {
            int ready = 0;

            foreach (Character character in Characters)
            {
                if (character.Ready)
                    ready++;
            }

            return ready;
        }
    }
}

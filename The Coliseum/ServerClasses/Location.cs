using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class Location
    {
        public string Name = "Location";

        //Connections
        public List<Location> Connections = new List<Location>();

        //Characters
        public List<Character> Characters = new List<Character>();

        public static void CreateLocation(string name)
        {
            Location location = new Location();
            location.Name = name;

            Server.MainServer.Game.Locations.Add(location);
        }
        public static void CreateLink(Location a, Location b)
        {
            LocationLink link = new LocationLink(a, b);

            Server.MainServer.Game.LocationLinks.Add(link);
        }
        public static void CreateLink(string a, string b)
        {
            Location al = Server.MainServer.Game.Locations.Find(t => t.Name == a);
            Location bl = Server.MainServer.Game.Locations.Find(t => t.Name == b);

            CreateLink(al, bl);
        }
    }
}

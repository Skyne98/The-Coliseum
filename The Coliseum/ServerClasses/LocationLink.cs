using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class LocationLink
    {
        public Location A;
        public Location B;

        public LocationLink(Location a, Location b)
        {
            A = a;
            B = b;

            A.Connections.Add(B);
            B.Connections.Add(A);
        }
    }
}

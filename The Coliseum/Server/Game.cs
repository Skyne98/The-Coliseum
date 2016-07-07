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

        public Game()
        {

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

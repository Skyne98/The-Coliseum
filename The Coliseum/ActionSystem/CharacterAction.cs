using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class CharacterAction
    {
        public Character Character;
        public string Code = "none";
        public int Priority = 0;

        public CharacterAction(Character character)
        {
            Character = character;
        }

        public virtual void AddActions()
        {

        }

        public virtual void ProcessCode(string code)
        {
            
        }
    }
}

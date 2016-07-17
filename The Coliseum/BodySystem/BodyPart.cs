using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class BodyPart
    {
        public string Name = "";
        public bool External = true;
        public float Health = 100;
        public float DamageCoeficient = 1;

        //BodySystem
        public BodySystem BodySystem;

        //Included Parts
        public List<BodyPart> Parts = new List<BodyPart>();

        public BodyPart(BodySystem bodySystem)
        {
            BodySystem = bodySystem;

            BodySystem.Parts.Add(this);
        }

        //Recursion
        public BodyPart GetBodyPart(string name)
        {
            if (Name == name)
                return this;
            else
            {
                BodyPart part = null;

                foreach (var bPart in Parts)
                {
                    BodyPart temp = bPart.GetBodyPart(name);

                    if (temp != null)
                        part = temp;
                }

                return part;
            }
        }
    }
}

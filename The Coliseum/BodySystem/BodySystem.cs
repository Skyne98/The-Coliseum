using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class BodySystem
    {
        //Character
        public Character Character;

        //Body
        public BodyPart Body;
        public List<BodyPart> Parts = new List<BodyPart>();

        public BodySystem(Character character)
        {
            Character = character;

            //Init Body
            Body = new BodyPart(this);
            Body.Name = "Torso";
            Body.External = true;
            Body.DamageCoeficient = 0.2f;

            //Limbs
            BodyPart Head = new BodyPart(this);
            Head.Name = "Head";
            Head.External = true;
            Head.DamageCoeficient = 0.7f;

            BodyPart LArm = new BodyPart(this);
            LArm.Name = "Left Arm";
            LArm.External = true;
            LArm.DamageCoeficient = 0.5f;

            BodyPart RArm = new BodyPart(this);
            RArm.Name = "Right Arm";
            RArm.External = true;
            RArm.DamageCoeficient = 0.5f;
            //TODO: Add legs
        }

        public BodyPart GetBodyPart(string name)
        {
            return Body.GetBodyPart(name);
        }
    }
}

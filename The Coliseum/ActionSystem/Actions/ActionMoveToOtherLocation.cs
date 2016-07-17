using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class ActionMoveToOtherLocation : CharacterAction
    {
        public ActionMoveToOtherLocation(Character character) : base(character)
        {
            //Change action code
            Code = "moveto";
            //Set action's priority (0 - fastest)
            Priority = 0;
        }

        public override void AddActions()
        {
            if (Character.Location.HasLinks())
            {
                foreach (var location in Character.Location.Connections)
                {
                    Character.ActionsSystem.AddAction($@"Map\Move to {location.Name}", "Change location", $@"{Code}\{location.Name}");
                }
            }
        }

        public override void ProcessCode(string code)
        {
            Func<int, string> split = (i) => { return code.Split(@"\".ToCharArray())[i]; };

            if (split(0) == Code)
            {
                string locationName = split(1);

                Location location = Character.Location.Connections.FirstOrDefault(a => a.Name == locationName);

                if (location != null)
                {
                    Character.SetLocation(location);
                    ServerMessageSender.SendInfo(Server.LogType.Common, "Moved to " + location.Name, Character);
                    Server.Log(Character.PlayerName + " moved to " + location.Name, Server.LogType.Common);
                }
                else
                {
                    ServerMessageSender.SendInfo(Server.LogType.Common, "Failed to move to " + locationName, Character);
                    Server.Log(Character.PlayerName + " failed to move to " + locationName, Server.LogType.Common);
                }
            }
        }
    }
}

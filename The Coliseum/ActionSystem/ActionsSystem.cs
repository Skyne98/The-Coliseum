using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class ActionsSystem
    {
        public ActionsSystem(Character character)
        {
            Character = character;
        }

        //Character
        Character Character;

        //Actions
        public string CurrentActionCode = "";
        public int CurrentActionPriority { get { return RegisteredActions.FirstOrDefault(a => a.Code == CurrentActionCode.Split(@"\".ToCharArray())[0]).Priority; } }

        public List<CharacterAction> RegisteredActions;
        public List<string> TempActions;

        public void GetActions()
        {
            TempActions = new List<string>();

            foreach (var action in RegisteredActions)
            {
                action.AddActions();
            }
        }
        public void AddAction(string path, string toolTip, string code)
        {
            TempActions.Add($"{path}|{toolTip}|{code}");
        }
        public void RegisterActions()
        {
            RegisteredActions = new List<CharacterAction>();

            //Add Actions to be Processed
            //RegisteredActions.Add(new *Action*(this));
            RegisteredActions.Add(new ActionMoveToOtherLocation(Character));
        }
        public void ProcessCode(string code)
        {
            foreach (var action in RegisteredActions)
            {
                action.ProcessCode(code);
            }
        }
        public void SendActions()
        {
            foreach (var action in TempActions)
            {
                ServerMessageSender.SendString(ServerMessageSender.StringType.NewAction, action, Character);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace The_Coliseum
{
    public class Action
    {
        public string Name = "";

        public List<PassiveCondition> PassiveConditions = new List<PassiveCondition>();
        public List<ActiveCondition> ActiveConditions = new List<ActiveCondition>();
        public List<Result> Results = new List<Result>();
    }
}

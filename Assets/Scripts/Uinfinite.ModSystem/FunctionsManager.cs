using System.Collections.Generic;

namespace Uinfinite.ModSystem
{
    public class FunctionsManager  {

        public static Dictionary<string, Functions> actions;

        public FunctionsManager(){
            actions = new Dictionary<string, Functions>();

        }

        public static Functions Get(string name){
            if(actions == null){
                return null;
            }

            if(actions.ContainsKey(name)){
                return actions[name];
            }

            return null;
        }
    }
}



using System;
using System.Collections.Generic;

namespace Uinfinite.ModSystem{
    
    public class Functions {

        public enum Type{
            Lua,
            CSharp
        }

        public List<IFunctions> FunctionsSets { get; private set; }

        public Functions()
        {
            FunctionsSets = new List<IFunctions>();
        }

        public bool LoadScript(string text, string scriptName, Type type){

            bool result = false;

            if(type == Type.Lua){
                
                LuaFunctions luaFunctions = new LuaFunctions();

                if(luaFunctions.LoadScript(text, scriptName)){

                    FunctionsSets.Add(luaFunctions);
                }
            }
            else
            {
                
            }

            return result;
        }
    }

}


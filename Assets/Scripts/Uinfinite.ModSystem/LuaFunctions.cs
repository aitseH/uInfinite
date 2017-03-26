using System;
using MoonSharp.Interpreter;


namespace Uinfinite.ModSystem {
    
    public class LuaFunctions : IFunctions {

        protected Script script;

        private string scriptName;

        public LuaFunctions(){
            
            UserData.RegisterAssembly();

            this.script = new Script();

            UserData.RegisterType<UnityEngine.Vector3>();
            UserData.RegisterType<UnityEngine.Vector2>();
            UserData.RegisterType<UnityEngine.Vector4>();
            UserData.RegisterType<UnityEngine.UI.Text>();

        }

        public bool HasFunction(string name){
            return name != null && script.Globals[name] != null;
        }

        public bool LoadScript(string text, string scriptName){
            
            this.scriptName = scriptName;

            try
            {
                script.DoString(text);
            }
            catch (SyntaxErrorException ex)
            {
                return false;
            }

            return true;
        }

        public DynValue CallWithError(string functionName, params object[] args)
        {
            return Call(functionName, true, args);
        }

        public DynValue Call(string functionName, params object[] args)
        {
            return Call(functionName, false, args);
        }

        public T Call<T>(string functionName, params object[] args)
        {
            return Call(functionName, args).ToObject<T>();
        }

        public void RegisterType(Type type)
        {
            RegisterGlobal(type);
        }

        private void RegisterGlobal(Type type)
        {
            script.Globals[type.Name] = type;
        }
    }
}



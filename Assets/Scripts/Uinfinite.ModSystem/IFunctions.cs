using MoonSharp.Interpreter;

public interface IFunctions {

    void RegisterType(System.Type type);

    bool HasFunction(string name);

    bool LoadScript(string text, string scriptName);

    DynValue Call(string functionName, params object[] args);

    T Call<T>(string functionName, params object[] args);

    DynValue CallWithError(string functionName, params object[] args);
}

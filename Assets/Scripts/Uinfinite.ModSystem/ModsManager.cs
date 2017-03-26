using System;
using System.IO;
using UnityEngine;

namespace Uinfinite.ModSystem
{
    public class ModsManager{

        private DirectoryInfo[] mods;

        public ModsManager(){

            mods = GetModsFiles();

            LoadShareFiles();

            LoadMainSceneFiles();
        }

        public static DirectoryInfo[] GetModsFiles(){
            DirectoryInfo modsDir = new DirectoryInfo(GetPathToModsFolder());
            return modsDir.GetDirectories();
        }

        public static string GetPathToModsFolder(){
            return System.IO.Path.Combine(System.IO.Path.Combine(Application.streamingAssetsPath, "Data"), "Mods");
        }


        public void LoadShareFiles(){

        }

        public void LoadMainSceneFiles(){
            
        }


        public void LoadFunctionsInFile(FileInfo file, string functionsName)
        {
            LoadTextFile(
                file.DirectoryName,
                file.Name,
                (filePath) =>
                {
                    StreamReader reader = new StreamReader(file.OpenRead());
                    string text = reader.ReadToEnd();
                    FunctionsManager.Get(functionsName).LoadScript(text, functionsName, file.Extension == ".lua" ? Functions.Type.Lua : Functions.Type.CSharp);
                });
        }

        private void LoadFunctions(string fileName, string functionsName)
        {
            string ext = Path.GetExtension(fileName);
            string folder = "LUA";
            Functions.Type scriptType = Functions.Type.Lua;

            if(string.Compare(".cs", ext, true) == 0 ){
                folder = "CSharp";
                scriptType = Functions.Type.CSharp;
            }

            LoadTextFile(folder, fileName, (filePath) =>
                {
                    if(File.Exists(filePath)){
                        string text = File.ReadAllText(filePath);
                        FunctionsManager.Get(functionsName).LoadScript(text, functionsName, scriptType);
                    }
                });
        }

        private void LoadTextFile(string directoryName, string fileName, Action<string> readText){
            
            string filePath = Path.Combine(Application.streamingAssetsPath, directoryName);
            filePath = Path.Combine(filePath, fileName);
            if(File.Exists(filePath)){
                readText(filePath);
            }

            foreach(DirectoryInfo mod in mods){
                filePath = Path.Combine(mod.FullName, fileName);
                if(File.Exists(filePath)){
                    readText(filePath);
                }
            }
        }

        private void LoadPrototypes(string fileName, Action<string> prototypesLoader)
        {
            LoadTextFile(
                "Data",
                fileName,
                (filePath) =>
                {
                    string text = File.ReadAllText(filePath);
                    prototypesLoader(text);
                });
        }

        private void LoadDirectoryAssets(string directoryName, Action<string> readDirectory)
        {
            string directoryPath = Path.Combine(Application.streamingAssetsPath, directoryName);
            if (Directory.Exists(directoryPath))
            {
                readDirectory(directoryPath);
            }

            foreach (DirectoryInfo mod in mods)
            {
                directoryPath = Path.Combine(mod.FullName, directoryName);
                if (Directory.Exists(directoryPath))
                {
                    readDirectory(directoryPath);
                }
            }
        }
    }
}



using System.Collections.Generic;
using UnityEngine.AddressableAssets;

namespace GameFolder.GlobalManagers
{
    public enum SceneType 
    {
        Entry,
        MainMenu
    }
    
    public static class SceneController
    {
        private static Dictionary<SceneType, string> scenesPath = new ()
        {
            {SceneType.MainMenu, "MainMenu"}
        };
        
        public static SceneType currentScene { get; private set; }

        public static void LoadScene(SceneType scene)
        {
            currentScene = scene;
            Addressables.LoadSceneAsync(scenesPath[scene]);
        }
    }
}
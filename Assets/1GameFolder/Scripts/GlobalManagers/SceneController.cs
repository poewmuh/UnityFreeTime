using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace GameFolder.GlobalManagers
{
    public enum SceneType 
    {
        Entry,
        MainMenu,
        Persistence,
        HubLocation,
        BattleLocation
    }
    
    public static class SceneController
    {
        public static event Action<SceneType> OnSceneChanged;
        
        private static readonly Dictionary<SceneType, string> scenesPath = new ()
        {
            { SceneType.MainMenu, "MainMenu" },
            { SceneType.Persistence, "Persistence" },
            { SceneType.HubLocation , "HubLocation" },
            { SceneType.BattleLocation , "BattleLocation" }
        };
        
        public static SceneType currentScene { get; private set; }

        public static void LoadScene(SceneType scene)
        {
            currentScene = scene;
            Addressables.LoadSceneAsync(scenesPath[scene]);
            OnSceneChanged?.Invoke(currentScene);
        }
        
        public static void LoadNetworkScene(SceneType scene)
        {
            currentScene = scene;
            NetworkManager.Singleton.SceneManager.LoadScene(scenesPath[scene], LoadSceneMode.Single);
            OnSceneChanged?.Invoke(currentScene);
        }
    }
}
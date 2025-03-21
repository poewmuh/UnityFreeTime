using GameFolder.Tools.DataControl;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolder.UI.Windows
{
    public class WindowsManager
    {
        public static event Action<WindowBase> OnWindowOpen;
        public static event Action<WindowBase> OnWindowStartClose;
        
        private static List<Type> _openedWindows = new();
        private static Dictionary<WindowBase, AssetLoaderHandler> _windowAssets = new();

        public static T CreateWindow<T>(string path, Transform parent = null) where T : WindowBase
        {
            if (_openedWindows.Contains(typeof(T)))
            {
                Debug.Log("[WindowsManager] Window already open");
                return null;
            }
            CloseAllWindows();
            var loadHandler = new AssetLoaderHandler();
            var windowObject = loadHandler.LoadGOImmediate<T>(path);
            var window = GameObject.Instantiate(windowObject, parent);
            window.onWindowClose += OnWindowClose;
            window.onStartClose += OnStartClose;
            _windowAssets.Add(window, loadHandler);
            _openedWindows.Add(typeof(T));
            window.OnCreated();
            OnWindowOpen?.Invoke(window);
            return window;
        }

        private static void CloseAllWindows()
        {
            foreach (var window in _windowAssets.Keys)
            {
                window.Close();
            }
        }

        private static void OnWindowClose(WindowBase window)
        {
            window.onWindowClose -= OnWindowClose;
            _windowAssets[window].Unload();
            _windowAssets.Remove(window);
            _openedWindows.Remove(window.GetType());
        }

        private static void OnStartClose(WindowBase window)
        {
            window.onStartClose -= OnStartClose;
            OnWindowStartClose?.Invoke(window);
        }
    }
}
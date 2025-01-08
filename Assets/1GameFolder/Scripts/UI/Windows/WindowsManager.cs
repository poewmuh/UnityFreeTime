using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolder.UI.Windows
{
    public class WindowsManager
    {
        public static event Action<WindowBase> OnWindowOpen;
        private static List<Type> _openedWindows = new();
        private static Dictionary<WindowBase, AssetLoaderHandler> _windowAssets = new();

        public static T CreateWindow<T>(string path, Transform parent = null) where T : WindowBase
        {
            if (_openedWindows.Contains(typeof(T)))
            {
                Debug.Log("[WindowsManager] Window already open");
                return null;
            }
            var loadHandler = new AssetLoaderHandler();
            var windowObject = loadHandler.LoadImmediate<T>(path);
            var window = GameObject.Instantiate(windowObject, parent);
            window.onWindowClose += OnWindowClose;
            _windowAssets.Add(window, loadHandler);
            _openedWindows.Add(typeof(T));
            return window;
        }

        private static void OnWindowClose(WindowBase window)
        {
            window.onWindowClose -= OnWindowClose;
            _windowAssets[window].Unload();
            _windowAssets.Remove(window);
            _openedWindows.Remove(window.GetType());
        }
    }
}
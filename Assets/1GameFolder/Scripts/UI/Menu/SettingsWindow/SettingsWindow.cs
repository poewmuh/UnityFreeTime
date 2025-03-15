using GameFolder.Tools.Serialization;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameFolder.UI.Menu.Settings
{
    public class SettingsWindow : WindowBase
    {
        public const string assetPath = "UI/Menu/SettingsWindow";

        [SerializeField] private DictionaryIntGameObject _tabs;

        public void OnChangeTabPressed(int tabId)
        {
            CloseAllTabs();
            _tabs[tabId].SetActive(true);
        }

        private void CloseAllTabs()
        {
            foreach (var tab in _tabs.Values)
            {
                tab.SetActive(false);
            }
        }
    }
}
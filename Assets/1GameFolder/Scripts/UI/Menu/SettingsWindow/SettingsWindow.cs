using GameFolder.Tools.Serialization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering;

namespace GameFolder.UI.Menu.Settings
{
    public class SettingsWindow : WindowBase
    {
        public const string assetPath = "UI/Menu/SettingsWindow";

        [SerializeField] private DictionaryIntGameObject _tabs;
        [SerializeField] private List<TextMeshProUGUI> _tabsText;

        private int _focusableTabId = -1;

        public override void OnCreated()
        {
            base.OnCreated();
            SetFocusableTab(0);
        }

        public void SetFocusableTab(int tabId)
        {
            if (_focusableTabId >= 0)
            {
                _tabs[_focusableTabId].SetActive(false);
                _tabsText[_focusableTabId].color = ColorsHelper.settingsTabTextColor;
            }

            _focusableTabId = tabId;
            _tabsText[_focusableTabId].color = ColorsHelper.textHighlightedColor;
            _tabs[_focusableTabId].SetActive(true);
        }
    }
}
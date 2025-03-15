using GameFolder.Data;
using GameFolder.Localization;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolder.UI.Menu.Settings
{
    public class GameplayBoxSettings : MonoBehaviour
    {
        [SerializeField] private TMP_Dropdown _languageDropDown;
        private ApplicationSettingsData _appSettings;

        private void Start()
        {
            _appSettings = ProfileData.GetLocalData<ApplicationSettingsData>();
            _languageDropDown.value = (int)_appSettings.LanguageType;
        }

        public void OnChangeLanguage(int languageId)
        {
            var newLanguageType = (LanguageType)languageId;
            if (_appSettings.LanguageType == newLanguageType) return;
            _appSettings.LanguageType = newLanguageType;
            _appSettings.Save();
            LocalizationLoader.LoadLocalization((LanguageType)languageId);
        }
    }
}
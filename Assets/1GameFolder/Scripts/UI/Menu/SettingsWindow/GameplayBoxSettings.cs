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

        public void OnChangeLanguage(bool isArrowRight)
        {
            int currentLanguageInt = (int)_appSettings.LanguageType;
            if (isArrowRight)
            {
                currentLanguageInt++;
                if (currentLanguageInt >= (int)LanguageType.EmptyMax)
                {
                    currentLanguageInt = 0;
                }
            }
            else
            {
                currentLanguageInt--;
                if (currentLanguageInt < 0)
                {
                    currentLanguageInt = (int)LanguageType.EmptyMax - 1;
                }
            }

            _languageDropDown.value = currentLanguageInt;
            _appSettings.LanguageType = (LanguageType)currentLanguageInt;
            _appSettings.Save();
            LocalizationLoader.LoadLocalization((LanguageType)currentLanguageInt);
        }
    }
}
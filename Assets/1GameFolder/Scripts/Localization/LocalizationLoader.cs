using GameFolder.Tools.DataControl;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace GameFolder.Localization
{
    public enum LanguageType
    {
        Rus = 0,
        Eng = 1
    }

    public class LocalizationLoader
    {
        public const string engPath = "localization_en";
        public const string rusPath = "localization_ru";

        private static AssetLoaderHandler _handler;

        public static void LoadLocalization(LanguageType languageType)
        {
            if (_handler != null)
            {
                UnloadLocalization();
            }

            var path = "";
            switch (languageType)
            {
                case LanguageType.Eng:
                    path = engPath;
                    break;
                case LanguageType.Rus:
                    path = rusPath;
                    break;
            }

            _handler = new AssetLoaderHandler();
            var localizationData =_handler.LoadImmediate<LocalizationTextData>(path);
            LocalizationManager.SetLocalizationData(localizationData);
        }

        public static void UnloadLocalization()
        {
            _handler.Unload();
        }

        public static LanguageType FromSystemLanguage(SystemLanguage language) => language switch
        {
            SystemLanguage.English => LanguageType.Eng,
            SystemLanguage.German => LanguageType.Eng,
            SystemLanguage.French => LanguageType.Eng,
            SystemLanguage.Italian => LanguageType.Eng,
            SystemLanguage.Spanish => LanguageType.Eng,
            SystemLanguage.Portuguese => LanguageType.Eng,
            SystemLanguage.Chinese => LanguageType.Eng,
            SystemLanguage.ChineseSimplified => LanguageType.Eng,
            SystemLanguage.ChineseTraditional => LanguageType.Eng,
            SystemLanguage.Russian => LanguageType.Rus,
            SystemLanguage.Indonesian => LanguageType.Eng,
            SystemLanguage.Korean => LanguageType.Eng,
            SystemLanguage.Japanese => LanguageType.Eng,
            SystemLanguage.Thai => LanguageType.Eng,
            _ => LanguageType.Eng
        };
    }
}
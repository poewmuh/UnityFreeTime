using GameFolder.Localization;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolder.Data
{
    public class ApplicationSettingsData : LocalDataBase
    {
        public const string NAME = "local_data_app_settings";

        [JsonProperty] private float masterMusicVolume = 1f;
        [JsonProperty] private LanguageType languageType;

        [JsonIgnore]
        public float MasterMusicVolume
        {
            get => masterMusicVolume;
            set => masterMusicVolume = value;
        }

        [JsonIgnore]
        public LanguageType LanguageType
        {
            get => languageType;
            set => languageType = value;
        }

        public override void OnGenerate()
        {
            base.OnGenerate();
            masterMusicVolume = 1;
            languageType = LocalizationLoader.FromSystemLanguage(Application.systemLanguage);
        }
    }
}
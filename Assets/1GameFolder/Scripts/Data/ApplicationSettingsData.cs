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

        [JsonProperty] private float masterVolume = 1f;
        [JsonProperty] private LanguageType languageType;

        [JsonIgnore]
        public float MasterVolume
        {
            get => masterVolume;
            set => masterVolume = value;
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
            masterVolume = 0.7f;
            languageType = LocalizationLoader.FromSystemLanguage(Application.systemLanguage);
        }
    }
}
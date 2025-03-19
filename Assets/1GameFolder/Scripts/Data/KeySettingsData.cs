using GameFolder.Data;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFolder.Data
{
    public class KeySettingsData : LocalDataBase
    {
        public const string NAME = "local_data_key_settings";

        [JsonProperty] private KeyCode moveUp;

    }
}
using GameFolder.Tools.Serialization;
using UnityEngine;

namespace GameFolder.Localization
{
    [CreateAssetMenu(fileName = "LocalizationTextData", menuName = "Data/Localization/TextData")]
    public class LocalizationTextData : ScriptableObject
    {
        [SerializeField] private DictionaryStringString _localizationDic;

        public string GetLocalizatedText(string key)
        {
            if (!_localizationDic.ContainsKey(key))
            {
                Debug.LogError($"Key '{key}' didn't exist in dictionary!");
                return "Error Check Localization!";
            }

            return _localizationDic[key];
        }
    }
}
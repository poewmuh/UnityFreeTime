using System.Collections.Generic;
using UnityEngine;

namespace GameFolder.Localization
{
    public class LocalizationManager
    {
        private static LocalizationTextData _data;
        private static List<ILocalizationUpdatable> _updatableTexts = new ();

        public static void SetLocalizationData(LocalizationTextData data)
        {
            _data = data;
            UpdateUpdatablesText();
        }

        public static void AddUpdatableText(ILocalizationUpdatable updatable)
        {
            _updatableTexts.Add(updatable);
        }

        public static void RemoveUpdatableText(ILocalizationUpdatable updatable)
        {
            _updatableTexts.Remove(updatable);
        }

        public static string GetText(string key)
        {
            if (!_data)
            {
                Debug.LogError("[LocalizationManager] Data not exist!");
                return "Error in localization!";
            }
            return _data.GetLocalizatedText(key);
        }

        private static void UpdateUpdatablesText()
        {
            foreach (var text in _updatableTexts)
            {
                text.UpdateText();
            }
        }
    }
}
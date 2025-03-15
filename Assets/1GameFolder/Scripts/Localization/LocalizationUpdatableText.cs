using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace GameFolder.Localization
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizationUpdatableText : MonoBehaviour, ILocalizationUpdatable
    {
        [SerializeField] private string _key;

        private TextMeshProUGUI _text;

        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
            UpdateText();
            LocalizationManager.AddUpdatableText(this);
        }

        public void UpdateText()
        {
            _text.text = LocalizationManager.GetText(_key);
        }

        private void OnDestroy()
        {
            LocalizationManager.RemoveUpdatableText(this);
        }
    }
}
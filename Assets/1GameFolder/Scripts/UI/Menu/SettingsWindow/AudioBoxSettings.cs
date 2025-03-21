using GameFolder.Audio;
using GameFolder.Data;
using GameFolder.Localization;
using GameFolder.Tools;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolder.UI.Menu.Settings
{
    public class AudioBoxSettings : MonoBehaviour
    {
        [SerializeField] private Slider _masterVolumeSlider;
        [SerializeField] private TextMeshProUGUI _masterVolumeText;

        private ApplicationSettingsData _appSettings;
        private AudioController _audioController;

        private void Start()
        {
            _audioController = ComponentLocator.Resolve<AudioController>();
            _appSettings = ProfileData.GetLocalData<ApplicationSettingsData>();
            _masterVolumeSlider.value = _appSettings.MasterVolume;
            _masterVolumeText.text = $"{GetVolumePercent(_appSettings.MasterVolume)}%";
        }

        public void OnChangeMasterVolume(float newValue)
        {
            if (Mathf.Approximately(newValue, _appSettings.MasterVolume)) return;
            _masterVolumeText.text = $"{GetVolumePercent(newValue)}%";
            _appSettings.MasterVolume = newValue;
            _audioController.OnMasterVolumeChange(newValue);
            _appSettings.Save();
        }

        private int GetVolumePercent(float sliderValue)
        {
            float percent = sliderValue * 100f;
            if (percent is < 1f and > 0f)
            {
                return 1;
            }
            
            return (int)percent;
        }
    }
}
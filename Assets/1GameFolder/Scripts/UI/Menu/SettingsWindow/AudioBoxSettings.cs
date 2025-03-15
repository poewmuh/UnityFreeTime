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

        private ApplicationSettingsData _appSettings;
        private AudioController _audioController;

        private void Start()
        {
            _audioController = ComponentLocator.Resolve<AudioController>();
            _appSettings = ProfileData.GetLocalData<ApplicationSettingsData>();
            _masterVolumeSlider.value = _appSettings.MasterVolume;
        }

        public void OnChangeMasterVolume(float newValue)
        {
            if (Mathf.Approximately(newValue, _appSettings.MasterVolume)) return;
            _appSettings.MasterVolume = newValue;
            _audioController.OnMasterVolumeChange(newValue);
            _appSettings.Save();
        }
    }
}
using System;
using GameFolder.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GameFolder.Gameplay.HubLocation.HeroChangeWindow
{
    public class HeroChangeCard : MonoBehaviour
    {
        public event Action<HeroData> OnCardClicked;
        
        [SerializeField] private TextMeshProUGUI _heroname;
        [SerializeField] private Image _heroSprite;
        [SerializeField] private TextMeshProUGUI _heroDesc;
        [SerializeField] private Button _selectButton;

        private HeroData _data;
        
        public void Init(HeroData heroData)
        {
            _data = heroData;
            _heroname.text = heroData.GetHeroName();
            _heroSprite.sprite = heroData.GetHeroSprite();
            _heroDesc.text = heroData.GetHeroDesc();
            
            _selectButton.onClick.AddListener(OnClicked);
        }
        
        private void OnDestroy()
        {
            Dispose();
        }

        private void OnClicked()
        {
            OnCardClicked?.Invoke(_data);
        }

        private void Dispose()
        {
            _selectButton.onClick.RemoveListener(OnClicked);
        }
    }
}
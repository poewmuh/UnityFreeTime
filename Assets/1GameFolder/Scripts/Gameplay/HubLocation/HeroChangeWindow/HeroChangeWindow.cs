using System;
using System.Collections.Generic;
using GameFolder.Data;
using GameFolder.Gameplay.HeroesSystem;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.HubLocation.HeroChangeWindow
{
    public class HeroChangeWindow : MonoBehaviour
    {
        [SerializeField] private AllHeroesData _allHeroesData;
        [SerializeField] private Transform _contentHolder;
        [SerializeField] private HeroChangeCard _heroChangeCardPrefab;

        private readonly List<HeroChangeCard> _currentActiveCards = new();

        private void Awake()
        {
            FillHeroes();
        }

        private void FillHeroes()
        {
            var allHeroesData = _allHeroesData.GetAllHeroesData();
            foreach (var heroData in allHeroesData)
            {
                var heroCard = Instantiate(_heroChangeCardPrefab, _contentHolder);
                heroCard.Init(heroData);
                heroCard.OnCardClicked += OnClickedCard;
                _currentActiveCards.Add(heroCard);
            }
        }

        private void OnClickedCard(HeroData selectHeroData)
        {
            HeroesController.Instance.RequestChangeHero(NetworkManager.Singleton.LocalClientId, selectHeroData.GetHeroId());
            HubLocationHUD.Instance.CloseHeroChangeWindow();
        }

        private void OnDestroy()
        {
            foreach (var heroCard in _currentActiveCards)
            {
                heroCard.OnCardClicked -= OnClickedCard;
            }
        }
    }
}
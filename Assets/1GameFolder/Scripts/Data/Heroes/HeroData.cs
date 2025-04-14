using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Data
{
    [CreateAssetMenu(fileName = "HeroData", menuName = "Data/HeroesData/HeroData")]
    public class HeroData : ScriptableObject
    {
        [SerializeField] private int _heroId;
        [SerializeField] private string _heroName;
        [SerializeField] private string _heroDescription;
        [SerializeField] private Sprite _heroSprite;
        [SerializeField] private NetworkObject _gameplayPrefab;

        public int GetHeroId() => _heroId;
        public string GetHeroName() => _heroName;
        public string GetHeroDesc() => _heroDescription;
        public Sprite GetHeroSprite() => _heroSprite;
        public NetworkObject GetGameplayPrefab() => _gameplayPrefab;
    }
}
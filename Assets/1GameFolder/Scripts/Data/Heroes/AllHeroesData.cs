using System.Collections.Generic;
using System.IO;
using TriInspector;
using Unity.Netcode;
using UnityEditor;
using UnityEngine;

namespace GameFolder.Data
{
    [CreateAssetMenu(fileName = "AllHeroesData", menuName = "Data/HeroesData/AllHeroesData")]
    public class AllHeroesData : ScriptableObject
    {
        [SerializeField] private List<HeroData> _heroesData;

        public bool TryGetHeroById(int id, out HeroData data)
        {
            data = _heroesData.Find(x => x.GetHeroId() == id);
            return data != null;
        }

        public List<HeroData> GetAllHeroesData()
        {
            return _heroesData;
        }

        public HeroData GetHeroDataById(int heroId)
        {
            var heroData = _heroesData.Find(x => x.GetHeroId() == heroId);
            return heroData;
        }

        public NetworkObject GetGameplayPrefabById(int heroId)
        {
            var heroData = GetHeroDataById(heroId);
            return heroData.GetGameplayPrefab();
        }

#if UNITY_EDITOR
        [Button]
        public void Fill()
        {
            var path = AssetDatabase.GetAssetPath(this);
            path = Path.GetDirectoryName(path);
            var guids = AssetDatabase.FindAssets("t:HeroData", new[] { path });
            _heroesData.Clear();
            foreach (var guid in guids)
            {
                var assetPath = AssetDatabase.GUIDToAssetPath(guid);
                var hero = AssetDatabase.LoadAssetAtPath<HeroData>(assetPath);
                if (hero != null)
                    _heroesData.Add(hero);
            }
            EditorUtility.SetDirty(this);
        }
#endif
    }
}
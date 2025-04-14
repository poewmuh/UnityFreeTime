using GameFolder.Data;
using GameFolder.Gameplay.BattleLocation.Map;
using GameFolder.Gameplay.BattleLocation.UnitComponent;
using GameFolder.Gameplay.HeroesSystem;
using GameFolder.Gameplay.Networking;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace GameFolder.Gameplay.BattleLocation
{
    public class BattleStartState : IBattleState
    {
        private readonly BattleStateMachine _stateMachine;
        private AllHeroesData _allHeroesData;
        public BattleStartState(BattleStateMachine battleStateMachine)
        {
            _stateMachine = battleStateMachine;
        }

        public void Enter()
        {
            Addressables.LoadAssetAsync<AllHeroesData>("AllHeroesData").Completed += handle => {
                if (handle.Status == AsyncOperationStatus.Succeeded) {
                    _allHeroesData = handle.Result;
                    SpawnAllUnits();
                } 
                else 
                {
                    Debug.LogError("ALL HEROES DATA NOT LOADED!");
                }
            };
        }
        
        private void SpawnAllUnits() 
        {
            foreach (var heroesDataKVP in HeroesController.Instance.GetAllClientsHeroesData()) 
            {
                var gameplayPrefab = _allHeroesData.GetGameplayPrefabById(heroesDataKVP.Value.heroId);
                if (gameplayPrefab == null) 
                {
                    Debug.LogError($"[BattleStart] Can't Find prefab for id: '{heroesDataKVP.Value.heroId}'");
                    continue;
                }

                var spawnCell = MapManager.Instance.GetStartCellForClient(heroesDataKVP.Key);

                var spawnedUnit = NetworkManager.Singleton.SpawnManager.InstantiateAndSpawn(gameplayPrefab, heroesDataKVP.Key, true);
                spawnedUnit.GetComponent<BattleUnit>().InitializeServer(spawnCell.GridPosition);
            }

            _stateMachine.SetState(BattleState.PlayerTurn);
        }

        public void Exit() { }

        public void Tick() { }
    }
}
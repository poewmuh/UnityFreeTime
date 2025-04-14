using System.Collections.Generic;
using GameFolder.Gameplay.Networking;
using GameFolder.Gameplay.UnitComponents;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.HubLocation
{
    public class HubUnitsManager : GameplaySystem<HubUnitsManager>
    {
        [SerializeField] private NetworkObject _playerPrefab;
        
        public static Unit mineUnit;

        private readonly Dictionary<ulong, Unit> _allUnits = new();
        private readonly List<ulong> _allUnitsNetIds = new();

        protected override void Initialize()
        {
            var session = SessionController.Instance;
            foreach (var user in session.activeSessionUsers)
            {
                SpawnPlayer(user.clientId);
            }

            SessionController.Instance.OnUserConnected += OnUserConnected;
        }

        public override void OnDestroy()
        {
            SessionController.Instance.OnUserConnected -= OnUserConnected;
            base.OnDestroy();
        }

        private void OnUserConnected(SessionUser user)
        {
            FillOthersUnitsRpc(_allUnitsNetIds.ToArray(), RpcTarget.Single(user.clientId, RpcTargetUse.Temp));
            SpawnPlayer(user.clientId);
        }

        private void SpawnPlayer(ulong clientId)
        {
            var player = NetworkManager.SpawnManager.InstantiateAndSpawn(_playerPrefab, clientId, true, true, false, Vector3.up * 1000f);
            _allUnitsNetIds.Add(player.NetworkObjectId);
            SpawnPlayerRpc(player.NetworkObjectId);
        }

        [Rpc(SendTo.Everyone)]
        private void SpawnPlayerRpc(ulong netId)
        {
            InitializeUnit(netId);
        }

        private void InitializeUnit(ulong netId)
        {
            var unit = NetworkManager.SpawnManager.SpawnedObjects[netId].GetComponent<Unit>();
            unit.gameObject.name = "Player_" + unit.NetworkObjectId;
            _allUnits.Add(unit.OwnerClientId, unit);

            if (unit.IsOwner)
            {
                mineUnit = unit;
            }
        }

        [Rpc(SendTo.SpecifiedInParams)]
        private void FillOthersUnitsRpc(ulong[] spawnedNetId, RpcParams rpcParams = default)
        {
            foreach (var netId in spawnedNetId)
            {
                InitializeUnit(netId);
            }
        }

        public Unit GetUnitById(ulong clientId)
        {
            return _allUnits[clientId];
        }

        public int GetAllUnitsCount()
        {
            return _allUnits.Count;
        }
    }
}
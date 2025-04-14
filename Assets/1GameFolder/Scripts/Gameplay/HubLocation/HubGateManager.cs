using System.Collections.Generic;
using GameFolder.Gameplay.Networking;
using GameFolder.Gameplay.UnitComponents;
using GameFolder.GlobalManagers;
using Unity.Netcode;

namespace GameFolder.Gameplay.HubLocation
{
    public class HubGateManager : GameplaySystem<HubGateManager>
    {
        private readonly List<ulong> _readyToGateUnits = new();
        private int _readyToOpenGatePlayers;
        
        protected override void Initialize() { }

        public void OnGateClicked(Unit unit)
        {
            OnGateClickedRpc(unit.OwnerClientId);
        }

        [Rpc(SendTo.Everyone)]
        public void OnGateClickedRpc(ulong clientId)
        {
            var unit = HubUnitsManager.Instance.GetUnitById(clientId);
            if (_readyToGateUnits.Contains(clientId))
            {
                unit.statsBoard.HideReadyMark();
                _readyToOpenGatePlayers--;
                _readyToGateUnits.Remove(clientId);
                return;
            }
            
            _readyToGateUnits.Add(clientId);
            unit.statsBoard.ShowReadyMark();
            _readyToOpenGatePlayers++;
            if (IsHost)
            {
                CheckCanOpenGate();
            }
        }

        private void CheckCanOpenGate()
        {
            if (_readyToOpenGatePlayers == HubUnitsManager.Instance.GetAllUnitsCount())
            {
                SceneController.LoadNetworkScene(SceneType.BattleLocation);
            }
        }
    }
}
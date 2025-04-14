using GameFolder.Gameplay.BattleLocation.Map;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.BattleLocation.UnitComponent
{
    public class BattleUnit : NetworkBehaviour
    {
        public Vector2Int gridPosition { get; private set; }

        public void InitializeServer(Vector2Int gridPos)
        {
            InitializeAllRpc(gridPos);
        }

        [Rpc(SendTo.Everyone)]
        private void InitializeAllRpc(Vector2Int gridPos)
        {
            gridPosition = gridPos;
            transform.position = MapManager.Instance.GetWorldPosition(gridPos);
            MapManager.Instance.GetCell(gridPos).IsOccupied = true;
        }

        public void DeinitializeServer()
        {
            DeInitializeAllRpc();
        }

        [Rpc(SendTo.Everyone)]
        private void DeInitializeAllRpc()
        {
            if (MapManager.Instance.GetCell(gridPosition).IsOccupied)
                MapManager.Instance.GetCell(gridPosition).IsOccupied = false;
        }
    }
}
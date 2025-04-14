using UnityEngine;

namespace GameFolder.Gameplay.BattleLocation.Map
{
    public class Cell
    {
        public Vector2Int GridPosition;
        public Vector3 WorldPosition;
        public bool IsOccupied;
        public GameObject TileObject;

        public Cell(Vector2Int gridPos, Vector3 worldPos, GameObject tileObject = null)
        {
            GridPosition = gridPos;
            WorldPosition = worldPos;
            TileObject = tileObject;
            IsOccupied = false;
        }
    }
}
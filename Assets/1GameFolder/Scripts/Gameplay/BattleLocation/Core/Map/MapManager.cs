using GameFolder.Gameplay.Networking;
using TriInspector;
using Unity.Netcode;
using UnityEngine;

namespace GameFolder.Gameplay.BattleLocation.Map
{
    public class MapManager : GameplaySystem<MapManager>
    {
        [SerializeField] private GameObject tilePrefab;
        [SerializeField] private Transform tileParent;
        [SerializeField] private int width = 8;
        [SerializeField] private int height = 6;
        [SerializeField] private float spacing = 1f;

        private Cell[,] _grid;
        private int _seed;

        protected override void Initialize() { }

        public void GenerateMapOnServer()
        {
            _seed = Random.Range(0, 1000000);
            GenereateWithSeedRpc(_seed);
        }
        
        [Rpc(SendTo.Everyone)]
        private void GenereateWithSeedRpc(int seed) 
        {
            Generate(seed);
        }
        
        public void Generate(int seed) {
            _seed = seed;
            Random.InitState(seed);

            _grid = new Cell[width, height];

            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    Vector3 worldPos = new Vector3(x * spacing, 0f, y * spacing);

                    var tileObj = Instantiate(tilePrefab, worldPos, Quaternion.identity, tileParent);

                    _grid[x, y] = new Cell(new Vector2Int(x, y), worldPos, tileObj);
                }
            }

            Debug.Log($"[MapManager] MAP GENERATED. SEED: {_seed}");
        }

        public Cell GetCell(Vector2Int gridPos) => _grid[gridPos.x, gridPos.y];

        public Vector3 GetWorldPosition(Vector2Int gridPos) => GetCell(gridPos).WorldPosition;

        public Cell GetStartCellForClient(ulong clientId) 
        {
            int index = (int)(clientId % 4);
            return _grid[1 + index, 1];
        }
        
        private void ClearAllTiles()
        {
            if (_grid != null) 
            {
                foreach (var cell in _grid) 
                {
                    if (cell?.TileObject != null) 
                    {
                        DestroyImmediate(cell.TileObject);
                    }
                }
            }
            
            foreach (Transform child in tileParent) 
            {
                DestroyImmediate(child.gameObject);
            }
        }
        
#if UNITY_EDITOR
        [ContextMenu("GenerateCheck")]
        public void GenerateCheck()
        {
            ClearAllTiles();
            Generate(Random.Range(0, 1000000));
        }
#endif
    }
}
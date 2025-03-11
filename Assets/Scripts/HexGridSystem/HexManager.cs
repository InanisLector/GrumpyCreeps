using ScriptableObjects.Grid;
using UnityEngine;

namespace HexGridSystem
{
    [RequireComponent(typeof(Grid))]
    public class HexManager : MonoBehaviour, IHexGrid
    {
        #region Data

        [SerializeField] private int width;
        [SerializeField] private int height;
        [Space]
        [SerializeField] private TileSriptableObject hex;

        private Grid _grid;
        private TileLogic[,] _tiles;

        #endregion

        #region Unity methods

        private void Awake()
        {
            _grid = GetComponent<Grid>();
        }

        private void Start()
        {
            transform.position += _grid.CellToWorld(new(width, height)) * (-0.5f);
            GenerateTiles();
        }


        #endregion

        #region Private Implementations

        private void GenerateTiles()
        {
            _tiles = new TileLogic[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _tiles[x, y] = new TileLogic(hex.InitialState);

                    var tile = Instantiate(hex.Prefab, _grid.CellToWorld(new(x, y, 0)), Quaternion.identity, this.transform);
                    tile.GetComponent<TileBehaviourScript>().SetLogicParent(_tiles[x, y]);
                }

            }
        }

        #endregion

        #region Public Implementations
        
        public Vector2Int WorldPositionToTile(Vector3 position)
        {
            Vector3Int unfilteredGridPosition = _grid.WorldToCell(position);
            Vector2Int gridPosition = new(0, 0);

            gridPosition.x = Mathf.Clamp(unfilteredGridPosition.x, 0, width - 1);
            gridPosition.y = Mathf.Clamp(unfilteredGridPosition.y, 0, height - 1);

            return gridPosition;
        }

        public Vector3 TileToWorldPosition(Vector2Int position)
            => _grid.CellToWorld(new Vector3Int(position.x, position.y, 0));

        private TileLogic[] GetSurroundingTilesInDiameter(Vector2Int centerTilePosition, int radius)
            => GetSurroundingTilesInDiameter(centerTilePosition.x, centerTilePosition.y, radius);

        private TileLogic[] GetSurroundingTilesInDiameter(int centerTileX, int centerTileY, int diameter)
        {
            if (diameter == 0)
                return new TileLogic[0];

            int radius = diameter / 2;

            int size = diameter * (2 * radius + 1) - radius * (radius + 1);
            var tiles = new TileLogic[size];
            int centerIndex = size / 2;


            centerTileX = Mathf.Clamp(centerTileX, radius, width - radius - 1);
            centerTileY = Mathf.Clamp(centerTileY, radius, height - radius - 1);

            int middleRowFirstIndex = centerIndex - radius;

            for (int x = 0; x < diameter; x++)
            {
                tiles[middleRowFirstIndex + x] = _tiles[centerTileX + x - radius, centerTileY];
            }

            for (int y = 1; y <= radius; y++)
            {
                int shiftConst = 0;

                if (centerTileY % 2 != 0 && y % 2 != 0)
                {
                    shiftConst = 1;
                }

                int downwardsShift = diameter * y - (y + 1) * y / 2;

                int upperStartIndex = middleRowFirstIndex - downwardsShift;
                int bottomStartIndex = middleRowFirstIndex + downwardsShift + y;

                int firstRowTileX = centerTileX - radius + y / 2 + shiftConst;

                for (int x = 0; x < diameter - y; x++)
                {
                    tiles[upperStartIndex + x] = _tiles[firstRowTileX + x, centerTileY + y];
                    tiles[bottomStartIndex + x] = _tiles[firstRowTileX + x, centerTileY - y];
                }
            }

            return tiles;
        }

        public void SelectTilesInDiameter(Vector2Int centerTilePosition, int diameter)
        {

            var tiles = GetSurroundingTilesInDiameter(centerTilePosition, diameter);

            if (tiles.Length == 0)
                return;

            foreach (var tile in tiles)
            {
                tile.StartedHoveringOnIt();
            }
        }

        public void DeselectTilesInDiameter(Vector2Int centerTilePosition, int diameter)
        {
            var tiles = GetSurroundingTilesInDiameter(centerTilePosition, diameter);

            foreach (var tile in tiles)
            {
                tile.StoppedHoveringOnIt();
            }
        }

        public bool CheckForVacancyInRadius(Vector2Int centerTilePosition, int diameter)
        {
            var tiles = GetSurroundingTilesInDiameter(centerTilePosition, diameter);

            foreach(var tile in tiles)
            {
                if (!tile.IsVacant())
                    return false;
            }

            return true;
        }

        public void AssignBuildingToTilesInRadius(Vector2Int centerTilePosition, int diameter, GameObject building)
        {
            var tiles = GetSurroundingTilesInDiameter(centerTilePosition, diameter);

            foreach(var tile in tiles)
            {
                tile.AddBuilding(building);
            }
        }

        #endregion
    }
}

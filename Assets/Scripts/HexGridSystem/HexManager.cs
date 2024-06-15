using ScriptableObjects.Grid;
using System;
using System.Collections.Generic;
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
        [SerializeField] private GameObject hex;
        [Space(2f)]
        [SerializeField] private ITileSettings[,] tilesToGenerate;
        [Space]
        [SerializeField] private bool blandGeneration;

        private Grid _grid;
        private IHexTile[,] _tiles;

        #endregion

        #region Unity methods

        private void Awake()
        {
            _grid = GetComponent<Grid>();
        }

        private void Start()
        {
            transform.position += _grid.CellToWorld(new(width, height)) * (-0.5f);
            if (blandGeneration)
                GenerateTilesWithSinceHexType();
            else
                GenerateTiles();
        }


        #endregion

        #region Private Implementations

        private void GenerateTilesWithSinceHexType()
        {
            _tiles = new HexTile[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    _tiles[x, y] = Instantiate(hex, _grid.CellToWorld(new(x, y)), Quaternion.identity, this.transform)
                        .GetComponent<HexTile>();

                    _tiles[x, y].SetGridPosition(x, y);
                }

            }
        }

        private void GenerateTiles()
        {
            
        }

        #endregion

        #region Public Implementations
        
        public Vector3Int WorldPositionToTile(Vector3 position)
        {
            var gridPosition = _grid.WorldToCell(position);

            gridPosition.x = Mathf.Clamp(gridPosition.x, 0, width - 1);
            gridPosition.y = Mathf.Clamp(gridPosition.y, 0, height - 1);

            return gridPosition;
        }

        public Vector3 TileToWorldPosition(Vector3Int position)
            => _grid.CellToWorld(position);

        private IHexTile[] GetSurroundingTilesInDiameter(Vector3Int centerTilePosition, int radius)
            => GetSurroundingTilesInDiameter(centerTilePosition.x, centerTilePosition.y, radius);

        private IHexTile[] GetSurroundingTilesInDiameter(int centerTileX, int centerTileY, int diameter)
        {
            if (diameter == 0)
                return new IHexTile[0];

            int radius = diameter / 2;

            int size = diameter * (2 * radius + 1) - radius * (radius + 1);
            var tiles = new IHexTile[size];
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

        public void SelectTilesInDiameter(Vector3Int centerTilePosition, int diameter)
        {

            var tiles = GetSurroundingTilesInDiameter(centerTilePosition, diameter);

            if (tiles.Length == 0)
                return;

            foreach (var tile in tiles)
            {
                tile.Select();
            }
        }

        public void DeselectTilesInDiameter(Vector3Int centerTilePosition, int diameter)
        {
            var tiles = GetSurroundingTilesInDiameter(centerTilePosition, diameter);

            foreach (var tile in tiles)
            {
                tile.Deselect();
            }
        }

        #endregion
    }
}

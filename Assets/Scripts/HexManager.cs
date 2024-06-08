using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HexManager : MonoBehaviour
{
    #region Data

    [SerializeField] private int width;
    [SerializeField] private int height;
    [Space]
    [SerializeField] private GameObject hex1;
    [SerializeField] private GameObject hex2;
    [SerializeField] private GameObject hex3;

    private Grid _grid;
    private TileScript[,] _tiles;

    #endregion

    #region Unity methods

    private void Awake()
    {
        _grid = GetComponent<Grid>();
    }

    private void Start()
    {
        GenerateTiles();
    }

    #endregion

    #region Private Implementations

    private void GenerateTiles()
    {
        var rotation = Quaternion.Euler(0, 90, 0);
        _tiles = new TileScript[width, height];
        var offset = (_grid.GetCellCenterWorld(new(0, 0)) - _grid.GetCellCenterWorld(new(width - 1, height - 1))) / 2;

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var worldPosition = _grid.GetCellCenterWorld(new(x, y));
                _tiles[x, y] = Instantiate((x + y) % 2 == 0 ? hex1 : hex2,
                                            worldPosition + offset,
                                            rotation,
                                            this.gameObject.transform)
                                .GetComponent<TileScript>();
                _tiles[x, y].SetGridPosition(x, y);
            }
        }
    }
    
    public TileScript[] GetSurroundingTilesInDiameter(Vector2Int centerTilePosition, int radius)
        => GetSurroundingTilesInDiameter(centerTilePosition.x, centerTilePosition.y, radius);

    #endregion

    #region Public Implementations

    public TileScript[] GetSurroundingTilesInDiameter(int centerTileX, int centerTileY, int diameter)
    {
        var tiles = new List<TileScript>();

        int halfRadius = diameter / 2;

        centerTileX = Mathf.Clamp(centerTileX, halfRadius, width - halfRadius - 1);
        centerTileY = Mathf.Clamp(centerTileY, halfRadius, height - halfRadius - 1);

        for (int x = 0; x < diameter; x++) // y == 0
        {
            tiles.Add(_tiles[centerTileX + x - halfRadius, centerTileY]);
        }

        for (int y = 1; y <= halfRadius; y++) // 2
        {
            int shiftConst = 0;

            if (centerTileY % 2 != 0 && y % 2 != 0)
            {
                shiftConst = 1;
            }

            for (int x = 0; x < diameter - y; x++)
            {
                int tileX = centerTileX + x - halfRadius + y/2 + shiftConst; // QwQ It took way too long

                tiles.Add(_tiles[tileX, centerTileY + y]);
                tiles.Add(_tiles[tileX, centerTileY - y]);
            }
        }

        return tiles.ToArray();
    }

    #endregion
}

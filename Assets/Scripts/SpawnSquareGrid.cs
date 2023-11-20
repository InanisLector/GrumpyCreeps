using UnityEngine;

public class SpawnSquareGrid : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private int gridWidth;
    [SerializeField] private int gridLenght;

    private HoverColorSquares[,] _tiles;

    private void Start()
    {
        var grid = this.GetComponent<Grid>();

        var rotation = Quaternion.Euler(90, 0, 0);

        _tiles = new HoverColorSquares[gridWidth, gridLenght];


        for (int i = 0; i < gridLenght; i++)
        {
            for (int j = 0; j < gridWidth; j++)
            {
                var worldPosition = grid.GetCellCenterWorld(new(i, j, 0));

                _tiles[j, i] = Instantiate(prefab, worldPosition, rotation).GetComponent<HoverColorSquares>();
                _tiles[j, i].SetCoordinates(j, i);
                _tiles[j, i].SetParent(this);
            }
        }
    }

    public HoverColorSquares[] GetNineSurround(Vector2Int coordinates)
    {
        coordinates.x = Mathf.Clamp(coordinates.x, 1, gridWidth - 2);
        coordinates.y = Mathf.Clamp(coordinates.y, 1, gridLenght - 2);

        var array = new HoverColorSquares[9];

        array[0] = _tiles[coordinates.x - 1, coordinates.y - 1];
        array[1] = _tiles[coordinates.x - 1, coordinates.y];
        array[2] = _tiles[coordinates.x - 1, coordinates.y + 1];

        array[3] = _tiles[coordinates.x, coordinates.y - 1];
        array[4] = _tiles[coordinates.x, coordinates.y];
        array[5] = _tiles[coordinates.x, coordinates.y + 1];

        array[6] = _tiles[coordinates.x + 1, coordinates.y - 1];
        array[7] = _tiles[coordinates.x + 1, coordinates.y];
        array[8] = _tiles[coordinates.x + 1, coordinates.y + 1];

        return array;
    }
}

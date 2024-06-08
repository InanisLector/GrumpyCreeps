using UnityEngine;

public class TileScript : MonoBehaviour
{
    [HideInInspector] public bool IsFree
        => _building == null;

    private HexManager _manager;
    private Vector2Int _gridPosition;

    private GameObject _building;

    private void Awake()
    {
        _manager = this.gameObject.GetComponentInParent<HexManager>();
    }

    public void SetGridPosition(int x, int y)
        => _gridPosition = new(x, y);
}

using UnityEngine;

public class HoverColorSquares : MonoBehaviour
{
    [SerializeField] private Material disabledMaterial;
    [SerializeField] private Material enabledMaterial;

    private SpawnSquareGrid _parent;
    private Vector2Int _gridPosition;
    private MeshRenderer _meshRenderer;


    private void Start()
    {
        _meshRenderer = this.GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        var tiles = _parent.GetNineSurround(_gridPosition);

        foreach (var tile in tiles)
        {
            if(tile != this)
                tile.EnableHover();

            tile.EnableMaterial();
        }

        this.EnableHover(2);
    }

    private void OnMouseExit()
    {
        var hexes = _parent.GetNineSurround(_gridPosition);

        foreach (var hex in hexes)
        {
            if (hex != this)
                hex.DisableHover();

            hex.DisableMaterial();
        }

        this.DisableHover(2);
    }

    public void SetCoordinates(int x, int y)
        => _gridPosition = new Vector2Int(x, y);

    public void SetParent(SpawnSquareGrid parent)
        => _parent = parent;

    private void EnableHover(float modifier = 1f)
    {
        var pos = transform.position;

        pos.y += 0.1f * modifier;

        transform.position = pos;
    }

    private void DisableHover(float modifier = 1f)
    {
        var pos = transform.position;

        pos.y -= 0.1f * modifier;

        transform.position = pos;
    }

    private void EnableMaterial()
        => _meshRenderer.material = enabledMaterial;

    private void DisableMaterial()
        => _meshRenderer.material = disabledMaterial;
}

using System;
using System.Diagnostics.Contracts;
using Unity.Transforms;
using UnityEngine;

public class HoverColor : MonoBehaviour
{
    [SerializeField] private Material disabledMaterial;
    [SerializeField] private Material enabledMaterial;
    [SerializeField] private Material builtMaterial;

    private SpawnGrid _parent;
    private Vector2Int _gridPosition;
    private MeshRenderer _meshRenderer;


    private void Start()
    {
        _meshRenderer = this.GetComponent<MeshRenderer>();
    }

    private void OnMouseEnter()
    {
        var hexes = _parent.GetSevenSurround(_gridPosition);

        foreach (var hex in hexes)
        {
            if (!hex.GetComponent<TileLogic>().TowerCanBeBuilt())
                continue;

            if(hex != this)
                hex.EnableHover();

            hex.EnableMaterial();
        }

        this.EnableHover(2);
    }

    private void OnMouseExit()
    {
        var hexes = _parent.GetSevenSurround(_gridPosition);

        foreach (var hex in hexes)
        {
            if (!hex.GetComponent<TileLogic>().TowerCanBeBuilt())
                continue;

            if (hex != this)
                hex.DisableHover();

            hex.DisableMaterial();
        }

        this.DisableHover(2);
    }

    private void OnMouseDown()
    {
        var hexes = _parent.GetSevenSurround(_gridPosition);
        var hexesLogics = new TileLogic[7];

        for (int i = 0; i < 7; i++)
        {
            hexesLogics[i] = hexes[i].GetComponent<TileLogic>();

            if (!hexesLogics[i].TowerCanBeBuilt())
                return;
        }

        Build(hexes, hexesLogics);
    }

    private void Build(HoverColor[] hexes, TileLogic[] hexesLogics)
    {
        OnMouseExit();

        for (int i = 0; i < 7; i++)
        {
            hexesLogics[i].PlaceTower();
            hexes[i].BuiltMaterial();
        }
    }

    public void SetCoordinates(int x, int y)
        => _gridPosition = new Vector2Int(x, y);

    public void SetParent(SpawnGrid parent)
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

    private void BuiltMaterial()
        => _meshRenderer.material = builtMaterial;
}

using UnityEngine;

public class TileLogic : MonoBehaviour // Template
{
    private bool _hasTowerOnIt = false;

    public void PlaceTower()
        => _hasTowerOnIt = true;

    public bool TowerCanBeBuilt()
        => !_hasTowerOnIt;
}

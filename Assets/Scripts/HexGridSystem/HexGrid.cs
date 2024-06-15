using UnityEngine;

namespace HexGridSystem
{
    public interface IHexGrid
    {
        public Vector3Int WorldPositionToTile(Vector3 position);
        public Vector3 TileToWorldPosition(Vector3Int position);
        //public Vector3Int[] GetSurroundingTilesInDiameter(Vector3Int centerTilePosition, int diameter);
        //public Vector3Int[] GetSurroundingTilesInDiameter(int centerTileX, int centerTileY, int diameter);
        public void SelectTilesInDiameter(Vector3Int centerTilePosition, int diameter);
        public void DeselectTilesInDiameter(Vector3Int centerTilePosition, int diameter);
    }
}

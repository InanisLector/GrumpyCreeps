using UnityEngine;

namespace HexGridSystem
{
    public interface IHexGrid
    {
        public Vector2Int WorldPositionToTile(Vector3 position);
        public Vector3 TileToWorldPosition(Vector2Int position);
        //public Vector3Int[] GetSurroundingTilesInDiameter(Vector3Int centerTilePosition, int diameter);
        //public Vector3Int[] GetSurroundingTilesInDiameter(int centerTileX, int centerTileY, int diameter);
        public void SelectTilesInDiameter(Vector2Int centerTilePosition, int diameter);
        public void DeselectTilesInDiameter(Vector2Int centerTilePosition, int diameter);

        /// <summary>
        /// Checks if any tiles in radius are occupied by a different building
        /// </summary>
        /// <returns> True if all the tiles are vacant </returns>
        public bool CheckForVacancyInRadius(Vector2Int centerTilePosition, int diameter);
        
        public void AssignBuildingToTilesInRadius(Vector2Int centerTilePosition, int diameter, GameObject building); // Definetly should rewrite it so I don't have to search for all the tiles, but non emirgent
    }
}

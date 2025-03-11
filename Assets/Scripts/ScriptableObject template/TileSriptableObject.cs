using UnityEngine;
using HexGridSystem;

namespace ScriptableObjects.Grid
{
    [CreateAssetMenu(fileName = "Tile preset", menuName ="Tiles")]
    public class TileSriptableObject : ScriptableObject
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float initialVerticalOffset = 0f;
        [Space]
        [SerializeField] private InitialTileState tileState;

        public GameObject Prefab
            => prefab;
        public InitialTileState InitialState 
            => tileState;
    }
}

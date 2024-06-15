using UnityEngine;

namespace ScriptableObjects.Grid
{
    [CreateAssetMenu(fileName = "Tile preset", menuName ="Tiles")]
    public class TileSriptableObject : ScriptableObject, ITileSettings
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private float initialVerticalOffset = 0f;
        [Space]
        [SerializeField] private TileAvialability accessablity = TileAvialability.Available;

        public GameObject Prefab
            => prefab;
        public float InitialVerticalOffset
            => initialVerticalOffset;

        public bool Available
            => accessablity != TileAvialability.UnAvailable;

        public bool HasToBePurchased
            => accessablity == TileAvialability.HasToBePurchased;
    }

    enum TileAvialability
    { 
        Available,
        HasToBePurchased,
        UnAvailable,
    }
}

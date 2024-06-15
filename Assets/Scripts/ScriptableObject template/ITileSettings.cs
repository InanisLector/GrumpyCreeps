using UnityEngine;

namespace ScriptableObjects.Grid
{
    public interface ITileSettings
    {
        public GameObject Prefab { get; }
        public float InitialVerticalOffset { get; }
        public bool Available { get; }
        public bool HasToBePurchased { get; }
    }
}

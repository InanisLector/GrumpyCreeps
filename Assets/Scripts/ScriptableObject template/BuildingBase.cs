using UnityEngine;

namespace ScriptableObjects.Building
{
    [CreateAssetMenu(fileName = "BuildingBase", menuName = "Buildings")]
    public class BuildingBase : ScriptableObject
    {
        [SerializeField] private int cost;
        [SerializeField] private int size;
        [SerializeField] private GameObject prefab;


        public int Cost
            => cost;
        public int Size
            => size;
        public GameObject Prefab
            => prefab;
    }
}

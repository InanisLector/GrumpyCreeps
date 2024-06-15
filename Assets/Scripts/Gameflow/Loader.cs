using UnityEngine;

namespace GameFlow.Loader
{
    public class Loader : MonoBehaviour
    {
        [SerializeField] private GameObject[] objectsToLoad;

        private void Start()
        {
            foreach (var obj in objectsToLoad)
            {
                Instantiate(obj);
            }
        }
    }
}

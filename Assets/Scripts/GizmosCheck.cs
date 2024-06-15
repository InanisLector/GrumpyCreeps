using System;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public struct V3Pairs
    {
        public Vector3 v1;
        public Vector3 v2;

        public V3Pairs(Vector3 v1, Vector3 v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
    }

    public class GizmosCheck : MonoBehaviour
    {
        [SerializeField] private V3Pairs[] pairs;

        private void OnDrawGizmos()
        {
            foreach (var pair in pairs)
            {
                Gizmos.DrawLine(pair.v1, pair.v2);
            }
        }
    }
}

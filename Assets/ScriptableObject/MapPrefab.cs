using GC.Spline;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Map/MapPrefab", fileName = "NewMapPrefab", order = 0)]
public class MapPrefab : ScriptableObject
{
    public GameObject SplineSegments;
}

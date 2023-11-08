using Unity.Collections;
using Unity.Entities;
using UnityEngine;

[ChunkSerializable]
public struct SplineContainer : IComponentData
{
    public NativeList<Spline> Splines;
    public bool isSetUp;

    public Spline GetSplineByIndex(int splineIndex)
    {
        if(splineIndex >= Splines.Length || splineIndex < 0)
        {
            Debug.LogError("Spline Container: Spline Index was out of bounds!");

            return new Spline();
        }

        return Splines[splineIndex];
    }
}

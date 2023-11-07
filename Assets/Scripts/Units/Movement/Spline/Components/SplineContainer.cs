using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public struct SplineContainer : IComponentData
{
    public NativeList<Spline> Splines;

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

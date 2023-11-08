using System;
using Unity.Mathematics;

[Serializable]
public struct SerializeSplineSegment
{
    public float3 StartPoint;
    public float3 FirstInterpolationPoint;
    public float3 SecondInterpolationPoint;
    public float3 EndPoint;
}

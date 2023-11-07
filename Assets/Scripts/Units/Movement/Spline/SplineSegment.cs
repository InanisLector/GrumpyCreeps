using System;
using Unity.Entities;
using Unity.Mathematics;

[Serializable]
public struct SplineSegment : IBufferElementData
{
    public float3 StartPoint;
    public float3 FirstInterpolationPoint;
    public float3 SecondInterpolationPoint;
    public float3 EndPoint;

    public SplineSegmentData SplineData;

    public void Init(float startPercent)
    {
        SplineData = new SplineSegmentData(this, startPercent);
    }

    public float3 GetPointOnSegment(float t)
    {
        float oneMinusT = 1f - t;

        return
            oneMinusT * oneMinusT * oneMinusT * this.StartPoint +
            3f * oneMinusT * oneMinusT * t * this.FirstInterpolationPoint +
            3f * oneMinusT * t * t * this.SecondInterpolationPoint +
            t * t * t * this.EndPoint;
    }
}

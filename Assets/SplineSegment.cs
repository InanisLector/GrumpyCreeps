using System;
using UnityEngine;

[Serializable]
public class SplineSegment
{
    [field: SerializeField] public Transform StartPoint { get; private set; }
    [field: SerializeField] public Transform FirstInterpolationPoint { get; private set; }
    [field: SerializeField] public Transform SecondInterpolationPoint { get; private set; }
    [field: SerializeField] public Transform EndPoint { get; private set; }

    public SplineSegmentData SplineData { get; private set; }

    public void Init(float startPercent)
    {
        SplineData = new SplineSegmentData(this, startPercent);
    }

    public Vector3 GetPointOnSegment(float t)
    {
        float oneMinusT = 1f - t;

        return
            oneMinusT * oneMinusT * oneMinusT * this.StartPoint.position +
            3f * oneMinusT * oneMinusT * t * this.FirstInterpolationPoint.position +
            3f * oneMinusT * t * t * this.SecondInterpolationPoint.position +
            t * t * t * this.EndPoint.position;
    }
}

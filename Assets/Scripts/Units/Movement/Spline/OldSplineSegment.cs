using System;
using UnityEngine;

[Serializable]
public struct OldSplineSegment
{
    [field : SerializeField] public Transform StartPoint { get; private set;}
    [field : SerializeField] public Transform FirstInterpolationPoint { get; private set; }
    [field : SerializeField] public Transform SecondInterpolationPoint { get; private set; }
    [field : SerializeField] public Transform EndPoint { get; private set; }

    [field : HideInInspector] public float StartPercent;
    [field : HideInInspector] public float LengthPercent;

    private float _length;

    public Vector3 GetPointOnSegment(float t)
    {
        float oneMinusT = 1f - t;

        return
            oneMinusT * oneMinusT * oneMinusT * this.StartPoint.position +
            3f * oneMinusT * oneMinusT * t * this.FirstInterpolationPoint.position +
            3f * oneMinusT * t * t * this.SecondInterpolationPoint.position +
            t * t * t * this.EndPoint.position;
    }

    public Vector3 GetFirstDerivirate(float t)
    {
        float oneMinusT = 1f - t;

        return
            3f * oneMinusT * oneMinusT * (this.FirstInterpolationPoint.position - this.StartPoint.position) +
            6f * oneMinusT * t * (this.SecondInterpolationPoint.position - this.FirstInterpolationPoint.position) +
            3f * t * t * (this.EndPoint.position - this.SecondInterpolationPoint.position);
    }

    public float GetLength()
    {
        if (_length > 0)
            return _length;

        _length = CalculateSegmentLenght();

        return _length;
    }


    public float CalculateSegmentLenght(int precision = 25)
    {
        float distance = 0f;
        float step = 1f / precision;

        Vector3 previousPoint = this.StartPoint.position;

        for (int i = 0; i < precision; i++)
        {
            Vector3 point = this.GetPointOnSegment(step * i);

            Vector3 vector = point - previousPoint;

            distance += vector.magnitude;
        }

        return distance;
    }
}

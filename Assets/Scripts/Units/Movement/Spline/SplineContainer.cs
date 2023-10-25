using System.Collections.Generic;
using UnityEngine;

public class SplineContainer : MonoBehaviour
{
    [field : Header("Spline")]
    [field : SerializeField] public List<SplineSegment> Spline;
    [Space]
    [Header("Gizmos")]
    [SerializeField] private int segmentCount = 20;

    public float SplineLength { get; private set; }

public Vector3 GetPointOnSpline(float time)
{
    int segmentIndex = (int)time;
    float t = time % 1;
    float oneMinusT = 1f - t;

    SplineSegment segment = Spline[segmentIndex];

    return
        oneMinusT * oneMinusT * oneMinusT * segment.StartPoint.position +
        3f * oneMinusT * oneMinusT * t * segment.FirstInterpolationPoint.position +
        3f * oneMinusT * t * t * segment.SecondInterpolationPoint.position +
        t * t * t * segment.EndPoint.position;
}

    public Vector3 GetFirstDerivirate(int segmentIndex, float t)
    {
        t = Mathf.Clamp01(t);

        float oneMinusT = 1f - t;

        SplineSegment segment = Spline[segmentIndex];

        return
            3f * oneMinusT * oneMinusT * (segment.FirstInterpolationPoint.position - segment.StartPoint.position) +
            6f * oneMinusT * t * (segment.SecondInterpolationPoint.position - segment.FirstInterpolationPoint.position) +
            3f * t * t * (segment.EndPoint.position - segment.SecondInterpolationPoint.position);
    }

    public float GetSegmentDistance(int segmentIndex)
    {
        SplineSegment segment = Spline[segmentIndex];

        float distance = 0f;

        Vector3 previousPoint = segment.StartPoint.position;

        for(int i = 0; i < 25; i++)
        {
            float t = i / 25f;

            Vector3 point = GetPointOnSpline(t);

            Vector3 vector = point - previousPoint;

            distance += vector.magnitude;
        }

        return distance;
    }
    private void GetSplineLenght()
    {
        float totalDistance = 0f;

        for(int i = 0; i < Spline.Count; i++)
        {
            totalDistance += GetSegmentDistance(i);
        }

        SplineLength = totalDistance / Spline.Count;
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (Spline == null)
            return;

        if (HasNullPoints())
            return;

        for(int i = 0; i < Spline.Count; i++)
        {
            Vector3 previousPoint = Spline[i].StartPoint.position;

            for(int j = 0; j <= segmentCount; j++)
            {
                float t = (float)j / segmentCount;

                Vector3 point = GetPointOnSpline(t);

                Gizmos.color = Color.red;

                Gizmos.DrawLine(previousPoint, point);

                Gizmos.color = Color.green;

                Gizmos.DrawSphere(point, 0.65f);

                previousPoint = point;
            }
        }
    }
    private bool HasNullPoints()
    {
        foreach(var segment in Spline)
        {
            Transform[] points = new Transform[4] { segment.StartPoint, segment.FirstInterpolationPoint, segment.SecondInterpolationPoint, segment.EndPoint };

            foreach(var point in points)
            {
                if (point == null)
                    return true;
            }
        }

        return false;
    }
    #endregion
}

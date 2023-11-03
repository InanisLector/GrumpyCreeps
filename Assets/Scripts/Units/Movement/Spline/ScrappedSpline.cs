using System.Linq;
using UnityEngine;
using UnityEngine.Rendering;

public class ScrappedSpline : MonoBehaviour
{
    [Header("Spline")]
    [SerializeField] private OldSplineSegment[] spline;
    [Space]
    [Header("Gizmos")]
    [SerializeField] private int segmentCount = 20;

    public float SplineLength { get; private set; }

    private int _countOfBrokenLinePoints = 100;
    private Vector3[] _brokenLinePoints;
    private float[] _brokenLinePercents;

    private void Awake()
    {
        Init();
    }

    #region INIT
    private void Init()
    {
        InitSplineLength();

        InitSegmentPercents();
    }

    private void InitSplineLength()
    {
        float totalDistance = 0f;

        for (int i = 0; i < spline.Length; i++)
        {
            totalDistance += spline[i].GetLength();
        }

        SplineLength = totalDistance;
    }

    private void InitSegmentPercents()
    {
        spline[0].StartPercent = 0;
        spline[0].LengthPercent = spline[0].GetLength() / SplineLength;

        for (int i = 1; i < spline.Length; i++)
        {
            spline[i].StartPercent = spline[i - 1].StartPercent + spline[i - 1].LengthPercent;
            spline[i].LengthPercent = spline[i].GetLength() / SplineLength;
        }
    }
    #endregion

    public Vector3 GetPointOnSpline(float percent) // 0 > x > 1
    {
        percent = Mathf.Clamp01(percent);

        for (int i = 1; i < spline.Length; i++)
        {
            if (spline[i].StartPercent < percent)
                continue;

            float calculatedPercent = (percent - spline[i - 1].StartPercent) / spline[i - 1].LengthPercent;

            return spline[i - 1].GetPointOnSegment(calculatedPercent);
        }

        return spline[^1].GetPointOnSegment((percent - spline[^1].StartPercent) / spline[^1].LengthPercent);
    }
    public Vector3 GetLookDirection(float percent) // 0 > x > 1
    {
        percent = Mathf.Clamp01(percent);

        for (int i = 1; i < spline.Length; i++)
        {
            if (spline[i].StartPercent < percent)
                continue;

            float calculatedPercent = (percent - spline[i - 1].StartPercent) / spline[i - 1].LengthPercent;

            float alpha = (calculatedPercent - spline[i - 1].StartPercent) / (spline[i - 1].StartPercent - spline[i].StartPercent);

            return spline[i - 1].GetFirstDerivirate(alpha);
        }

        return spline[^1].GetFirstDerivirate((percent - spline[^1].StartPercent) / spline[^1].LengthPercent);
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (spline == null)
            return;

        if (HasNullPoints())
            return;

        for(int i = 0; i < spline.Length; i++)
        {
            Vector3 previousPoint = spline[i].StartPoint.position;

            for(int j = 0; j <= segmentCount; j++)
            {
                float t = (float)j / segmentCount;

                Vector3 point = GetDrawPoint(i, t);

                Gizmos.color = Color.red;

                Gizmos.DrawLine(previousPoint, point);

                Gizmos.color = Color.green;

                Gizmos.DrawSphere(point, 0.65f);

                previousPoint = point;
            }
        }
    }

    private Vector3 GetDrawPoint(int segmentIndex, float t)
    {
        float oneMinusT = 1f - t;

        var segment = spline[segmentIndex];

        return
            oneMinusT * oneMinusT * oneMinusT * segment.StartPoint.position +
            3f * oneMinusT * oneMinusT * t * segment.FirstInterpolationPoint.position +
            3f * oneMinusT * t * t * segment.SecondInterpolationPoint.position +
            t * t * t * segment.EndPoint.position;
    }

    private bool HasNullPoints()
    {
        foreach(var segment in spline)
        {
            Transform[] points = { segment.StartPoint, segment.FirstInterpolationPoint, segment.SecondInterpolationPoint, segment.EndPoint };

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

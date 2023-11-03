using System.Collections.Generic;
using UnityEngine;

public class Spline : MonoBehaviour
{
    [field: Header("Spline")]
    [field: SerializeField] public List<SplineSegment> SplineSegments { get; private set; }
    [Space]
    [field: Header("Gizmos")]
    [SerializeField] private Color linearSplineColor = new Color(255f, 0f, 255f, 255f);
    [SerializeField] private Color splineColor = new Color(255f, 0f, 0f, 255f);
    [SerializeField] private Color splinePointColor = new Color(0f, 255f, 0f, 255f);

    private float _splineLength = 0f;

    private void Awake()
    {
        InitSpline();

        GetTotalLength();
    }

    private void InitSpline()
    {
        SplineSegments[0].Init(0);

        for (int i = 1; i < SplineSegments.Count; i++)
        {
            float startPercent = SplineSegments[i - 1].SplineData.brokenLinesPercents[^1];

            SplineSegments[i].Init(startPercent);
        }
    }

    private void GetTotalLength()
    {
        foreach (var segment in SplineSegments)
        {
            _splineLength += segment.SplineData.brokenLinesPercents[^1];
        }
    }

    public (Vector3 position, Vector3 rotation) GetPointAndRotationOnSpline(float time)
    {
        SplineSegment currentSegment = GetCurrentSegment(time);

        var data = currentSegment.SplineData;

        var percents = data.brokenLinesPercents;
        var points = data.brokenLinePoints;
        var rotations = data.brokenLineRotations;

        for (int i = 0; i < percents.Count - 1; i++)
        {
            if (time > percents[i] && time < percents[i + 1])
            {
                float alpha = (time - percents[i]) / (percents[i + 1] - percents[i]);

                return (Vector3.Lerp(points[i], points[i + 1], alpha), Vector3.Lerp(rotations[i], rotations[i + 1], alpha));
            }
        }

        return (points[^1], rotations[^1]);
    }

    private SplineSegment GetCurrentSegment(float time)
    {
        for(int i = 0; i < SplineSegments.Count; i++)
        {
            float percent = SplineSegments[i].SplineData.brokenLinesPercents[^1];

            if (time / _splineLength < percent / _splineLength)
                return SplineSegments[i];
        }

        return SplineSegments[^1];
    }

    #region Gizmos
    private void OnDrawGizmos()
    {
        if (SegmentsAreNull())
            return;

        foreach (var segment in SplineSegments)
        {
            DrawLinearSpline(segment);

            DrawCubicSpline(segment);
        }
    }

    private void DrawLinearSpline(SplineSegment segment)
    {
        Gizmos.color = linearSplineColor;

        Gizmos.DrawLine(segment.StartPoint.position, segment.FirstInterpolationPoint.position);
        Gizmos.DrawLine(segment.FirstInterpolationPoint.position, segment.SecondInterpolationPoint.position);
        Gizmos.DrawLine(segment.SecondInterpolationPoint.position, segment.EndPoint.position);
    }

    private void DrawCubicSpline(SplineSegment segment) 
    {
        Vector3 previousPoint = segment.StartPoint.position;

        int countOfPoints = SplineSegmentData.COUNT_OF_BROKEN_LINE_POINTS;

        for (int i = 0; i <= countOfPoints; i++)
        {
            Vector3 currentPoint = segment.GetPointOnSegment((float)i / countOfPoints);

            Gizmos.color = splineColor;

            Gizmos.DrawLine(previousPoint, currentPoint);

            Gizmos.color = splinePointColor;

            Gizmos.DrawSphere(currentPoint, 0.25f);

            previousPoint = currentPoint;
        }
    }

    private bool SegmentsAreNull()
    {
        if (SplineSegments == null)
            return true;

        foreach(var segment in SplineSegments)
        {
            if (segment.StartPoint == null)
                return true;

            if(segment.FirstInterpolationPoint == null)
                return true;

            if (segment.SecondInterpolationPoint == null)
                return true;

            if(segment.EndPoint == null)
                return true;
        }

        return false;
    }
    #endregion
}

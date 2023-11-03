using System.Collections.Generic;
using UnityEngine;

public struct SplineSegmentData
{
    public const int COUNT_OF_BROKEN_LINE_POINTS = 50;

    public List<Vector3> brokenLinePoints { get; private set; }
    public List<Vector3> brokenLineRotations { get; private set; }
    public List<float> brokenLinesPercents { get; private set; }

    public SplineSegmentData(SplineSegment segmentToInit, float startPercent) 
    {
        brokenLinePoints = new();
        brokenLineRotations = new();
        brokenLinesPercents = new();

        InitBrokenLinePoints(segmentToInit);

        InitBrokenLinePercents(startPercent);
    }

    private void InitBrokenLinePoints(SplineSegment segment)
    {
        for (int i = 0; i <= COUNT_OF_BROKEN_LINE_POINTS; i++)
        {
            brokenLinePoints.Add(segment.GetPointOnSegment((float)i / COUNT_OF_BROKEN_LINE_POINTS));
        }
    }

    private void InitBrokenLinePercents(float startPercent)
    {
        brokenLinesPercents.Add(0 + startPercent);
        brokenLineRotations.Add(Vector3.zero);

        for (int i = 0; i < brokenLinePoints.Count - 1; i++)
        {
            var start = brokenLinePoints[i];
            var end = brokenLinePoints[i + 1];

            float length = (end - start).magnitude;

            brokenLinesPercents.Add(length + brokenLinesPercents[i]);
            brokenLineRotations.Add(end - start);
        }
    }
}

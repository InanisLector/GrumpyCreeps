using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;

public struct Spline : IComponentData
{
    public NativeList<SplineSegment> SplineSegments;

    private float _splineLength;

    public void Init(NativeList<SplineSegment> segments)
    {
        SplineSegments = segments;

        _splineLength = 0f;

        InitSpline();

        GetTotalLength();
    }

    private void InitSpline()
    {
        SplineSegments[0].Init(0);

        for (int i = 1; i < SplineSegments.Length; i++)
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

    public (float3 position, float3 rotation) GetPointAndRotationOnSpline(float time)
    {
        int currentSegmentIndex = GetCurrentSegment(time);

        var data = SplineSegments[currentSegmentIndex].SplineData;

        var percents = data.brokenLinesPercents;
        var points = data.brokenLinePoints;
        var rotations = data.brokenLineRotations;

        for (int i = 0; i < percents.Length - 1; i++)
        {
            if (time > percents[i] && time < percents[i + 1])
            {
                float alpha = (time - percents[i]) / (percents[i + 1] - percents[i]);

                return (math.lerp(points[i], points[i + 1], alpha), math.lerp(rotations[i], rotations[i + 1], alpha));
            }
        }

        return (points[^1], rotations[^1]);
    }

    private int GetCurrentSegment(float time)
    {
        for(int i = 0; i < SplineSegments.Length; i++)
        {
            float percent = SplineSegments[i].SplineData.brokenLinesPercents[^1];

            if (time / _splineLength < percent / _splineLength)
                return i;
        }

        return SplineSegments.Length - 1;
    }
}

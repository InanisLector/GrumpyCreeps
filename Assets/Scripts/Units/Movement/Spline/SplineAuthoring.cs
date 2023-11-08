using System.Collections.Generic;
using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class SplineAuthoring : MonoBehaviour
{
    //[Header("Gizmos")]
    //public List<SerializeSplineSegment> SplineSegments = new List<SerializeSplineSegment>();
    public List<SplineSegment> SplineSegments = new List<SplineSegment>();
    [Space]
    [Header("Gizmos")]
    [SerializeField] private Color brokenLinePointColor = new Color(0f, 255f, 0f, 255f);
    [SerializeField] private Color splineColor = new Color(255f, 0f, 0f, 255f);
    [SerializeField] private Color linearSplineColor = new Color(255f, 0f, 255f, 255f);
    [SerializeField] private float gizmosIconSize = 1f;

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

        Vector3 cubeSize = new Vector3(gizmosIconSize, gizmosIconSize, gizmosIconSize);

        Gizmos.DrawLine(segment.StartPoint, segment.FirstInterpolationPoint);
        Gizmos.DrawLine(segment.FirstInterpolationPoint, segment.SecondInterpolationPoint);
        Gizmos.DrawLine(segment.SecondInterpolationPoint, segment.EndPoint);

        Gizmos.DrawCube(segment.StartPoint, cubeSize);
        Gizmos.DrawCube(segment.FirstInterpolationPoint, cubeSize);
        Gizmos.DrawCube(segment.SecondInterpolationPoint, cubeSize);
        Gizmos.DrawCube(segment.EndPoint, cubeSize);
    }

    private void DrawCubicSpline(SplineSegment segment)
    {
        Vector3 previousPoint = segment.StartPoint;

        int countOfPoints = SplineSegmentData.COUNT_OF_BROKEN_LINE_POINTS;

        for (int i = 0; i <= countOfPoints; i++)
        {
            Vector3 currentPoint = segment.GetPointOnSegment((float)i / countOfPoints);

            Gizmos.color = splineColor;

            Gizmos.DrawLine(previousPoint, currentPoint);

            Gizmos.color = brokenLinePointColor;

            Gizmos.DrawSphere(currentPoint, gizmosIconSize);

            previousPoint = currentPoint;
        }
    }

    private bool SegmentsAreNull()
    {
        if (SplineSegments == null)
            return true;

        return false;
    }
    #endregion
}

public class SplineBaker : Baker<SplineAuthoring>
{
    public override void Bake(SplineAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        Spline spline = new Spline();
        //NativeArray<SplineSegment> segments = new NativeArray<SplineSegment>(authoring.SplineSegments.Count, Allocator.Persistent);

        //for (int i = 0; i < authoring.SplineSegments.Count; i++)
        //{
        //    SerializeSplineSegment segment = authoring.SplineSegments[i];

        //    segments[i] = new SplineSegment
        //    {
        //        StartPoint = segment.StartPoint,
        //        EndPoint = segment.EndPoint,
        //        FirstInterpolationPoint = segment.FirstInterpolationPoint,
        //        SecondInterpolationPoint = segment.SecondInterpolationPoint,
        //    };
        //}

        spline.Init(authoring.SplineSegments.ToNativeArray(Allocator.Persistent));

        AddComponent(entity, spline);
    }
}

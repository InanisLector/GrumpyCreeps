using UnityEditor.U2D;
using UnityEngine;

public class TestMovemebt : MonoBehaviour
{
    [SerializeField] private SplineContainer container;
    [SerializeField] private float speed;

    private float distance;
    private float travelTime = 0;
    private int segmentIndex;
    private float milage;

    private void Awake()
    {
        GetDistance();
    }

    private void Update()
    {
        if(IsNearPoint())
        {
            ProceedToNextPoint();
            return;
        }

        if (segmentIndex >= container.Spline.Count)
        {
            return;
        }

        travelTime += Time.deltaTime * speed;
        milage += travelTime;

        transform.position = container.GetPointOnSpline(milage);
        transform.rotation = Quaternion.LookRotation(container.GetFirstDerivirate(segmentIndex, milage));
    }
    private void ProceedToNextPoint()
    {
        segmentIndex++;
        travelTime = 0f;

        GetDistance();
    }
    private void GetDistance()
    {
        if (segmentIndex >= container.Spline.Count)
            return;

        distance = container.GetSegmentDistance(segmentIndex);
    }

    private bool IsNearPoint()
    {
        if (segmentIndex >= container.Spline.Count)
            return false;

        float dst = Vector3.Distance(transform.position, container.Spline[segmentIndex].EndPoint.position);

        return dst <= 0.01f;
    }
}

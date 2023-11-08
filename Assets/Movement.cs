using UnityEngine;

public class Movement : MonoBehaviour
{
    //[SerializeField] private Spline spline;
    [SerializeField] private float speed = 3f;

    private float time;

    private void Update()
    {
        time += Time.deltaTime * speed;

        //var newTransform = spline.GetPointAndRotationOnSpline(time);

        //transform.SetPositionAndRotation(newTransform.position, Quaternion.LookRotation(newTransform.rotation));
    }
}

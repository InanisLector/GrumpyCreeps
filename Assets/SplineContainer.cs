using UnityEngine;

public class SplineContainer : MonoBehaviour
{
    public static SplineContainer Instance { get; private set; }

    [SerializeField] private Spline[] splines = new Spline[3];

    private void Awake()
    {
        CreateInstance();
    }

    private void CreateInstance()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.LogWarning("Spline Container Instance already exists!");
    }

    public Spline GetSplineByIndex(int splineIndex)
    {
        if(splineIndex >= splines.Length || splineIndex < 0)
        {
            Debug.LogError("Spline Container: Spline Index was out of bounds!");

            return new Spline();
        }

        return splines[splineIndex];
    }
}

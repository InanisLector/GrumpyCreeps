using System;
using UnityEngine;

public class BuildingBase : MonoBehaviour
{
    [SerializeField, Min(1)] public int size = 1;

    public int Size => size;

    public Action CallOnDestrtoy;

    private void Start()
    {
        
    }

    private void Update()
    {
        
    }

    private void OnDestroy()
    {
        CallOnDestrtoy.Invoke();
    }
}

using Unity.Mathematics;
using UnityEngine;

public static class SHelpers // StaticHelpers
{
    public static float3 GetMouseWorldPos(Vector3 mouseScreenPosition)
    {
        return Camera.main.ScreenToWorldPoint(mouseScreenPosition);
    }
}

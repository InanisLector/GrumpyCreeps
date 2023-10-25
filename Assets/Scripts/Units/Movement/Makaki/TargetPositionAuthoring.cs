using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class TargetPositionAuthoring : MonoBehaviour
{
    public float3 position;
}

public class TargetPositionBaker : Baker<TargetPositionAuthoring>
{
    public override void Bake(TargetPositionAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new TargetPosition
        {
            position = authoring.position,
        });
    }
}
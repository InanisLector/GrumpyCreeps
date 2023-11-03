using Unity.Entities;
using UnityEngine;

public class UnitMovementStatsAuthoring : MonoBehaviour
{
    public float speed;
    public float detectionRadius;
}

public class UnitMovementStatsBaker : Baker<UnitMovementStatsAuthoring>
{
    public override void Bake(UnitMovementStatsAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new UnitMovementStats
        {
            speed = authoring.speed,
            detectionRadius = authoring.detectionRadius,
        });
    }
}
using Unity.Entities;
using UnityEngine;

public class UnitMovementComponentAuthoring : MonoBehaviour
{
    public float Speed;
    public float Time;
    public int SplineIndex;
}
public class UnitMovementComponentBaker : Baker<UnitMovementComponentAuthoring>
{
    public override void Bake(UnitMovementComponentAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new UnitMovementComponent
        {
            Speed = authoring.Speed,
            SplineIndex = authoring.SplineIndex,
            Time = 0,
        });
    }
}

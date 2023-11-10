using Unity.Collections;
using Unity.Entities;
using UnityEngine;

public class SplineContainerAuthoring : MonoBehaviour { }

public class SplineContainerBaker : Baker<SplineContainerAuthoring>
{
    public override void Bake(SplineContainerAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        AddComponent(entity, new SplineContainer
        {
            Splines = new NativeList<Spline>(Allocator.Persistent),
            IsSetUp = false,
        });
    }
}


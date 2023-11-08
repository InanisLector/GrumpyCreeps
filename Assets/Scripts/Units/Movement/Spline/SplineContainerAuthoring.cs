using System.Collections.Generic;
using Unity.Entities;
using UnityEngine;

public class SplineContainerAuthoring : MonoBehaviour
{
    //public List<Entity> entities;
}

public class SplineContainerBaker : Baker<SplineContainerAuthoring>
{
    public override void Bake(SplineContainerAuthoring authoring)
    {
        //Entity entity = GetEntity(TransformUsageFlags.Dynamic);

        //AddComponent(entity, new SplineContainer{});
    }
}


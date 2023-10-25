using Unity.Entities;
using Unity.Mathematics;

public struct TargetComponent : IComponentData
{
    public Entity enemy;
    public float3 position;
}

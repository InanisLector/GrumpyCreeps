using Unity.Entities;

public struct UnitMovementComponent : IComponentData
{
    public float Speed;
    public float Time;
    public int SplineIndex;
}

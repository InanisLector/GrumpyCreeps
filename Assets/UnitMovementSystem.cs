using Unity.Burst;
using Unity.Entities;

[BurstCompile]
public partial struct UnitMovementSystem : ISystem
{
    //RefRW<SplineContainer>

    [BurstCompile]
    private void OnCreate(ref SystemHandle state)
    {
        
    }

    [BurstCompile]
    void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    void OnUpdate(ref SystemState state)
    {

        foreach (RefRO<UnitMovementComponent> movement in SystemAPI.Query<RefRO<UnitMovementComponent>>())
        {

        }
    }
}

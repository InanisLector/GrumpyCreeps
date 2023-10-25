using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Aspects;
using Unity.Transforms;

[BurstCompile]
public readonly partial struct MoveToPositionAspect : IAspect
{
    private readonly Entity entity;

    private readonly RefRW<LocalTransform> localTransform;
    private readonly RefRO<UnitMovementStats> movementStats;
    private readonly RefRW<PhysicsVelocity> velocity;
    private readonly RefRW<TargetPosition> targetPosition;
    private readonly RefRO<TeamComponent> team;

    private readonly ColliderAspect collider;

    [BurstCompile]
    public void Move(float deltaTime)
    {
        float3 direction = math.normalize(targetPosition.ValueRO.position - localTransform.ValueRO.Position);
        velocity.ValueRW.Linear += direction * movementStats.ValueRO.speed * deltaTime;
        localTransform.ValueRW.Rotate(quaternion.LookRotation(targetPosition.ValueRO.position - localTransform.ValueRO.Position, new float3(0f, 1f, 0f)));
        //localTransform.ValueRW.Position += direction * speed.ValueRO.value * deltaTime;
    }

    [BurstCompile]
    public void CheckTargetReach()
    {
        //collider.OverlapBox();

        //if (math.distancesq(localTransform.ValueRO.Position, targetPosition.ValueRO.position) < 50 * 50)
        //{
        //    //targetPosition.ValueRW.position = GetRandomPosition(randomComponent);
        //}
    }
}

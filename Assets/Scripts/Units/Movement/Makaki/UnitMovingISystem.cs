using Unity.Burst;
using Unity.Burst.Intrinsics;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;

[BurstCompile]
public partial struct UnitMovingISystem : ISystem
{
    [BurstCompile]
    public void OnCreate(ref SystemState state) {  }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        float deltaTime = SystemAPI.Time.DeltaTime;

        JobHandle moveJobHandle = new MoveJob
        {
            deltaTime = deltaTime,
        }.ScheduleParallel(state.Dependency);

        moveJobHandle.Complete();

        //JobHandle CheckTargetReachJobHandle = new CheckTargetReachJob { }
        //    .ScheduleParallel(state.Dependency);

        //CheckTargetReachJobHandle.Complete();
    }
}

[BurstCompile]
public partial struct MoveJob : IJobEntity
{
    public float deltaTime;

    [BurstCompile]
    public void Execute(MoveToPositionAspect moveToPositionAspect)
    {
        moveToPositionAspect.Move(deltaTime);
    }
}

[BurstCompile]
public partial struct CheckTargetReachJob : IJobEntity
{
    [BurstCompile]
    public void Execute(MoveToPositionAspect moveToPositionAspect)
    {
        moveToPositionAspect.CheckTargetReach();
    }
}

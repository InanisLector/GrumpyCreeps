using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(FixedStepSimulationSystemGroup))]
[BurstCompile]
public partial struct TowerUnitDetectionSystem : ISystem 
{
    private ComponentLookup<LocalTransform> localTransformLookup;
    private EntityStorageInfoLookup entityStorageInfo;

    [BurstCompile]
    public void OnCreate(ref SystemState state)
    {
        localTransformLookup = state.GetComponentLookup<LocalTransform>(true);
        entityStorageInfo = state.GetEntityStorageInfoLookup();
    }

    [BurstCompile]
    public void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    public void OnUpdate(ref SystemState state)
    {
        var unitCollisionFilters =
            SystemAPI.GetSingleton<DetectionCollisionFiltersComponent>();

        localTransformLookup.Update(ref state);
        entityStorageInfo.Update(ref state);

        EntityQueryBuilder builder = new EntityQueryBuilder(Allocator.Temp).WithAll<PhysicsWorldSingleton>();
        EntityQuery singletonQuery = state.WorldUnmanaged.EntityManager.CreateEntityQuery(builder);
        CollisionWorld collisionWorld = singletonQuery.GetSingleton<PhysicsWorldSingleton>().CollisionWorld;
        singletonQuery.Dispose();

        JobHandle unitDetectionHandle = new UnitDetectionJob
        {
            unitCollisionFilters = unitCollisionFilters,
            collisionWorld = collisionWorld,
        }.ScheduleParallel(state.Dependency);

        unitDetectionHandle.Complete();

        foreach (RefRW<TargetComponent> towerTarget in SystemAPI.Query<RefRW<TargetComponent>>())
        {
            if (towerTarget.ValueRO.enemy == Entity.Null)
                return;

            if (!entityStorageInfo.Exists(towerTarget.ValueRO.enemy))
            {
                towerTarget.ValueRW.enemy = Entity.Null;

                return;
            }
        }
    }
}

[BurstCompile]
public partial struct UnitDetectionJob : IJobEntity
{
    public DetectionCollisionFiltersComponent unitCollisionFilters;
    [ReadOnly] public CollisionWorld collisionWorld;

    [BurstCompile]
    public void Execute(ref TowerRadiusComponent towerRadius, ref LocalTransform localTransform, ref TargetComponent towerTarget)
    {
        NativeList<DistanceHit> unitHits = new NativeList<DistanceHit>(Allocator.Temp);

        collisionWorld.OverlapSphere(localTransform.Position, towerRadius.radius, ref unitHits, unitCollisionFilters.unit);
        
        if (unitHits.Length == 0)
            return;

        float minDistance = float.MaxValue;
        int minDistanceIndex = 0;
        int i = 0;

        foreach (DistanceHit hit in unitHits)
        {
            if (hit.Fraction < 0.1f)
                continue;

            if (minDistance > hit.Fraction)
            {
                minDistance = hit.Fraction;
                minDistanceIndex = i;
            }

            i++;
        }

        towerTarget.enemy = unitHits[minDistanceIndex].Entity;
    }
}

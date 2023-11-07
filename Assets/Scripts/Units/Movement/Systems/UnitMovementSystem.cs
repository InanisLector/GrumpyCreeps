using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[BurstCompile]
[UpdateAfter(typeof(InitializationSystemGroup))]
public partial struct UnitMovementSystem : ISystem
{
    private SplineContainer _splineContainer;

    [BurstCompile]
    private void OnCreate(ref SystemState state)
    {
        _splineContainer = SystemAPI.GetSingleton<SplineContainer>();
    }

    [BurstCompile]
    private void OnDestroy(ref SystemState state) { }

    [BurstCompile]
    private void OnUpdate(ref SystemState state)
    {
        foreach ((RefRW<UnitMovementComponent> movement, RefRW<LocalTransform> transform) in SystemAPI.Query<RefRW<UnitMovementComponent>, RefRW<LocalTransform>>())
        {
            var spline = _splineContainer.GetSplineByIndex(movement.ValueRO.SplineIndex);

            var newTransform = spline.GetPointAndRotationOnSpline(movement.ValueRO.Time);

            transform.ValueRW.Position = newTransform.position;
            transform.ValueRW.Rotation = Quaternion.LookRotation(newTransform.rotation);

            movement.ValueRW.Time += SystemAPI.Time.DeltaTime;
        }
    }
}

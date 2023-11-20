using System;
using System.Diagnostics.Contracts;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class TowerPlacementSystem : SystemBase
{
    private Camera camera;
    private PlayerControls controls;
    private CollisionFilter raycastInputFilter;

    protected override void OnCreate()
    {
        base.OnCreate();

        camera = Camera.main;
        controls = new PlayerControls();

        raycastInputFilter = new CollisionFilter()
        {
            BelongsTo = ~0u,
            CollidesWith = ~0u,
        };
    }

    protected override void OnStartRunning()
    {
        base.OnStartRunning();

        controls.Towers.PlaceTower.performed += PlaceTower;
        controls.Towers.Enable();
    }
    protected override void OnUpdate()
    {
        
    }

    protected override void OnStopRunning()
    {
        base.OnStopRunning();

        controls.Towers.PlaceTower.performed -= PlaceTower;
        controls.Towers.Disable();
    }

    private void PlaceTower(InputAction.CallbackContext callback)
    {
        RefRW<Grid> grid = SystemAPI.GetSingletonRW<Grid>();

        Vector2 mousePos = controls.Towers.MousePosition.ReadValue<Vector2>();
        float3 mouseWorldPos = SHelpers.GetMouseWorldPos(mousePos);

        EntityQueryBuilder builder = new EntityQueryBuilder(Allocator.Temp).WithAll<PhysicsWorldSingleton>();
        EntityQuery singletonQuery = EntityManager.CreateEntityQuery(builder);
        CollisionWorld collisionWorld = singletonQuery.GetSingleton<PhysicsWorldSingleton>().CollisionWorld;

        Unity.Physics.RaycastHit hit;
        RaycastInput raycastInput = new RaycastInput
        {
            Filter = raycastInputFilter,
            Start = mouseWorldPos,
            End = mouseWorldPos + ((float3)camera.transform.forward * 100),
        };
        collisionWorld.CastRay(raycastInput, out hit);


        Debug.Log(grid.ValueRW.GetXY(hit.Position));
        //Debug.Log($"{grid.ValueRW.GetXY(hit.Position)} || {mouseWorldPos} || {(mouseWorldPos + (float3)camera.transform.forward * 100)} || {hit.Position}");
    }
}

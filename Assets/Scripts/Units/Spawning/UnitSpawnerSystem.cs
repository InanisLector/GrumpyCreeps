using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct UnitSpawnerSystem : ISystem
{
    public void OnCreate(ref SystemState state) 
    {
        //state.World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>()
    }

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state)
    {
        EntityCommandBuffer entityCommandBuffer = 
            SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.World.Unmanaged);

        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (UnitSpawnerAspect unitSpawner in SystemAPI.Query<UnitSpawnerAspect>())
        {
            unitSpawner.UpdateSpawnTimer(deltaTime);
            unitSpawner.Spawn(entityCommandBuffer);
        }
    }
}

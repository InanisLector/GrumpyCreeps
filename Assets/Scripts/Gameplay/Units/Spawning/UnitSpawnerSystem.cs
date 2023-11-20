using GC.Spline;
using Unity.Burst;
using Unity.Entities;

[BurstCompile]
[UpdateInGroup(typeof(SimulationSystemGroup))]
public partial struct UnitSpawnerSystem : ISystem
{
    private BufferLookup<UnitDeckIndex> unitDeckIndexLookup;
    public void OnCreate(ref SystemState state) 
    {
        //state.World.GetOrCreateSystem<BeginInitializationEntityCommandBufferSystem>()
        unitDeckIndexLookup = state.GetBufferLookup<UnitDeckIndex>(true);
    }

    public void OnDestroy(ref SystemState state) { }

    public void OnUpdate(ref SystemState state)
    {
        // get singleton of unit wave

        EntityCommandBuffer entityCommandBuffer = 
            SystemAPI.GetSingleton<BeginSimulationEntityCommandBufferSystem.Singleton>().CreateCommandBuffer(state.World.Unmanaged);

        SplineContainer splineContainer = SystemAPI.GetSingleton<SplineContainer>();
        UnitDeckComponent unitDeck = SystemAPI.GetSingleton<UnitDeckComponent>();

        unitDeckIndexLookup.Update(ref state);

        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (UnitSpawnerAspect unitSpawner in SystemAPI.Query<UnitSpawnerAspect>())
        {
            unitSpawner.UpdateSpawnTimer(deltaTime);
            unitSpawner.Spawn(entityCommandBuffer, splineContainer, unitDeck, unitDeckIndexLookup);
        }
    }
}

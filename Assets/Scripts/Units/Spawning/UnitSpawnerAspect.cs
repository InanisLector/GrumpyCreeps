using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[BurstCompile]
public readonly partial struct UnitSpawnerAspect : IAspect
{
    private readonly Entity entity;

    private readonly RefRW<LocalTransform> localTransform;
    private readonly RefRO<UnitDeckComponent> unitDeck;
    private readonly RefRW<UnitSpawnTimeComponent> spawnTime;
    private readonly RefRO<TeamComponent> team;

    [BurstCompile]
    public void UpdateSpawnTimer(float deltaTime)
    {
        spawnTime.ValueRW.spawnTimer += deltaTime;

        if (spawnTime.ValueRO.spawnTimer < spawnTime.ValueRO.spawnInterval)
            return;

        spawnTime.ValueRW.spawnTimer = 0;
        spawnTime.ValueRW.canSpawn = true;
    }

    [BurstCompile]
    public void Spawn(EntityCommandBuffer entityCommandBuffer)
    {
        if (!spawnTime.ValueRW.canSpawn)
            return;

        Entity entity = entityCommandBuffer.Instantiate(unitDeck.ValueRO.unit);
        entityCommandBuffer.SetComponent(entity, localTransform.ValueRO);
        entityCommandBuffer.SetComponent(entity, team.ValueRO);

        spawnTime.ValueRW.canSpawn = false;
    }
}

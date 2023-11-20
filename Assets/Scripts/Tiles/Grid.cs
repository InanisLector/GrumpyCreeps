using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

[ChunkSerializable]
public struct Grid : IComponentData
{
    private NativeArray<NativeArray<Entity>> grid;
    private const float CELL_SIZE = 2.5f;

    private int width;
    private int height;

    private float3 worldOffset;

    public void Setup(int width, int height, float3 worldOffset)
    {
        this.width = width;
        this.height = height;
        this.worldOffset = worldOffset;

        grid = new NativeArray<NativeArray<Entity>>(width, Allocator.Persistent);

        for (int x = 0; x < width; x++)
        {
            grid[x] = new NativeArray<Entity>(height, Allocator.Persistent);
            var temp = grid[x];

            for (int y = 0; y < height; y++)
            {
                temp[y] = Entity.Null;
            }

            grid[x] = temp;
        }
    }

    public float3 GetWorldPosition(int x, int y)
    {
        return new float3(x, 0, y) * CELL_SIZE + worldOffset;
    }

    public int2 GetXY(float3 worldPos)
    {
        int2 xy = new int2(0, 0);

        xy.x = (int)math.floor((worldPos.x - worldOffset.x) / CELL_SIZE);
        xy.y = (int)math.floor((worldPos.z - worldOffset.z) / CELL_SIZE);

        return xy;
    }

    public void SetEntity(int x, int y, Entity entity)
    {
        if (!(x >= 0 && y >= 0 && x < width && y < height))
            return;

        var temp = grid[x];
        temp[y] = entity;
        grid[x] = temp;
    }

    public void SetEntity(float3 worldPos, Entity entity)
    {
        int2 tilePos = GetXY(worldPos);

        var temp = grid[tilePos.x];
        temp[tilePos.y] = entity;
        grid[tilePos.x] = temp;
    }

    public Entity GetValue(int x, int y)
    {
        return grid[x][y];
    }

    public Entity GetValue(float3 worldPos)
    {
        int2 tilePos = GetXY(worldPos);

        return grid[tilePos.x][tilePos.y];
    }
}

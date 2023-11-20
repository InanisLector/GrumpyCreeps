using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class GridAuthoring : MonoBehaviour
{
    public int width = 30;
    public int height = 20;

    public Vector2 worldOffset = new Vector2(5, 5);
}

public class GridBaker : Baker<GridAuthoring>
{
    public override void Bake(GridAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);

        float3 worldOffset = new float3(authoring.worldOffset.x, 0, authoring.worldOffset.y);

        Grid grid = new Grid();
        grid.Setup(authoring.width, authoring.height, worldOffset);
        AddComponent(entity, grid);
    }
}

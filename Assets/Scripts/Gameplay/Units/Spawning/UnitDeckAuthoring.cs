using Unity.Entities;
using UnityEngine;

public class UnitDeckAuthoring : MonoBehaviour
{
    public GameObject playerPrefab;
}

public class UnitDeckBaker : Baker<UnitDeckAuthoring>
{
    public override void Bake(UnitDeckAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new UnitDeckComponent
        {
            unit = GetEntity(authoring.playerPrefab, TransformUsageFlags.Dynamic),
        });
    }
}
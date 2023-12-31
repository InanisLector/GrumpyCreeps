using Unity.Entities;
using Unity.Physics.Authoring;
using UnityEngine;

[RequireComponent(typeof(PhysicsShapeAuthoring))]
public class DamageAuthoring : MonoBehaviour
{
    public uint damage;
    public ushort pierce;
}

public class DamageBaker : Baker<DamageAuthoring>
{
    public override void Bake(DamageAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.None);
        AddComponent(entity, new DamageComponent
        {
            damage = authoring.damage,
            pierce = authoring.pierce,
        });

        AddBuffer<HitList>(entity);
    }
}

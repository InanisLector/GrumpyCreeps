using Unity.Entities;
using UnityEngine;

public class TowerShooterAuthoring : MonoBehaviour
{
    public GameObject projectile;

    public byte projectileCount;
    public float attackTime;

    //public NativeList<ProjectileModifiers> projectileModifiers;
}

public class TowerShooterBaker : Baker<TowerShooterAuthoring>
{
    public override void Bake(TowerShooterAuthoring authoring)
    {
        Entity entity = GetEntity(TransformUsageFlags.Dynamic);
        AddComponent(entity, new TowerShooterComponent
        {
            projectile = GetEntity(authoring.projectile, TransformUsageFlags.Dynamic),
            projectileCount = authoring.projectileCount,
            attackTime = authoring.attackTime,
            attackTimer = authoring.attackTime,
        });
    }
}

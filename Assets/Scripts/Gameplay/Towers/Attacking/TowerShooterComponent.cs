using Unity.Entities;

public struct TowerShooterComponent : IComponentData
{
    public Entity projectile;

    public byte projectileCount;

    public float attackTime;
    public float attackTimer;

    //public NativeList<ProjectileModifiers> projectileModifiers;
}

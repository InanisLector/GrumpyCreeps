using Unity.Entities;

public struct DamageComponent : IComponentData
{
    public uint damage;
    public byte pierce;
    //public DamageType damageType;
}

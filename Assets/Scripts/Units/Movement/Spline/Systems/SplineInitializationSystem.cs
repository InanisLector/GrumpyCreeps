using Unity.Entities;

[UpdateBefore(typeof(UnitMovementSystem))]
public partial class SplineInitializationSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();
        
    }
    protected override void OnUpdate()
    {
        RefRW<SplineContainer> splineContainer = SystemAPI.GetSingletonRW<SplineContainer>();

        if (splineContainer.ValueRW.isSetUp)
            return;

        foreach (Spline spline in SystemAPI.Query<Spline>())
        {
            splineContainer.ValueRW.Splines.Add(spline);
        }

        splineContainer.ValueRW.isSetUp = true;
    }
}

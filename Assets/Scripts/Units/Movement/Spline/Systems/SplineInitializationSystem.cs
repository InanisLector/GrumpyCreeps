using Unity.Entities;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial class SplineInitializationSystem : SystemBase
{
    protected override void OnCreate()
    {
        base.OnCreate();

        var container = SystemAPI.GetSingleton<SplineContainer>();

        foreach(Spline spline in SystemAPI.Query<Spline>())
        {
            container.Splines.Add(spline);
        }
    }
    protected override void OnUpdate()
    {

    }
}

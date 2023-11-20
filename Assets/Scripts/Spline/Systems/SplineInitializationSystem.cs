using GC.Units.Movement;
using Unity.Entities;

namespace GC.Spline
{
    [UpdateBefore(typeof(UnitMovementSystem))]
    public partial class SplineInitializationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            RefRW<SplineContainer> splineContainer = SystemAPI.GetSingletonRW<SplineContainer>();

            if (splineContainer.ValueRW.IsSetUp)
                return;

            foreach (Spline spline in SystemAPI.Query<Spline>())
            {
                splineContainer.ValueRW.Splines.Add(spline);
            }

            splineContainer.ValueRW.IsSetUp = true;
        }
    }
}

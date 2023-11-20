using GC.Units.Movement;
using Unity.Entities;
using UnityEngine.SceneManagement;
using Unity.Collections;

namespace GC.Spline
{
    [UpdateBefore(typeof(UnitMovementSystem))]
    public partial class SplineInitializationSystem : SystemBase
    {
        protected override void OnUpdate()
        {
            if (ExcludedMaps.SystemExclusion.Contains(SceneManager.GetActiveScene().name))
                return;

            RefRW<SplineContainer> splineContainer;

            if (!SystemAPI.TryGetSingletonRW(out splineContainer))
                return;

            if (splineContainer.ValueRW.IsSetUp)
                return;

            InitSplineContainer(splineContainer);
        }

        private void InitSplineContainer(RefRW<SplineContainer> splineContainer)
        {
            NativeList<Spline> splines = new NativeList<Spline>(Allocator.Persistent);

            foreach (Spline spline in SystemAPI.Query<Spline>())
            {
                splines.Add(spline);
            }

            splineContainer.ValueRW.Splines = splines.ToArray(Allocator.Persistent);

            splineContainer.ValueRW.IsSetUp = true;
        }
    }
}

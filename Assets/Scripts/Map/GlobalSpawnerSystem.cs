using Unity.Entities;
using GC.Spline;
using Unity.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[UpdateInGroup(typeof(InitializationSystemGroup))]
public partial class GlobalSpawnerSystem : SystemBase
{
    public static MapPrefab MapPrefab;

    public static bool FinishedCreatingMap = false;

    protected override void OnCreate()
    {
        base.OnCreate();
    }

    protected override void OnUpdate()
    {
        bool isExcludedScene = ExcludedMaps.SystemExclusion.Contains(SceneManager.GetActiveScene().name);

        if (isExcludedScene)
            return;

        if (FinishedCreatingMap)
            return;

        CreateLevel();

        FinishedCreatingMap = true;
    }

    private void CreateLevel()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var splines = MapPrefab.SplineSegments.GetComponentsInChildren<SplineAuthoring>();

        foreach (var spline in splines)
        {
            CreateSpline(entityManager, spline.SplineSegments);
        }

        CreateSplineContainer(EntityManager);
    }

    private void CreateSpline(EntityManager entityManager, List<SplineSegment> splineSegments)
    {
        EntityArchetype archetype = entityManager.CreateArchetype(typeof(Spline));

        Entity entity = entityManager.CreateEntity(archetype);

        Spline spline = new Spline();

        spline.Init(new NativeArray<SplineSegment>(splineSegments.ToArray(), Allocator.Persistent));

        entityManager.SetComponentData(entity, spline);
    }

    private void CreateSplineContainer(EntityManager entityManager)
    {
        EntityArchetype archetype = entityManager.CreateArchetype(typeof(SplineContainer));

        Entity entity = entityManager.CreateEntity(archetype);

        entityManager.SetComponentData(entity, new SplineContainer());
    }
}

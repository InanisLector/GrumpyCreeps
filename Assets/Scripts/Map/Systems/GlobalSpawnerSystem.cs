using Unity.Entities;
using GC.SplineMovement;
using Unity.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using System;

namespace GC.Map
{
    [UpdateInGroup(typeof(InitializationSystemGroup))]
    public partial class GlobalSpawnerSystem : SystemBase
    {
        public static MapPrefab MapPrefab;

        public static bool FinishedCreatingMap = false;

        private EntityManager entityManager;

        protected override void OnCreate()
        {
            base.OnCreate();

            entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
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
            CreateSplines();

            CreateSplineContainer();
        }

        private void CreateSplines()
        {
            var splines = MapPrefab.Splines.GetComponentsInChildren<SplineAuthoring>();

            foreach (var spline in splines)
            {
                CreateSpline(spline.SplineSegments);
            }
        }

        private void CreateSpline(List<SplineSegment> splineSegments)
        {
            Spline spline = new Spline();

            spline.Init(new NativeArray<SplineSegment>(splineSegments.ToArray(), Allocator.Persistent));

            Entity entity = CreateEntityFromType(typeof(Spline));

            entityManager.SetComponentData(entity, spline);
        }

        private void CreateSplineContainer()
        {
            Entity entity = CreateEntityFromType(typeof(SplineContainer));

            entityManager.SetComponentData(entity, new SplineContainer());
        }

        private void CreateGrid()
        {

        }

        private Entity CreateEntityFromType(Type type)
        {
            EntityArchetype archetype = entityManager.CreateArchetype(type);

            return entityManager.CreateEntity(archetype);
        }
    }
}

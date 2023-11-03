using Unity.Entities;

public partial class UnitMovementSystemBase : SystemBase
{
    protected override void OnUpdate()
    {
        //RefRW<RandomComponent> randomComponent = SystemAPI.GetSingletonRW<RandomComponent>();
        //float deltaTime = SystemAPI.Time.DeltaTime;

        //foreach (MoveToPositionAspect moveToPositionAspect in SystemAPI.Query<MoveToPositionAspect>())
        //{
        //    moveToPositionAspect.Move(deltaTime, randomComponent);
        //}
    }
}



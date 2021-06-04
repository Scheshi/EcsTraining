using ECSFPS.ECS.Components;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


namespace Assets.Scripts.ECS.Systems
{
    public class NetworkPlayerMoveSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return Entities.WithAll<NetworkPlayerDataComponent>().ForEach(
                (ref NetworkInputComponent input, ref Translation transform, ref Rotation rotation) =>
                {
                    transform.Value = input.position;
                    rotation.Value = new quaternion(input.rotation);
                }).Schedule(inputDeps);
        }
    }
}
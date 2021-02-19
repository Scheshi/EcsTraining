using ECSFPS.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;


namespace ECSFPS.ECS.Systems
{
    [BurstCompile]
    public class StabilishedXZSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return Entities.WithAll<FreezeXZRotation>().ForEach((ref PhysicsMass mass) =>
            {
                mass.InverseInertia.xz = new float2(0.0f, 0.0f);
            }).Schedule(inputDeps);
        }
    }
}
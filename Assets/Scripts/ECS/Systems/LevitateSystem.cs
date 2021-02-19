using Assets.Scripts.Services;
using ECSFPS.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;


namespace ECSFPS.ECS.Systems
{
    [BurstCompile]
    public class LevitateSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return Entities.ForEach((ref PlayerDataComponent player, ref InputComponent input, ref PhysicsVelocity velocity, ref Rotation rotation,
                ref Translation translation, ref PhysicsMass mass) =>
            {
                var point = new float3(0.0f, 1.0f, 0.0f);
                if (input.JumpValue)
                {
                    /*VelocityWorker.ApplyImpulse(
                        ref velocity, mass, translation, rotation, 
                        player.JumpForce * input.deltaTime * point, point);*/
                    velocity.Linear.y += player.JumpForce;
                }
            }).Schedule(inputDeps);
        }
    }
}

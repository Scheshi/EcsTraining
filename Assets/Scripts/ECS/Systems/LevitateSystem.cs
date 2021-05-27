using ECSFPS.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Physics;


namespace ECSFPS.ECS.Systems
{
    /*[BurstCompile]
    public class LevitateSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return Entities.ForEach((ref PlayerDataComponent player, ref InputComponent input, ref PhysicsVelocity velocity) =>
            {
                if (input.JumpValue)
                {
                    /*VelocityWorker.ApplyImpulse(
                        ref velocity, mass, translation, rotation, 
                        player.JumpForce * input.deltaTime * point, point);#1#
                    velocity.Linear.y = player.JumpForce;
                }
            }).Schedule(inputDeps);
        }
    }*/
}

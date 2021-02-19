using Assets.Scripts.Services;
using ECSFPS.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;


[BurstCompile]
public class PlayerMoveSystem : JobComponentSystem
{
    protected override JobHandle OnUpdate(JobHandle inputDeps)
    {
        return Entities.ForEach((ref PhysicsVelocity velocity, ref PhysicsMass mass, ref LocalToWorld transform, ref InputComponent input, ref PlayerDataComponent player) =>
        {
            if (math.all(input.MovementValue == float3.zero))
            {
                mass.InverseInertia = float3.zero;
            }
            else
            {
                float3 direction =
                    (input.MovementValue.z * transform.Forward + input.MovementValue.x * transform.Right) *
                    input.deltaTime * player.Speed;

                velocity.Linear = new float3(direction.x, 0.0f, direction.z);
                //velocity.Angular = 

                /*VelocityWorker.ApplyImpulse(ref velocity, mass, translation, rotation,
                    input.MovementValue * player.Speed * input.deltaTime, new float3(0.0f, 1.0f, 0.0f));*/
            }
        }).Schedule(inputDeps);
    }
}

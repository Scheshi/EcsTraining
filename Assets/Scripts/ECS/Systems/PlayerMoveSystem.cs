
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
        return Entities.ForEach((ref PhysicsVelocity velocity, ref PhysicsMass mass, ref LocalToWorld transform, ref Rotation rotation, ref InputComponent input, ref PlayerDataComponent player) =>
        {
            if (math.all(input.MovementValue == float3.zero))
            {
                mass.InverseInertia = float3.zero;
            }
            else
            {
                float3 forwardVector = math.mul(rotation.Value, input.MovementValue) * input.deltaTime * player.Speed;
                /*float3 direction =
                    (input.MovementValue.z * transform.Forward + input.MovementValue.x * transform.Right) *
                    input.deltaTime * player.Speed;*/

                velocity.Linear = new float3(forwardVector.x, velocity.Linear.y, forwardVector.z);
            }
        }).Schedule(inputDeps);
    }
}

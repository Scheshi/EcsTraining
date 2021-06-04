using Assets.Scripts.Services.Networking;
using ECSFPS.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;
using UnityEngine;


namespace ECSFPS.ECS.Systems
{
    [BurstCompile]
    public class PlayerRotationSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return Entities.ForEach((ref InputComponent input, ref PhysicsVelocity velocity, ref Rotation rotation) =>
            {
                velocity.Angular = new float3(0.0f, input.XMousePosition * 1000 * input.deltaTime, 0.0f);
                if (rotation.Value.value.y >= 0.98) rotation.Value.value.y = -1;
                if (rotation.Value.value.y <= -0.98) rotation.Value.value.y = 1;
            }).Schedule(inputDeps);
        }
    }
}
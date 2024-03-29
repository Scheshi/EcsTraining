﻿using ECSFPS.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;


namespace Assets.Scripts.ECS.Systems
{
    [BurstCompile]
    public class PlayerMoveSystem : JobComponentSystem
    {
        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return Entities.ForEach((ref PlayerDataComponent player, ref LocalToWorld transform, ref PhysicsVelocity velocity, ref InputComponent input, ref Rotation rot) =>
            {
                float3 dir = (transform.Forward * input.MovementValue.z + transform.Right * input.MovementValue.x) * player.Speed;
                velocity.Linear += new float3(dir.x, 0.0f, dir.z);
            }).Schedule(inputDeps);
        }
    }
}

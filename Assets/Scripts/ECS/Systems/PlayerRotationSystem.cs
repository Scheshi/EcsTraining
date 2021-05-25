using ECSFPS.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Transforms;


namespace ECSFPS.ECS.Systems
{
    [BurstCompile]
    public class PlayerRotationSystem : JobComponentSystem
    {
        private float _yRot = 0;

        protected override JobHandle OnUpdate(JobHandle inputDeps)
        {
            return Entities.ForEach((ref InputComponent input, ref Rotation rotation) =>
            {
                //var X = rotation.Value.value.y + input.YMousePosition * 5;
                rotation.Value  = new quaternion(rotation.Value.value.x, input.XRot, rotation.Value.value.z, rotation.Value.value.w);
                if (rotation.Value.value.y > 360) rotation.Value.value.y -= 360;
            }).Schedule(inputDeps);
        }
    }
}
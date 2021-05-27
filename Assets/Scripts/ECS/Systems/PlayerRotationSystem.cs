using ECSFPS.ECS.Components;
using Unity.Burst;
using Unity.Entities;
using Unity.Jobs;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Transforms;


namespace ECSFPS.ECS.Systems
{
    /*public class PlayerRotationSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref InputComponent input, ref PhysicsVelocity velocity) =>
            {
                //var X = rotation.Value.value.y + input.YMousePosition * 5;
                /*rotation.Value  = new quaternion(rotation.Value.value.x, input.XRot, rotation.Value.value.z, rotation.Value.value.w);
                if (rotation.Value.value.y > 360) rotation.Value.value.y -= 360;#1#
                velocity.Angular = new float3(0.0f, input.MovementValue.x * 90 * input.deltaTime, 0.0f);
            });
        }
    }*/
}
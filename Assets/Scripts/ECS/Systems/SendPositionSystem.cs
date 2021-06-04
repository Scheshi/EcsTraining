using Assets.Scripts.Services.Networking;
using ECSFPS.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace Assets.Scripts.ECS.Systems
{
    public class SendPositionSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.ForEach((ref InputComponent input, ref Translation transform, ref Rotation rotation) =>
            {
                if (input.MovementValue.x > float3.zero.x || input.MovementValue.y > float3.zero.y ||
                    input.MovementValue.z > float3.zero.z)
                {
                    NetworkManager.Instance.Handler.SendMessage(transform.Value, Tags.SEND_POSITION, () => { });
                }
                if (input.XMousePosition > 0)
                {
                    NetworkManager.Instance.Handler.SendMessage(rotation.Value.value, Tags.SEND_ROTATION, () => {});
                }
            });
        }
    }
}
using ECSFPS.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.ECS.Systems
{
    public class InputSystem : ComponentSystem
    {
        protected override void OnUpdate()
        {
            Entities.WithAll<InputComponent>().ForEach((ref InputComponent input) =>
            {
                input = new InputComponent()
                {
                    MovementValue = new float3(Input.GetAxis("Horizontal"), 0.0f, Input.GetAxis("Vertical")),
                    JumpValue = Input.GetButton("Jump"),
                    FireValue = Input.GetButton("Fire1")
                };
            });
        }
    }
}
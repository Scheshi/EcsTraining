using ECSFPS.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;


namespace ECSFPS.ECS.Systems
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
                    FireValue = Input.GetButton("Fire1"),
                    XMousePosition = Input.GetAxis("Mouse X"),
                    YMousePosition = Input.GetAxis("Mouse Y"),
                    deltaTime = Time.DeltaTime,
                    XRot = input.XRot,
                    YRot = input.YRot
                };
                input.XRot += Input.GetAxis("Mouse X") * 90 * Time.DeltaTime;
            });
        }
    }
}
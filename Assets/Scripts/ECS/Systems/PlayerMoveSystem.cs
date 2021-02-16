using ECSFPS.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;


public class PlayerMoveSystem : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.WithAll<Translation, PlayerMarkerComponent, Rotation>()
            .ForEach((ref Translation translation, ref Rotation rotation, ref InputComponent input) =>
        {
            translation.Value += input.MovementValue;
        });
    }
}

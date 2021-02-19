using Unity.Entities;
using Unity.Mathematics;


namespace ECSFPS.ECS.Components
{
    public struct PlayerDataComponent : IComponentData
    {
        public float Speed;
        public float JumpForce;
    }
}

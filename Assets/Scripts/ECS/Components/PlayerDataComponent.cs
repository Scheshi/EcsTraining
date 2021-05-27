using Unity.Entities;

 
namespace ECSFPS.ECS.Components
{
    [GenerateAuthoringComponent]
    public struct PlayerDataComponent : IComponentData
    {
        public float Speed;
        public float JumpForce;
    }
}

using Unity.Entities;

namespace ECSFPS.ECS.Components
{
    public struct NetworkPlayerDataComponent : IComponentData
    {
        public ushort Id;
    }
}
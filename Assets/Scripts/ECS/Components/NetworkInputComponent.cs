using Unity.Entities;
using Unity.Mathematics;


namespace ECSFPS.ECS.Components
{
    public struct NetworkInputComponent : IComponentData
    {
        public float3 position;
        public float4 rotation;
    }
}
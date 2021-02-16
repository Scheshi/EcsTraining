using Unity.Entities;
using Unity.Mathematics;

namespace ECSFPS.ECS.Components
{
    public struct InputComponent : IComponentData
    {
        public float3 MovementValue;
        public bool JumpValue;
        public bool FireValue;
    }
}
using Unity.Entities;
using Unity.Mathematics;


namespace ECSFPS.ECS.Components
{
    [GenerateAuthoringComponent]
    public struct InputComponent : IComponentData
    {
        public float3 MovementValue;
        public bool JumpValue;
        public bool FireValue;
        public float deltaTime;
        public float XMousePosition;
        public float YMousePosition;
        public float YRot;
        public float XRot;
    }
}
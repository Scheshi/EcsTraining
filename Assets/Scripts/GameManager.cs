using ECSFPS.ECS.Components;
using ECSFPS.Services;
using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using Material = UnityEngine.Material;


public class GameManager : MonoBehaviour
{
    [SerializeField] private Material _testMaterial;
    [SerializeField] private Mesh _testMesh;

    private EntityFabric _entityFabric = new EntityFabric();

    private void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
        var mesh = new RenderMesh()
        {
            material = _testMaterial,
            mesh = _testMesh
        };
        
        Entity player = _entityFabric.ConstructDinamycPhisic
            (entityManager, mesh, 0.5f, float3.zero, quaternion.identity, 1.0f);
        
        entityManager.AddComponentData(player, new InputComponent()
        {
            FireValue = false,
            JumpValue = false,
            MovementValue = float3.zero
        });
        entityManager.AddComponentData(player, new PlayerDataComponent()
        {
            Speed = 30.0f,
            JumpForce = 3.0f
        });
    }
}

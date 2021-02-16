using ECSFPS.ECS.Components;
using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Rendering;



public class GameManager : MonoBehaviour
{
    [SerializeField] private Material _testMaterial;
    [SerializeField] private Mesh _testMesh;
    
    void Start()
    {
        var entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        var testEntity = entityManager.CreateEntity(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld),
            typeof(PlayerMarkerComponent),
            typeof(InputComponent)
        );

        entityManager.AddSharedComponentData(testEntity, new RenderMesh()
        {
            material = _testMaterial,
            mesh = _testMesh
        });


    }
}

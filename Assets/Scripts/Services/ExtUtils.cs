using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using RaycastHit = Unity.Physics.RaycastHit;

namespace Assets.Scripts.Services
{
    public static class ExtUtils
    {
        public static Entity Raycast(float3 fromPosition, float3 toPosition)
        {
            BuildPhysicsWorld buildPhysics =
                World.DefaultGameObjectInjectionWorld.GetExistingSystem<BuildPhysicsWorld>();
            CollisionWorld collisionWorld = buildPhysics.PhysicsWorld.CollisionWorld;

            RaycastInput raycastInput = new RaycastInput
            {
                Start = fromPosition,
                End = toPosition,
                Filter = new CollisionFilter()
                {
                    BelongsTo = ~0u,
                    CollidesWith = ~0u,
                    GroupIndex = 0
                }
            };

            RaycastHit hit = new RaycastHit();
            if (collisionWorld.CastRay(raycastInput, out hit))
            {
                return buildPhysics.PhysicsWorld.Bodies[hit.RigidBodyIndex].Entity;
            }

            return Entity.Null;
        }

        public static Entity InstantiateEntityFromPrefab(this GameObject obj, BlobAssetStore store)
        {
            GameObjectConversionSettings settings  = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
                var go = Object.Instantiate(obj);
                var entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(go, settings);
                var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
                manager.SetComponentData(entity, new Translation(){ Value = go.transform.position});
                manager.SetComponentData(entity, new Rotation(){ Value = go.transform.rotation});
                Object.Destroy(go);
                return entity;
        }
    }
}

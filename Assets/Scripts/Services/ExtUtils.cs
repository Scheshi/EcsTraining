using System;
using System.Threading;
using ECSFPS.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Transforms;
using UnityEngine;
using Object = UnityEngine.Object;
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

        public static Entity InstantiateEntity(this GameObject obj, BlobAssetStore store)
        {
            GameObjectConversionSettings settings  = GameObjectConversionSettings.FromWorld(World.DefaultGameObjectInjectionWorld, store);
            var entity = GameObjectConversionUtility.ConvertGameObjectHierarchy(obj, settings);
            var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
            manager.SetComponentData(entity, new Translation(){ Value = obj.transform.position});
            manager.SetComponentData(entity, new Rotation(){ Value = obj.transform.rotation});
            return manager.Instantiate(entity);
        }

        public static Entity InstantiatePlayerFromPrefab(this GameObject obj, BlobAssetStore store, bool isLocalPlayer, string name = null, ushort id = 0)
        {
            var entity = obj.InstantiateEntity(store);
                var manager = World.DefaultGameObjectInjectionWorld.EntityManager;
                if (isLocalPlayer)
                {
                    manager.SetComponentData(entity, new InputComponent());
                }
                else
                {
                    manager.SetComponentData(entity, new NetworkInputComponent());
                    manager.SetComponentData(entity, new NetworkPlayerDataComponent() {Id = id});
                }

                return entity;
        }
    }
}

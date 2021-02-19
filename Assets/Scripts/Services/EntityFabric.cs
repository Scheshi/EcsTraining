using ECSFPS.ECS.Components;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Rendering;
using Unity.Transforms;


namespace ECSFPS.Services
{
    public class EntityFabric
    {
        public Entity Construct(EntityManager entityManager,
            RenderMesh displayMesh, float3 position, quaternion orientation, BlobAssetReference<Collider> collider,
            float3 linearVelocity, float3 angularVelocity, float mass, bool isDynamic)
        {
            ComponentType[] componentTypes = new ComponentType[isDynamic ? 10 : 7];

            componentTypes[0] = typeof(RenderMesh);
            componentTypes[1] = typeof(RenderBounds);
            componentTypes[2] = typeof(Translation);
            componentTypes[3] = typeof(Rotation);
            componentTypes[4] = typeof(LocalToWorld);
            componentTypes[5] = typeof(PhysicsCollider);
            componentTypes[6] = typeof(FreezeXZRotation);
            if (isDynamic)
            {
                componentTypes[7] = typeof(PhysicsVelocity);
                componentTypes[8] = typeof(PhysicsMass);
                componentTypes[9] = typeof(PhysicsDamping);
            }
            Entity entity = entityManager.CreateEntity(componentTypes);

            var physCollider = new PhysicsCollider() {Value = collider};
            
            entityManager.SetSharedComponentData(entity, displayMesh);
            entityManager.SetComponentData(entity, new RenderBounds {Value = displayMesh.mesh.bounds.ToAABB()});

            entityManager.SetComponentData(entity, new Translation {Value = position});
            entityManager.SetComponentData(entity, new Rotation {Value = orientation});

            entityManager.SetComponentData(entity, physCollider);

            if (isDynamic)
            {
                entityManager.SetComponentData(entity,
                    PhysicsMass.CreateDynamic(physCollider.MassProperties, mass));
                // Calculate the angular velocity in local space from rotation and world angular velocity
                float3 angularVelocityLocal =
                    math.mul(math.inverse(physCollider.MassProperties.MassDistribution.Transform.rot),
                        angularVelocity);
                entityManager.SetComponentData(entity, new PhysicsVelocity()
                {
                    Linear = linearVelocity,
                    Angular = angularVelocityLocal
                });
                entityManager.SetComponentData(entity, new PhysicsDamping()
                {
                    Linear = 0.1f,
                    Angular = 0.5f
                });
            }

            return entity;
        }

        public Entity ConstructDinamycPhisic(EntityManager manager, RenderMesh displayMesh, float radius,
            float3 position, quaternion orientation, float mass)
        {
            // Sphere with default filter and material. Add to Create() call if you want non default:
            BlobAssetReference<Unity.Physics.Collider> spCollider = Unity.Physics.CapsuleCollider.Create(
                new CapsuleGeometry()
                {
                    Radius = radius,
                    Vertex0 = new float3(position.x, position.y + 1.0f, position.z),
                    Vertex1 = new float3(position.x, position.y - 1.0f, position.z)
                });
            return Construct(manager, displayMesh, position, orientation, spCollider, float3.zero, float3.zero, mass,
                true);

        }
    }
}
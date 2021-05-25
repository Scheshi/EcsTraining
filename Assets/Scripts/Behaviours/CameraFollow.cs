using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;
using Cursor = UnityEngine.Cursor;

namespace Assets.Scripts.Behaviours
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Camera _currentCamera;
        [SerializeField] private float3 _offset;
        private Entity _entity;
        private float _xRot = 0;
        private float _yRot = 0;

        private EntityManager _manager;

        private void Awake()
        {
            _manager = World.DefaultGameObjectInjectionWorld.EntityManager;
        }

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            _xRot = transform.rotation.x;
            _yRot = transform.rotation.y;

        }

        private void Update()
        {
            _xRot += Input.GetAxis("Mouse X") * 90.0f * Time.deltaTime;
            _yRot += Input.GetAxis("Mouse Y") * 90.0f * Time.deltaTime;
        }

        private void LateUpdate()
        {
            var followEntityTranslation = _manager.GetComponentData<Translation>(_entity);
            if (followEntityTranslation.Equals(default)) return;
            var rotation = _manager.GetComponentData<Rotation>(_entity).Value;
            transform.position = followEntityTranslation.Value + _offset;
            transform.rotation = Quaternion.Euler(-_yRot, _xRot, 0.0f);
        }

        public void SetEntityFollow(Entity entity)
        {
            _entity = entity;
        }
    }
}
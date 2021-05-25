using Assets.Scripts.Behaviours;
using Assets.Scripts.Services;
using Unity.Entities;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private CameraFollow _camera;
    private BlobAssetStore _store;

    private void Awake()
    {
        _store = new BlobAssetStore();
        var player = _object.InstantiateEntityFromPrefab(_store);
        _camera.SetEntityFollow(player);
    }

    private void OnDestroy()
    {
        _store.Dispose();
    }
}

using System;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Services;
using Assets.Scripts.Services.Networking;
using Unity.Entities;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private CameraFollow _camera;
    private BlobAssetStore _store;
    

    private void Start()
    {
        NetworkManager.OnConnected += OnConnected;
    }

    public bool TryConnect(string loginName)
    {
        return NetworkManager.Connect(OnConnectionFinal, loginName);
    }

    private void OnConnectionFinal(Exception e)
    {
        if (e == null)
        {
            Debug.Log("Подключение успешно");
        }
        else Debug.LogWarning(e.Message);
    }

    private void OnConnected()
    {
        _store = new BlobAssetStore();
        var player = _object.InstantiateEntityFromPrefab(_store);
        _camera.SetEntityFollow(player);
    }

    private void OnDestroy()
    {
        _store?.Dispose();
        NetworkManager.Disconnect(() => {});
        NetworkManager.OnConnected -= OnConnected;
    }
}

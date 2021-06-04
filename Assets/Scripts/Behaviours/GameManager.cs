using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Behaviours;
using Assets.Scripts.Services;
using Assets.Scripts.Services.Networking;
using DarkRift;
using DarkRift.Client;
using Unity.Entities;
using UnityEngine;
using UnityEngine.Events;


public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject _object;
    [SerializeField] private CameraFollow _camera;
    private List<Entity> _otherPlayers = new List<Entity>();
    private Entity _localPlayer;
    private BlobAssetStore _store;
    private EntityManager _entityManager;
    private UnityEvent<string, ushort> _otherConnectAction = new UnityEvent<string, ushort>();
    

    private void Start()
    {
        NetworkManager.Instance.OnConnected += OnConnected;
        NetworkManager.Instance.OnDisconnected += OnDisconnected;
        NetworkManager.Instance.Handler.Subscription(Tags.SEND_CONNECTION, OnOtherConnected, false);
        _entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;
    }

    private void Update() => NetworkManager.Instance.Handler.Execute();

    private void OnDisconnected()
    {
        _entityManager.DestroyEntity(_localPlayer);
        for (int i = 0; i < _otherPlayers.Count; i++)
        {
            _entityManager.DestroyEntity(_otherPlayers[i]);
        }
    }

    public bool TryConnect(string loginName)
    {
        return NetworkManager.Instance.Connect(OnConnectionFinal, loginName);
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
        _store ??= new BlobAssetStore();
        _localPlayer = _object.InstantiatePlayerFromPrefab(_store, true);
        _camera.SetEntityFollow(_localPlayer);
    }

    private void OnOtherConnected(MessageReceivedEventArgs message)
    {
        using (Message msg = message.GetMessage())
        {
            using (DarkRiftReader reader = msg.GetReader())
            {
                var playerName = reader.ReadString();
                var playerId = reader.ReadUInt16();
                _store ??= new BlobAssetStore();
                _object.InstantiatePlayerFromPrefab(_store, false, playerName, playerId);
                Debug.Log("Подключился " + playerName);
            }
        }
    }

    private void OnDisconnectionFinal(Exception e)
    {
        if (e == null)
        {
            Debug.Log("Отключение успешно");
        }
        else Debug.LogWarning(e.Message);
    }

    private void OnDestroy()
    {
        _otherConnectAction.RemoveAllListeners();
        _store?.Dispose();
        NetworkManager.Instance.Disconnect(OnDisconnectionFinal);
        NetworkManager.Instance.Handler.Unsubscription(Tags.SEND_CONNECTION, OnOtherConnected, false);
        NetworkManager.Instance.OnConnected -= OnConnected;
        NetworkManager.Instance.OnDisconnected -= OnDisconnected;
    }
}

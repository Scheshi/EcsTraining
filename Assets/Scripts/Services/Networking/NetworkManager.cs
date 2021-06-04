using System;
using System.Net;
using DarkRift;
using DarkRift.Client;
using UnityEngine;


namespace Assets.Scripts.Services.Networking
{
    public enum Tags
    {
        SEND_NAME,
        SEND_POSITION,
        SEND_ROTATION,
        SEND_CONNECTION
    }
    
    public class NetworkManager
    {
        public static NetworkManager _instance;

        public static NetworkManager Instance
        {
            get
            {
                return _instance ??= new NetworkManager();
            }
        }

        private DarkRiftClient _client;
        private MessageHandler _handler;
        
        public event Action OnConnected = () => { };
        public event Action OnDisconnected = () => { };
        public event Action<string, ushort> OnOtherPlayerConnected = (name, id) => { };
        public bool IsConnected => _client.ConnectionState == ConnectionState.Connected;
        public MessageHandler Handler => _handler;

        private NetworkManager()
        {
            _client = new DarkRiftClient();
            _handler = new MessageHandler(_client);
        }
        
        public bool Connect(Action<Exception> onComplete, string name)
        {
            if (_client.ConnectionState == ConnectionState.Connected ||
                _client.ConnectionState == ConnectionState.Connecting)
            {
                return false;
            }

            try
            {
                _client.ConnectInBackground(IPAddress.Parse("127.0.0.1"), 4296, true, onComplete.Invoke);
                _handler.SendMessage(name, Tags.SEND_NAME, OnConnected);
                return true;
            }
            catch(Exception e)
            {
                Debug.LogErrorFormat(e.Message);
            }
            return false;
        }

        public bool Disconnect(Action<Exception> onComplete)
        {
            if (_client.ConnectionState == ConnectionState.Disconnected ||
                _client.ConnectionState == ConnectionState.Disconnecting)
            {
                return false;
            }
            try
            {
                _client.Disconnect();
                OnDisconnected.Invoke();
                _handler?.Dispose();
                _handler = null;
                onComplete.Invoke(null);
                return true;
            }
            catch(Exception e)
            {
                onComplete.Invoke(e);
            }
            return false;
        }

        public void Dispose()
        {
            OnConnected = null;
            OnDisconnected = null;
            OnOtherPlayerConnected = null;
            _client?.Dispose();
            _client = null;
            _handler?.Dispose();
            _handler = null;
        }
    }
}

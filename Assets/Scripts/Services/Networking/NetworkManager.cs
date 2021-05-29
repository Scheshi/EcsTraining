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
        SEND_POSITION
    }
    
    public class NetworkManager
    {
        public static event Action OnConnected = () => { };
        public static event Action OnDisconnected = () => { };
        private static DarkRiftClient _client;
        private static MessageSendler _sendler;
        private static bool IsConnected => _client.ConnectionState == ConnectionState.Connected;
        public static MessageSendler Sendler => _sendler;

        static NetworkManager()
        {
            _client = new DarkRiftClient();
            _sendler = new MessageSendler(_client);
        }
        
        public static bool Connect(Action<Exception> onComplete, string name)
        {
            if (_client.ConnectionState == ConnectionState.Connected ||
                _client.ConnectionState == ConnectionState.Connecting)
            {
                return false;
            }

            try
            {
                _client.ConnectInBackground(IPAddress.Parse("127.0.0.1"), 4296, true, onComplete.Invoke);
                _sendler.SendMessage(name, Tags.SEND_NAME, OnConnected);
                return true;
            }
            catch(Exception e)
            {
                Debug.LogErrorFormat(e.Message);
            }
            return false;
        }

        public static bool Disconnect(Action onComplete)
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
                return true;
            }
            catch(Exception e)
            {
                Debug.LogErrorFormat(e.Message);
            }
            return false;
        }
    }
}

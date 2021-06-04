using System;
using System.Collections.Generic;
using DarkRift;
using DarkRift.Client;
using DarkRift.Dispatching;
using Unity.Mathematics;
using UnityEngine;

namespace Assets.Scripts.Services.Networking
{
    public class MessageHandler: IDisposable
    {
        private DarkRiftClient _client;
        private Dispatcher _dispatcher;

        private Dictionary<Tags, Action<MessageReceivedEventArgs>> _actions =
            new Dictionary<Tags, Action<MessageReceivedEventArgs>>()
            {
                {Tags.SEND_NAME, message => {}},
                {Tags.SEND_POSITION, message => {}},
                {Tags.SEND_ROTATION, message => {}},
                {Tags.SEND_CONNECTION, message => {}}
            };
        public MessageHandler(DarkRiftClient client)
        {
            _client = client;
            _dispatcher = new Dispatcher(true);
            _client.MessageReceived += OnMessageReceived;
            
        }

        public void Execute()
        {
            _dispatcher.ExecuteDispatcherTasks();
        }

        public void Subscription(Tags tag, Action<MessageReceivedEventArgs> onMessage, bool isAllowMultiThread)
        {
            if (isAllowMultiThread)
            {
                _actions[tag] += onMessage;
            }
            else _actions[tag] += args => _dispatcher.InvokeAsync(() => onMessage(args));
        }

        public void Unsubscription(Tags tag, Action<MessageReceivedEventArgs> onMessage, bool isAllowMultiThread)
        {
            if (isAllowMultiThread)
            {
                _actions[tag] -= onMessage;
            }
            else _actions[tag] -= args => _dispatcher.InvokeAsync(() => onMessage(args));
        }

        private void OnMessageReceived(object sender, MessageReceivedEventArgs message)
        {
            Tags tag = (Tags) message.Tag;
            _actions[tag].Invoke(message);
        }

        public void SendMessage(string str, Tags tag, Action onSended)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(str);
                using (Message message = Message.Create((ushort)tag, writer))
                {
                    onSended.Invoke();
                    _client.SendMessage(message, SendMode.Reliable);
                }
            }
        }

        public void SendMessage(float3 vector, Tags tag, Action onSended)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write((double)vector.x);
                writer.Write((double)vector.y);
                writer.Write((double)vector.z);
                using (Message message = Message.Create((ushort) tag, writer))
                {
                    onSended.Invoke();
                    _client.SendMessage(message, SendMode.Reliable);
                }
            }
        }
        
        public void SendMessage(float4 rotation, Tags tag, Action onSended)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write((double)rotation.x);
                writer.Write((double)rotation.y);
                writer.Write((double)rotation.z);
                writer.Write((double)rotation.w);
                using (Message message = Message.Create((ushort) tag, writer))
                {
                    onSended.Invoke();
                    _client.SendMessage(message, SendMode.Reliable);
                }
            }
        }

        public void Dispose()
        {
            _actions.Clear();
            _actions = null;
            _client.MessageReceived -= OnMessageReceived;
            _client?.Dispose();
            
        }
    }
}
using System;
using DarkRift;
using DarkRift.Client;
using Unity.Mathematics;

namespace Assets.Scripts.Services.Networking
{
    public class MessageSendler
    {
        private DarkRiftClient _client;
        public MessageSendler(DarkRiftClient client)
        {
            _client = client;
        }
        
        public void SendMessage(string str, Tags tag, Action onSended)
        {
            using (DarkRiftWriter writer = DarkRiftWriter.Create())
            {
                writer.Write(str);
                using (Message message = Message.Create((ushort) tag, writer))
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
                writer.Write(vector.x);
                writer.Write(vector.y);
                writer.Write(vector.z);
                using (Message message = Message.Create((ushort) tag, writer))
                {
                    onSended.Invoke();
                    _client.SendMessage(message, SendMode.Reliable);
                }
            }
        }
    }
}
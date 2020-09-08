using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using Azure.Messaging.EventHubs.Producer;
using Newtonsoft.Json;

namespace ConfApp.Services
{
    public class PassiveLocationService : IEventHubProducer
    {
        private const string ConnectionString =
            "Endpoint=sb://confapp-eventhub.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=mVOAaZ3ZCZf6fKgTFrvcE0PrXdkVFTdmrrHwQHyFQ6A=";

        private const string EventHubName = "heartbeat";
        private readonly ConcurrentQueue<HeartBeatMessage> _messages = new ConcurrentQueue<HeartBeatMessage>();
        private EventHubProducerClient _client;

        public PassiveLocationService()
        {
            ConfigureClient();
            Task.Run(ProcessMessages).ConfigureAwait(false);
        }

        public bool IsInitialized { get; private set; }

        public void SendAsync(HeartBeatMessage message)
        {
            _messages.Enqueue(message);
        }

        private async void ProcessMessages()
        {
            const int maxPerBatch = 1000;
            while (true & IsInitialized)
                // Create a batch of events 
                try
                {
                    var eventBatch = await _client.CreateBatchAsync();
                    var howMany = _messages.Count >= maxPerBatch ? maxPerBatch : _messages.Count;
                    for (var i = 0; i < howMany; i++)
                    {
                        HeartBeatMessage msg;
                        var success = _messages.TryDequeue(out msg);

                        if (success)
                        {
                            var data = JsonConvert.SerializeObject(msg, Formatting.Indented);
                            eventBatch.TryAdd(new EventData(Encoding.UTF8.GetBytes(data)));
                        }
                    }

                    if (eventBatch.Count > 0)
                    {
                        await _client.SendAsync(eventBatch);
                        Console.WriteLine(
                            $"Sent {eventBatch.Count} events - Batch Size:{eventBatch.SizeInBytes / 1024}KBs. {_messages.Count} messages in the Queue.");
                    }
                }
                catch (Exception ex)
                {
                    // Most likely there was an issue sending data to Event Hub
                    Debug.WriteLine(ex);
                    IsInitialized = false;
                }
        }

        private void ConfigureClient()
        {
            var connOptions = new EventHubConnectionOptions
            {
                TransportType = EventHubsTransportType.AmqpWebSockets
            };
            var co = new EventHubProducerClientOptions
            {
                ConnectionOptions = connOptions,
                RetryOptions = new EventHubsRetryOptions
                {
                    Mode = EventHubsRetryMode.Exponential
                }
            };

            try
            {
                _client = new EventHubProducerClient(ConnectionString, EventHubName, co);
                IsInitialized = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
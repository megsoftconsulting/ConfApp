namespace ConfApp.Services
{
    public interface IEventHubProducer
    {
        void SendAsync(HeartBeatMessage message);
    }
}
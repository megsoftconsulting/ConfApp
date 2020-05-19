namespace ConfApp.Services
{
    public interface IGeofencingService
    {
        void StartMonitoring(double latitude, double longitude, double radius, string id);
        void Clear();

        void StopMonitoring(string id);
    }
}
namespace d4160.Systems.Networking
{
    public interface INetworkingLauncher
    {
        bool IgnoreCallbacks { get; set; }

        void Awake();

        void Connect(byte maxPlayersPerRoom, string version);
    }
}
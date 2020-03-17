using System;

namespace d4160.Networking
{
    public interface INetworkingLauncher
    {
        Action OnConnected { get; set; }

        void Awake();

        void Connect(byte maxPlayersPerRoom, string version);
    }
}
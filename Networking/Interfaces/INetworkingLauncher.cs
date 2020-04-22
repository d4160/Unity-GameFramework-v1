using System;

namespace d4160.Networking
{
    public interface INetworkingLauncher
    {
        Action OnConnectedCallback { get; set; }

        void Awake();

        void Connect(byte maxPlayersPerRoom, string version);
    }
}
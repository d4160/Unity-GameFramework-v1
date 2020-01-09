namespace d4160.Networking
{
    using d4160.Core;

    public abstract class NetworkingManagerBase : Singleton<NetworkingManagerBase>
    {
        public abstract NetworkingControllerBase NetworkingController { get; }
        public abstract ChatControllerBase Chat { get; }
    }
}
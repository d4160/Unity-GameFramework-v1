namespace d4160.Networking
{
    public class DefaultNetworkingController : NetworkingControllerBase
    {
        public override void CreateNetworkingLauncher()
        {
            switch(m_networkingType)
            {
                case NetworkingType.PhotonUnityNetworking:
                    //m_chatProvider = new PUNChatProvider();
                break;
            }
        }
    }
}
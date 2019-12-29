namespace d4160.Systems.Networking
{
    using UnityEngine;

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
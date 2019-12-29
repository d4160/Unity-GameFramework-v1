namespace d4160.Systems.Networking
{
    using UnityEngine;

    public abstract class NetworkingControllerBase : MonoBehaviour
    {
        [SerializeField] protected NetworkingType m_networkingType = NetworkingType.PhotonUnityNetworking;
        [Tooltip("The maximum number of players per room")]
        [SerializeField] protected byte m_maxPlayersPerRoom = 4;
        /// <summary>
		/// This client's version number. Users are separated from each other by gameVersion (which allows you to make breaking changes).
		/// </summary>
		[SerializeField] protected string m_clientVersion = "1.0";

        protected INetworkingLauncher m_networkingLauncher;

        public INetworkingLauncher NetworkingLauncher => m_networkingLauncher;

        protected virtual void Awake()
        {
            CreateNetworkingLauncher();

            if (m_networkingLauncher != null)
            {
                m_networkingLauncher.Awake();
            }
        }

        public abstract void CreateNetworkingLauncher();

        public virtual void Connect()
        {
            if (m_networkingLauncher == null) return;

            m_networkingLauncher.Connect(m_maxPlayersPerRoom, m_clientVersion);
        }
    }
}
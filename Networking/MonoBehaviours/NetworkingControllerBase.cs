using UltEvents;
using UnityEngine.UIElements;

namespace d4160.Networking
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
        [SerializeField] protected bool m_connectAtStart;
        [Header("EVENTS")] [SerializeField] protected UltEvent _onConnected;

        protected INetworkingLauncher m_networkingLauncher;

        public INetworkingLauncher NetworkingLauncher => m_networkingLauncher;

        protected virtual void Awake()
        {
            CreateNetworkingLauncher();

            if (m_networkingLauncher != null)
            {
                if (_onConnected != null)
                    m_networkingLauncher.OnConnectedCallback += _onConnected.Invoke;

                m_networkingLauncher.Awake();
            }
        }

        protected virtual void Start()
        {
            if (m_connectAtStart)
                Connect();
        }

        protected virtual void OnDestroy()
        {
            if (m_networkingLauncher != null)
            {
                if (_onConnected != null)
                    m_networkingLauncher.OnConnectedCallback -= _onConnected.Invoke;
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
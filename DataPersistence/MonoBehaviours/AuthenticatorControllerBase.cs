using UltEvents;

namespace d4160.DataPersistence
{
    using System;
#if NAUGHTY_ATTRIBUTES
    using NaughtyAttributes;
#endif
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class AuthenticatorControllerBase : MonoBehaviour
    {
        [SerializeField] protected AuthenticationType m_authenticationType = AuthenticationType.Local;
        [SerializeField] protected bool m_allowOverrideAuthentication = false;
#if NAUGHTY_ATTRIBUTES
        [ShowIf("IsAuthenticationRemote")]
#endif
        [SerializeField] protected RemotePersistenceType m_remotePersistenceType = RemotePersistenceType.PlayFab;

        [SerializeField] protected bool m_useDeviceUniqueIdentifier;
#if NAUGHTY_ATTRIBUTES
        [ShowIf("IsAuthenticationRemote")]
#endif
        [SerializeField] protected bool m_multiplayerAuthentication;
#if NAUGHTY_ATTRIBUTES
        [ShowIf("IsAuthenticationRemote")]
#endif
        [SerializeField] protected bool m_chatAuthentication;

#if NAUGHTY_ATTRIBUTES
        [ShowIf("ShowUserDisplayName")]
#endif
        [SerializeField] protected string m_username;
        [SerializeField] protected bool m_authenticateAtStart;
        [SerializeField] protected UltEvent m_onLoginCompleted;
        [SerializeField] protected UltEvent m_onLoginFailed;

        protected IAuthenticator m_authenticator;
        protected bool m_authenticated;

        #region Editor Only
#if UNITY_EDITOR
        protected bool IsAuthenticationRemote => m_authenticationType == AuthenticationType.Remote;
        protected bool IsAuthenticationPlayFabRemote => IsAuthenticationRemote && m_remotePersistenceType == RemotePersistenceType.PlayFab;
        protected bool ShowChatController => IsAuthenticationRemote && m_chatAuthentication;
        protected bool ShowUserDisplayName => (!m_useDeviceUniqueIdentifier || (IsAuthenticationRemote && m_chatAuthentication));
#endif
        #endregion

        public AuthenticationType AuthenticationType => m_authenticationType;
        public RemotePersistenceType RemotePersistenceType => m_remotePersistenceType;
        public string AuthenticationId => m_authenticator != null ? m_authenticator.Id : "Player1";
        public bool Authenticated => m_authenticated;
        public string Username { get =>  m_username; set => m_username = value; }
        public bool CanAuthenticate => !m_authenticated || (m_authenticated && m_allowOverrideAuthentication);

        public bool AllowOverrideAuthentication
        {
            get => m_allowOverrideAuthentication;
            set => m_allowOverrideAuthentication = value;
        }

        protected virtual void Start()
        {
            if (m_authenticateAtStart)
                Login();
        }

        public virtual void Login()
        {
            Login(null);
        }

        public virtual void Login(Action onCompleted, Action onFailed = null)
        {
            if(m_authenticated && !m_allowOverrideAuthentication) return;

            m_authenticator = CreateAuthenticator(
                () => {
                    onCompleted?.Invoke();
                    m_onLoginCompleted?.Invoke();

                    m_authenticated = true;
                }, () => {
                    onFailed?.Invoke();
                    m_onLoginFailed?.Invoke();
                });

            if(m_authenticator != null)
                m_authenticator.Login();
        }

        public virtual void Logout()
        {
            if(!m_authenticated) return;

            if (m_authenticator != null)
            {
                m_authenticator.Logout();

                m_authenticated = false;
            }
        }

        protected abstract IAuthenticator CreateAuthenticator(Action onCompleted = null, Action onFailed = null);
    }
}
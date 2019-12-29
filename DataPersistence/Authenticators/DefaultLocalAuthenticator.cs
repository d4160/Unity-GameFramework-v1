namespace d4160.Systems.DataPersistence
{
    using System;

    public class DefaultLocalAuthenticator : IAuthenticator
    {
        protected string m_username;
        protected string m_id;
        protected Action m_resultCallback;
        protected Action m_errorCallback;

        public string Id => m_id;

        public DefaultLocalAuthenticator(string username,
            Action resultCallback,
            Action errorCallback)
        {
            m_username = username;
            m_resultCallback = resultCallback;
            m_errorCallback = errorCallback;
        }

        public virtual void Login()
        {
            m_id = m_username;
            // TODO link to PlayerList in GameData

            m_resultCallback?.Invoke();
        }

        public virtual void Logout()
        {
            m_id = string.Empty;
        }
    }
}
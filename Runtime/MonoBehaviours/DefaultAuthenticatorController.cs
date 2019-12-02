namespace d4160.GameFramework
{
    using System;
    using d4160.Systems.DataPersistence;

    public class DefaultAuthenticatorController : AuthenticatorControllerBase
    {
        protected override IAuthenticator CreateAuthenticator(Action onCompleted = null, Action onFailed = null)
        {
            var authenticator = new DefaultLocalAuthenticator
            (
                m_username, onCompleted, onFailed
            );

            return authenticator;
        }
    }
}
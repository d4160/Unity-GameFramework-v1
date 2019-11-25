namespace d4160.GameFramework
{
    using System;
    using d4160.Systems.DataPersistence;

    public class DefaultAuthenticatorController : AuthenticatorController
    {
        protected override IAuthenticator CreateAuthenticator(Action onCompleted = null, Action onFailed = null)
        {
            var authenticator = new LocalAuthenticator
            (
                m_username, onCompleted, onFailed
            );

            return authenticator;
        }
    }
}
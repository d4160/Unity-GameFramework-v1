namespace d4160.DataPersistence
{
    using System;

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
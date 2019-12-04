namespace d4160.GameFramework
{
  using d4160.Core;
  using d4160.Systems.DataPersistence;
    using NaughtyAttributes;
    using UnityEngine;

    public abstract class DefaultLeaderboardController : Singleton<DefaultLeaderboardController>
    {
        [SerializeField] protected AuthenticatorControllerBase m_authenticator;

        public abstract bool SubmitStat(string statDefinitionId, int value);
        public abstract bool SubmitStat(int statDefinitionHash, int value);
    }
}
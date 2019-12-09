namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Systems.DataPersistence;
    using NaughtyAttributes;
    using UnityExtensions;
    using UnityEngine;

    public abstract class DefaultLeaderboardController : Singleton<DefaultLeaderboardController>
    {
        [InspectInline]
        [SerializeField] protected AuthenticatorControllerBase m_authenticator;
        [ShowIf("IsAuthenticatorLocal")]
        [SerializeField] protected string m_playerStatsItem = "normalModePlayerStats";

#if UNITY_EDITOR
        protected bool IsAuthenticatorLocal => m_authenticator.AuthenticationType == AuthenticationType.Local;
#endif

        public virtual string PlayerStatsItem
        {
            get => m_playerStatsItem;
            set => m_playerStatsItem = value;
        }

        public AuthenticatorControllerBase Authenticator => m_authenticator;

        public abstract bool SubmitStat(string statDefinitionId, int value);
        public abstract bool SubmitStat(string statDefinitionId, int value, out int climbedPositions);
        public abstract bool SubmitStat(int statDefinitionId, int value);
        public abstract bool SubmitStat(int statDefinitionHash, int value, out int climbedPositions);
    }
}
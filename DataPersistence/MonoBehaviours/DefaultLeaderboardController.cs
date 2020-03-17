using System.Linq;
using d4160.Core.Attributes;
using UnityEngine.GameFoundation;

namespace d4160.DataPersistence
{
#if NAUGHTY_ATTRIBUTES
    using NaughtyAttributes;
#endif
    using UnityExtensions;
    using UnityEngine;

    public abstract class DefaultLeaderboardController : MonoBehaviour
    {
        [InspectInline]
        [SerializeField] protected AuthenticatorControllerBase m_authenticator;
#if NAUGHTY_ATTRIBUTES
        [ShowIf("IsAuthenticatorLocal")]

#endif
        [Dropdown(ValuesProperty = "ItemNames")]
        [SerializeField] protected int m_playerStatsItem;

#if UNITY_EDITOR
        protected bool IsAuthenticatorLocal => m_authenticator.AuthenticationType == AuthenticationType.Local;
        protected string[] ItemNames =>
            InventoryManager.catalog.GetItemDefinitions().Select(x => x.displayName).ToArray();
#endif

        public virtual int PlayerStatsItemIndex
        {
            get => m_playerStatsItem;
            set => m_playerStatsItem = value;
        }

        public virtual InventoryItemDefinition PlayerStatsItem =>
            InventoryManager.catalog.GetItemDefinitions()[m_playerStatsItem];

        public AuthenticatorControllerBase Authenticator => m_authenticator;

        public abstract bool SubmitStat(string statDefinitionId, int value);
        public abstract bool SubmitStat(string statDefinitionId, int value, out int climbedPositions);
        public abstract bool SubmitStat(int statDefinitionId, int value);
        public abstract bool SubmitStat(int statDefinitionHash, int value, out int climbedPositions);
    }
}
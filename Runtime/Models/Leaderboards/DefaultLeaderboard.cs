namespace d4160.GameFramework
{
    using d4160.Core.Attributes;
    using UnityEngine.GameFoundation;
    using System.Linq;
    using UnityEngine;

    [System.Serializable]
    public class DefaultLeaderboard : DefaultArchetype
    {
        [Dropdown(ValuesProperty = "StatNames")]
        [SerializeField] protected int m_stat;


#if UNITY_EDITOR
        protected string[] StatNames => StatManager.catalog.GetStatDefinitions().Select((x) => x.id).ToArray();
#endif
    }

    [System.Serializable]
    public class LeaderboardEntry
    {
    }
}
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
        [SerializeField] protected LeaderboardResetFrequency m_resetFrequency;
        [SerializeField] protected LeaderboardAggregationMethod m_aggregationMethod;
        [SerializeField] protected int m_currentIteration;

        [SerializeField] protected LeaderboardEntry[] m_entries;
    }

    [System.Serializable]
    public struct LeaderboardEntry
    {
        public string playerId;
        public float value;
    }
}
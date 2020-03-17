namespace d4160.GameFramework
{
    using d4160.Core.Attributes;
    using d4160.Core;
    using UnityEngine.GameFoundation;
    using System.Linq;
    using UnityEngine;
    using System.Collections.Generic;

    [System.Serializable]
    public class DefaultLeaderboard : DefaultArchetype
    {
        [Dropdown(ValuesProperty = "StatNames")]
        [SerializeField] protected int m_stat;
        [SerializeField] protected LeaderboardResetFrequency m_resetFrequency;
        [SerializeField] protected LeaderboardAggregationMethod m_aggregationMethod;
        [SerializeField] protected int m_currentIteration;

        [SerializeField] protected List<LeaderboardEntry> m_entries = new List<LeaderboardEntry>();

        public StatDefinition Stat => StatManager.catalog.GetStatDefinitions()[m_stat];

        public virtual bool Submit(string playerId, float value, out int climbedPositions)
        {
            bool newEntry = false;
            bool newRecord = false;
            int previousPos = 0;
            var updatedEntry = m_entries.FirstOrDefault(x => x.playerId == playerId);
            if(string.IsNullOrEmpty(updatedEntry.playerId))
            {
                // New entry for player
                newEntry = true;
                newRecord = true;

                updatedEntry.playerId = playerId;
                updatedEntry.value = value;
            }
            else
            {
                // Override entry

                var lastVal = updatedEntry.value;
                var newValue = value;

                switch(m_aggregationMethod)
                {
                    case LeaderboardAggregationMethod.Last:
                        newRecord = true;
                        break;
                    case LeaderboardAggregationMethod.Maximun:
                        if (newValue > lastVal)
                        {
                            newRecord = true;
                        }
                        break;
                    case LeaderboardAggregationMethod.Minimum:
                        if (newValue < lastVal)
                        {
                            newRecord = true;
                        }
                        break;
                    case LeaderboardAggregationMethod.Sum:
                        newValue += lastVal;
                        newRecord = true;
                        break;
                }

                if (newRecord)
                    updatedEntry.value = newValue;
            }

            if(newEntry)
            {
                previousPos = m_entries.Count + 1;
                m_entries.Add(updatedEntry);
            }
            else
            {
                if (newRecord)
                {
                    for (int i = 0; i < m_entries.Count; i++)
                    {
                        if (m_entries[i].playerId == playerId)
                        {
                            m_entries[i] = updatedEntry;
                            previousPos = i + 1;
                            break;
                        }
                    }
                }
            }

            switch(m_aggregationMethod)
            {
                case LeaderboardAggregationMethod.Last:
                    var last = m_entries.Last();
                    m_entries.RemoveAt(m_entries.LastIndex());
                    m_entries.Insert(0, last);
                break;
                case LeaderboardAggregationMethod.Minimum:
                    m_entries = m_entries.OrderBy(x => x.value).ToList();
                break;
                case LeaderboardAggregationMethod.Maximun:
                case LeaderboardAggregationMethod.Sum:
                    m_entries = m_entries.OrderByDescending(x => x.value).ToList();
                break;
            }

            int updatedPos = 0;

            for (int i = 0; i < m_entries.Count; i++)
            {
                if (m_entries[i].playerId == playerId)
                {
                    updatedPos = i + 1;
                    break;
                }
            }

            climbedPositions = previousPos - updatedPos;

            return newRecord;
        }
    }

    [System.Serializable]
    public struct LeaderboardEntry
    {
        public string playerId;
        public float value;
    }
}
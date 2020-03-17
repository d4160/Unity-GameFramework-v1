using System.Collections.Generic;
using System.Linq;
using d4160.Core;
using d4160.GameFramework;
using d4160.Utilities;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.GameFoundation;
using UnityExtensions;

namespace d4160.GameFoundation
{
    // TODO: Save behaviour (need an event to call IStoreStat which search for some StatCalculator)
    public class MultipleStatCalculator : MonoBehaviour, IMultipleStatCalculator
    {
        [InspectInline(canEditRemoteTarget = true, canCreateSubasset = true)]
        [SerializeField] protected MultipleStatCalculatorDefinitionBase m_statCalculatorDefinition;
        [SerializeField] protected bool m_calculateAtStart;
        [SerializeField] protected int m_difficultyLevelToSetAtStart;
        [SerializeField] protected int m_additionalEmptyStatsNumber;
        [SerializeField] protected IntFloatUltEvent m_onStatUpdated;

        protected float[] m_statValues;

        public IntFloatUltEvent OnStatUpdated => m_onStatUpdated;

        public virtual float this[int index]
        {
            get => m_statValues.IsValidIndex(index) ? m_statValues[index] : 0f;
            set
            {
                if (m_statValues.IsValidIndex(index))
                {
                    m_statValues[index] = value;
                    m_onStatUpdated?.Invoke(index, value);
                }
            }
        }

        public virtual float[] FloatStats
        {
            get => m_statValues;
            set
            {
                m_statValues = value;
                for (int i = 0; i < m_statValues.Length; i++)
                {
                    m_onStatUpdated?.Invoke(i, m_statValues[i]);
                }
            }
        }
        
        public virtual int GetIntStat(int index) => (int)this[index];
        public virtual void SetIntStat(int index, int value) => this[index] = value;

        protected virtual void Start()
        {
            if (!m_calculateAtStart) return;

            if (!InventoryManager.IsInitialized)
            {
                DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls += CalculateStatsCallback;
            }
            else
            {
                CalculateStatsCallback();
            }
        }

        protected virtual void OnDestroy()
        {
            DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls -= CalculateStatsCallback;
        }

        protected void CalculateStatsCallback()
        {
            CalculateStats(m_difficultyLevelToSetAtStart, m_additionalEmptyStatsNumber);
        }

        public virtual float[] CalculateStats(int difficultyLevel = 1, int additionalStats = 0)
        {
            if (m_statCalculatorDefinition)
            {
                FloatStats = m_statCalculatorDefinition.CalculateStats(difficultyLevel, additionalStats);
            }

            return FloatStats;
        }

        public virtual float CalculateStat(int index, int difficultyLevel = 1)
        {
            if (m_statCalculatorDefinition)
            {
                this[index] = m_statCalculatorDefinition.CalculateStat(index, difficultyLevel);
            }

            return this[index];
        }

        public virtual void StoreStats()
        {
            if (m_statCalculatorDefinition)
            {
                m_statCalculatorDefinition.StoreStats(m_statValues);
            }
        }

        public virtual void StoreStat(int index)
        {
            if (m_statCalculatorDefinition)
            {
                m_statCalculatorDefinition.StoreStat(index, this[index]);
            }
        }

        public virtual void AddIntValueToStats(int value)
        {
            float val = value;
            for (int i = 0; i < FloatStats.Length; i++)
            {
                this[i] += val;
            }
        }

        public virtual void AddIntValueToStat(int index, int value)
        {
            if (FloatStats.IsValidIndex(index))
            {
                this[index] += value;
            }
        }

        public virtual void AddFloatValueToStats(float value)
        {
            float val = value;
            for (int i = 0; i < FloatStats.Length; i++)
            {
                this[i] += val;
            }
        }

        public virtual void AddFloatValueToStat(int index, float value)
        {
            if (FloatStats.IsValidIndex(index))
            {
                this[index] += value;
            }
        }

        public virtual int AddNewStat(int statIndex = -1, int difficultyLevel = 1)
        {
            List<float> list = null;
            list = m_statValues != null ? new List<float>(m_statValues) { default } : new List<float>(1){ default };

            m_statValues = list.ToArray();

            if (statIndex >= 0)
            {
                this[m_statValues.LastIndex()] = m_statCalculatorDefinition.CalculateStat(statIndex, difficultyLevel);
            }
            else
            {
                this[m_statValues.LastIndex()] = 0;
            }

            return m_statValues.LastIndex();
        }
    }
}

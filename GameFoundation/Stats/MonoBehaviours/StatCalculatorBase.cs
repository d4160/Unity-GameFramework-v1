using d4160.GameFramework;
using d4160.Utilities;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.GameFoundation;

namespace d4160.GameFoundation
{
    public class StatCalculatorBase : MonoBehaviour, IStatCalculator
    {
        [SerializeField] protected StatCalculatorDefinitionBase m_statCalculatorDefinition;
        [SerializeField] protected bool m_calculateAtStart;
        [SerializeField] protected int m_difficultyLevelToSetAtStart;
        [SerializeField] protected float m_statValue;
        [SerializeField] protected float m_maxStatValue;

        [SerializeField] protected FloatUltEvent m_onStatUpdated;
        [SerializeField] protected FloatUltEvent m_onNormalizedStatUpdated;
        [SerializeField] protected FloatUltEvent m_onMaxStatUpdated;

        public virtual int IntStat
        {
            get => (int)FloatStat;
            set => FloatStat = value;
        }

        public virtual float FloatStat
        {
            get => m_statValue;
            protected set
            {
                m_statValue = value;

                m_onStatUpdated?.Invoke(m_statValue);

                m_onNormalizedStatUpdated?.Invoke(m_statValue / m_maxStatValue);
            }
        }

        public virtual int IntMaxStat
        {
            get => (int)FloatMaxStat;
            set => FloatMaxStat = value;
        }

        public virtual float FloatMaxStat
        {
            get => m_maxStatValue;
            protected set
            {
                m_maxStatValue = value;

                m_onMaxStatUpdated?.Invoke(m_maxStatValue);
            }
        }

        protected virtual void Start()
        {
            if (!m_calculateAtStart) return;

            if (!InventoryManager.IsInitialized)
            {
                DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls += CalculateStatAtStart;
            }
            else
            {
                CalculateStatAtStart();
            }
        }

        protected virtual void OnDestroy()
        {
            if (InventoryManager.IsInitialized)
                DefaultDataLoader.GameFoundationDataLoader.OnInitializeCompleted.DynamicCalls -= CalculateStatAtStart;
        }

        protected void CalculateStatAtStart()
        {
            CalculateStat(m_difficultyLevelToSetAtStart);
        }

        public virtual float CalculateStat(int difficultyLevel = 1)
        {
            if (m_statCalculatorDefinition)
            {
                FloatStat = m_statCalculatorDefinition.CalculateStat(difficultyLevel);
                FloatMaxStat = FloatStat;
            }
            else
            {
                FloatStat = FloatMaxStat;
            }

            return FloatStat;
        }

        public virtual void UpdateStat(float diff)
        {
            var newVal = FloatStat + diff;

            FloatStat = Mathf.Clamp(newVal, 0f, FloatMaxStat);
        }

        public virtual void UpdateMaxStat(float diff)
        {
            var newVal = FloatMaxStat + diff;

            if (newVal < 0)
                FloatMaxStat = 0f;
            else
                FloatMaxStat = newVal;
        }

        public virtual void UpdateStat(int diff)
        {
            var newVal = FloatStat + diff;

            FloatStat = Mathf.Clamp(newVal, 0f, FloatMaxStat);
        }

        public virtual void UpdateMaxStat(int diff)
        {
            var newVal = FloatMaxStat + diff;

            if (newVal < 0)
                FloatMaxStat = 0f;
            else
                FloatMaxStat = newVal;
        }
    }

    public abstract class StatCalculatorBase<T1, T2> : MonoBehaviour, IStatCalculator<T2> where T1 : StatCalculatorDefinitionBase<T2>
    {
        [SerializeField] protected T1 m_statCalculatorDefinition;
        [SerializeField] protected UltEvent m_onStatUpdated;

        protected T2 m_statValue;

        public virtual T2 Stat
        {
            get => m_statValue;
            protected set
            {
                m_statValue = value;

                m_onStatUpdated?.Invoke(m_statValue);
            }
        }

        public virtual T2 CalculateStat(int difficultyLevel)
        {
            if (m_statCalculatorDefinition)
                Stat = m_statCalculatorDefinition.CalculateStat(difficultyLevel);

            return Stat;
        }

        public virtual void UpdateStat(float deltaTime)
        {
        }

        [System.Serializable]
        public class UltEvent : UltEvent<T2>
        {
        }
    }
}

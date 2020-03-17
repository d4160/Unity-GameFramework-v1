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
        [SerializeField] protected FloatUltEvent m_onStatUpdated;

        protected float m_statValue;

        public virtual int IntStat
        {
            get => (int) FloatStat;
            set => FloatStat = value;
        }

        public virtual float FloatStat
        {
            get => m_statValue;
            protected set
            {
                m_statValue = value;

                m_onStatUpdated?.Invoke(m_statValue);
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
                FloatStat = m_statCalculatorDefinition.CalculateStat(difficultyLevel);

            return FloatStat;
        }

        public virtual void UpdateStat(float deltaTime)
        {
            float diff = deltaTime;
            FloatStat += diff;
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

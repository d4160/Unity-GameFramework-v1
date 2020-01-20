using d4160.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace d4160.Systems.Flow
{
    public class StatCalculatorBase : MonoBehaviour, IStatCalculator
    {
        [SerializeField] protected StatCalculatorDefinitionBase m_statCalculatorDefinition;
        [SerializeField] protected UnityUtilities.FloatEvent m_onStatUpdated;

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
        [SerializeField] protected UnityEvent m_onStatUpdated;

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
        public class UnityEvent : UnityEvent<T2>
        {
        }
    }
}

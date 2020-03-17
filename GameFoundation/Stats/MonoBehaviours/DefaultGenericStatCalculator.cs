using UnityEngine;

namespace d4160.GameFoundation
{
    public abstract class DefaultGenericStatCalculator<T1, T2> : StatCalculatorBase<T1, T2> where T1 : StatCalculatorDefinitionBase<T2>
    {
        [SerializeField] protected UltEvent m_onPartialStatUpdated;

        protected T2 m_partialStatValue;

        public virtual T2 PartialStat
        {
            get => m_partialStatValue;
            protected set
            {
                m_partialStatValue = value;

                m_onPartialStatUpdated?.Invoke(m_partialStatValue);
            }
        }

        public override T2 CalculateStat(int difficultyLevel)
        {
            if (m_statCalculatorDefinition)
            {
                PartialStat = m_statCalculatorDefinition.CalculateStat(difficultyLevel);
            }

            Stat = PartialStat;

            return Stat;
        }
    }
}
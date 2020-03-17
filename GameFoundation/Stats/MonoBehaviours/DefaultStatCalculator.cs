using d4160.Utilities;
using UltEvents;
using UnityEngine;

namespace d4160.GameFoundation
{
    public class DefaultStatCalculator : StatCalculatorBase
    {
        [SerializeField] protected FloatUltEvent m_onPartialStatUpdated;

        public virtual int PartialIntStat => (int)PartialFloatStat;

        protected float m_partialStatValue;

        public virtual float PartialFloatStat
        {
            get => m_partialStatValue;
            protected set
            {
                m_partialStatValue = value;

                m_onPartialStatUpdated?.Invoke(m_partialStatValue);
            }
        }

        public override float CalculateStat(int difficultyLevel = 1)
        {
            if (m_statCalculatorDefinition)
            {
                PartialFloatStat = m_statCalculatorDefinition.CalculateStat(difficultyLevel);

                FloatStat += PartialFloatStat;
            }

            return FloatStat;
        }
    }
}
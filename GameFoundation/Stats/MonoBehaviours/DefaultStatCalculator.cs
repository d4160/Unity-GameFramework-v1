using d4160.Utilities;
using UltEvents;
using UnityEngine;

namespace d4160.GameFoundation
{
    public class DefaultStatCalculator : StatCalculatorBase
    {
        [SerializeField] protected FloatUltEvent m_onPartialStatUpdated;
        [SerializeField] protected FloatUltEvent m_onMaxPartialStatUpdated;

        public virtual int PartialIntStat => (int)PartialFloatStat;
        public virtual int MaxPartialIntStat => (int)MaxPartialFloatStat;

        protected float m_partialStatValue;
        protected float m_maxPartialStatValue;

        public virtual float PartialFloatStat
        {
            get => m_partialStatValue;
            protected set
            {
                m_partialStatValue = value;

                m_onPartialStatUpdated?.Invoke(m_partialStatValue);
            }
        }

        public virtual float MaxPartialFloatStat
        {
            get => m_maxPartialStatValue;
            protected set
            {
                m_maxPartialStatValue = value;

                m_onMaxPartialStatUpdated?.Invoke(m_maxPartialStatValue);
            }
        }

        public override void UpdateStat(float diff)
        {
            PartialFloatStat = diff;

            base.UpdateStat(diff);
        }

        public override void UpdateMaxStat(float diff)
        {
            MaxPartialFloatStat = diff;

            base.UpdateMaxStat(diff);
        }

        public override void UpdateStat(int diff)
        {
            PartialFloatStat = diff;

            base.UpdateStat(diff);
        }

        public override void UpdateMaxStat(int diff)
        {
            MaxPartialFloatStat = diff;

            base.UpdateMaxStat(diff);
        }
    }
}
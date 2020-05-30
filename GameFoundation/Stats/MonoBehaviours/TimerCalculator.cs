using d4160.Loops;
using d4160.Utilities;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;

namespace d4160.GameFoundation
{
    public class TimerCalculator : StatCalculatorBase
    {
        [SerializeField] protected bool m_activeWhenCalculated;
        [SerializeField] protected FloatUltEvent m_onTimerCalculated;
        [SerializeField] protected UltEvent m_onTimerOver;

        [SerializeField] protected bool _activeState;

        private void OnEnable()
        {
            UpdateLoop.OnUpdate += UpdateStat;
        }

        private void OnDisable()
        {
            UpdateLoop.OnUpdate -= UpdateStat;
        }

        public override void UpdateStat(float diff)
        {
            if (!_activeState) return;

            if (FloatStat > 0)
            {
                var newVal = FloatStat - diff;

                if (newVal <= 0)
                {
                    FloatStat = 0;

                    m_onTimerOver?.Invoke();
                }
                else 
                {
                    FloatStat = newVal;
                }
            }
        }

        public override float CalculateStat(int difficultyLevel = 1)
        {
            var stat = base.CalculateStat(difficultyLevel);

            _activeState = m_activeWhenCalculated;

            m_onTimerCalculated?.Invoke(stat);

            return stat;
        }

        public virtual void SetTimerActive(bool active)
        {
            _activeState = active;
        }
    }
}
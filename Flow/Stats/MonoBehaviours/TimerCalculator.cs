using d4160.Loops;
using d4160.Utilities;
using UnityEngine;
using UnityEngine.Events;

namespace d4160.Systems.Flow
{
    public class TimerCalculator : StatCalculatorBase
    {
        [SerializeField] protected UnityEvent m_onTimerOver;

        private void OnEnable()
        {
            UpdateLoop.OnUpdate += UpdateStat;
        }

        private void OnDisable()
        {
            UpdateLoop.OnUpdate -= UpdateStat;
        }

        public override void UpdateStat(float deltaTime)
        {
            if (FloatStat > 0)
            {
                FloatStat -= deltaTime;

                if (FloatStat <= 0)
                {
                    FloatStat = 0;

                    m_onTimerOver?.Invoke();
                }
            }
        }
    }
}
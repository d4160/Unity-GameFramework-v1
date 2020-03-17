using System.Collections.Generic;
using d4160.Core;
using d4160.Loops;
using d4160.Utilities;
using UltEvents;
using UnityEngine;
using UnityEngine.Events;

namespace d4160.GameFoundation
{
    public class MultipleTimerCalculator : MultipleStatCalculator
    {
        [SerializeField] protected bool m_activeWhenCalculated;
        [SerializeField] protected IntFloatUltEvent m_onTimerCalculated;
        [SerializeField] protected IntUltEvent m_onTimerOver;

        protected bool[] _activeStates;

        public IntFloatUltEvent OnTimerCalculated => m_onTimerCalculated;
        public IntUltEvent OnTimerOver => m_onTimerOver;

        private void OnEnable()
        {
            UpdateLoop.OnUpdate += AddFloatValueToStats;
        }

        private void OnDisable()
        {
            UpdateLoop.OnUpdate -= AddFloatValueToStats;
        }

        public override void AddFloatValueToStats(float value)
        {
            if (FloatStats == null) return; 

            for (int i = 0; i < FloatStats.Length; i++)
            {
                if (!_activeStates[i]) continue;

                if (FloatStats[i] > 0)
                {
                    FloatStats[i] -= value;

                    if (FloatStats[i] <= 0)
                    {
                        FloatStats[i] = 0;

                        m_onTimerOver?.Invoke(i);
                    }
                }
            }
        }

        public override float[] CalculateStats(int difficultyLevel = 1, int additionalStats = 0)
        {
            var stats = base.CalculateStats(difficultyLevel, additionalStats);
            _activeStates = new bool[stats.Length];

            for (int i = 0; i < stats.Length; i++)
            {
                _activeStates[i] = m_activeWhenCalculated;
                m_onTimerCalculated?.Invoke(i, stats[i]);
            }

            return stats;
        }

        public override float CalculateStat(int index, int difficultyLevel = 1)
        {
            var stat = base.CalculateStat(index, difficultyLevel);

            _activeStates[index] = m_activeWhenCalculated;

            m_onTimerCalculated?.Invoke(index, stat);

            return stat;
        }

        public virtual void SetAllTimersActive(bool active)
        {
            for (var i = 0; i < _activeStates.Length; i++)
            {
                _activeStates[i] = active;
            }
        }

        public virtual void SetTimerActive(int index, bool active)
        {
            if (_activeStates.IsValidIndex(index))
            {
                _activeStates[index] = active;
            }
        }

        public override int AddNewStat(int statIndex = -1, int difficultyLevel = 1)
        {
            var index = base.AddNewStat(statIndex, difficultyLevel);
            List<bool> list = null;

            list = _activeStates == null ? new List<bool>(1) { m_activeWhenCalculated } : new List<bool>(_activeStates) { m_activeWhenCalculated };

            _activeStates = list.ToArray();

            m_onTimerCalculated?.Invoke(index, this[index]);

            return index;
        }
    }
}
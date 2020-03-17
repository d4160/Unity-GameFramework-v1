using UnityEngine;
using UnityEngine.Events;

namespace d4160.GameFoundation
{
    public class ObjectiveCalculator : StatCalculatorBase
    {
        [SerializeField] protected UnityEvent m_onObjectiveCompleted;

        public virtual void CompleteObjective()
        {
            m_onObjectiveCompleted?.Invoke();
        }
    }
}
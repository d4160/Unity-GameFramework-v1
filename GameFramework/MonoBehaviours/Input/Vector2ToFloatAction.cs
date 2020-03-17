using UltEvents;
using UnityEngine;

namespace d4160.GameFramework
{
    public class Vector2ToFloatAction : MonoBehaviour
    {
        [SerializeField] protected FloatUltEvent _xAxisEvent;
        [SerializeField] protected FloatUltEvent _yAxisEvent;

        public virtual void Convert(Vector2 value)
        {
            _xAxisEvent?.Invoke(value.x);
            _yAxisEvent?.Invoke(value.y);
        }
    }
}
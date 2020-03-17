using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using UltEvents;
using UnityEngine;

namespace d4160.GameFramework
{
    [RequireComponent(typeof(Animator))]
    public class AnimatorBehaviour : MonoBehaviour
    {
        [SerializeField] protected AnimatorString[] _paramNames;
        [SerializeField] protected AnimatorString[] _stateNames;
        [SerializeField] protected IntUltEvent _onAnimationStartCallback;
        [SerializeField] protected IntUltEvent _onAnimationFinishCallback;

        protected Animator _animator;

        protected virtual void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        public virtual void PlayAnimState(int stateIndex = 0)
        {
            if (_stateNames.IsValidIndex(stateIndex))
            {
                _animator.Play(ref _stateNames[stateIndex]);
            }
        }

        public virtual void PlayAnimState(int stateIndex, int layer)
        {
            if (_stateNames.IsValidIndex(stateIndex))
            {
                _animator.Play(ref _stateNames[stateIndex], layer);
            }
        }

        public virtual void PlayAnimState(int stateIndex, int layer, float normalizedTime)
        {
            if (_stateNames.IsValidIndex(stateIndex))
            {
                _animator.Play(ref _stateNames[stateIndex], layer, normalizedTime);
            }
        }

        public virtual void ChangeCurrentStateNormalizedTime(int layer, float normalizedTime)
        {
            _animator.ChangeCurrentStateNormalizedTime(layer, normalizedTime);
        }

        public virtual void ChangeCurrentStateNormalizedTime(int layer = 0)
        {
            _animator.ChangeCurrentStateNormalizedTime(layer);
        }

        public virtual void SetTrigger(int paramIndex)
        {
            if (_paramNames.IsValidIndex(paramIndex))
            {
                _animator.SetTrigger(ref _paramNames[paramIndex]);
            }
        }

        public virtual void SetInteger(int paramIndex, int value)
        {
            if (_paramNames.IsValidIndex(paramIndex))
            {
                _animator.SetInteger(ref _paramNames[paramIndex], value);
            }
        }

        public virtual void SetFloat(int paramIndex, float value)
        {
            if (_paramNames.IsValidIndex(paramIndex))
            {
                _animator.SetFloat(ref _paramNames[paramIndex], value);
            }
        }

        public virtual void SetBool(int paramIndex, bool value)
        {
            if (_paramNames.IsValidIndex(paramIndex))
            {
                _animator.SetBool(ref _paramNames[paramIndex], value);
            }
        }

        public virtual void FinishAnimation(int index)
        {
            _onAnimationFinishCallback?.Invoke(index);
        }

        public virtual void StartAnimation(int index)
        {
            _onAnimationStartCallback?.Invoke(index);
        }
    }
}

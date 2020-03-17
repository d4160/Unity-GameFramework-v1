using System;
using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using UltEvents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace d4160.GameFramework
{
    public class InputValueActions : MonoBehaviour
    {
        [SerializeField] protected InputAxisAction[] _axisActions;
        [SerializeField] protected InputVector2Action[] _vector2Actions;

        public int SelectedIndex { get; set; } = 0;

        public virtual void ConvertToAxis(InputAction.CallbackContext ctx)
        {
            if (_axisActions.IsValidIndex(SelectedIndex))
            {
                if (!_axisActions[SelectedIndex].enabled)
                    return;

                _axisActions[SelectedIndex]._event?.Invoke(ctx.ReadValue<float>());
            }
        }

        public virtual void ConvertToVector2(InputAction.CallbackContext ctx)
        {
            if (_vector2Actions.IsValidIndex(SelectedIndex))
            {
                if (!_vector2Actions[SelectedIndex].enabled)
                    return;

                _vector2Actions[SelectedIndex]._event?.Invoke(ctx.ReadValue<Vector2>());
            }
        }

        /// <summary>
        /// -1 means the SelectedIndex
        /// </summary>
        /// <param name="active"></param>
        /// <param name="index"></param>
        public virtual void SetAxisInputActive(bool active, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;
            if (_axisActions.IsValidIndex(idx))
            {
                _axisActions[idx].enabled = active;
            }
        }

        public virtual void SetAllAxisInputActive(bool active)
        {
            for (int i = 0; i < _axisActions.Length; i++)
            {
                _axisActions[i].enabled = active;
            }
        }

        /// <summary>
        /// -1 means the SelectedIndex
        /// </summary>
        /// <param name="active"></param>
        /// <param name="index"></param>
        public virtual void SetVector2InputActive(bool active, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;
            if (_vector2Actions.IsValidIndex(idx))
            {
                _vector2Actions[idx].enabled = active;
            }
        }

        public virtual void SetAllVector2InputActive(bool active)
        {
            for (int i = 0; i < _vector2Actions.Length; i++)
            {
                _vector2Actions[i].enabled = active;
            }
        }

        public virtual void AddAxisCallback(Action<float> callback, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;

            if (_axisActions.IsValidIndex(idx))
            {
                _axisActions[idx]._event.DynamicCalls += callback;
            }
        }

        public virtual void AddVector2Callback(Action<Vector2> callback, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;

            if (_vector2Actions.IsValidIndex(idx))
            {
                _vector2Actions[idx]._event.DynamicCalls += callback;
            }
        }

        public virtual void RemoveAxisCallback(Action<float> callback, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;

            if (_axisActions.IsValidIndex(idx))
            {
                _axisActions[idx]._event.DynamicCalls -= callback;
            }
        }

        public virtual void RemoveVector2Callback(Action<Vector2> callback, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;

            if (_vector2Actions.IsValidIndex(idx))
            {
                _vector2Actions[idx]._event.DynamicCalls -= callback;
            }
        }
    }

    [System.Serializable]
    public struct InputAxisAction
    {
        public bool enabled;
        public FloatUltEvent _event;
    }

    [System.Serializable]
    public struct InputVector2Action
    {
        public bool enabled;
        public Vector2UltEvent _event;
    }
}
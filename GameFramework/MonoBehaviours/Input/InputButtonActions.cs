using System;
using System.Collections;
using System.Collections.Generic;
using d4160.Core;
using UltEvents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace d4160.GameFramework
{

    public class InputButtonActions : MonoBehaviour
    {
        [SerializeField] protected InputSystemAction[] _actions;

        public int SelectedIndex { get; set; }

        public virtual void DispachEvent(InputAction.CallbackContext ctx)
        {
            if (_actions.IsValidIndex(SelectedIndex))
            {
                if (!_actions[SelectedIndex].enabled)
                    return;

                switch (_actions[SelectedIndex].eventPhase)
                {
                    case 0:
                        break;
                    case InputSystemActionPhase.Started:
                        if (ctx.started)
                            _actions[SelectedIndex]._event.Invoke(ctx);
                        break;
                    case InputSystemActionPhase.Performed:
                        if (ctx.performed)
                            _actions[SelectedIndex]._event.Invoke(ctx);
                        break;
                    case InputSystemActionPhase.Canceled:
                        if (ctx.canceled)
                            _actions[SelectedIndex]._event.Invoke(ctx);
                        break;
                    case InputSystemActionPhase.Started | InputSystemActionPhase.Performed:
                        if (ctx.performed || ctx.started)
                            _actions[SelectedIndex]._event.Invoke(ctx);
                        break;
                    case InputSystemActionPhase.Started | InputSystemActionPhase.Canceled:
                        if (ctx.canceled || ctx.started)
                            _actions[SelectedIndex]._event.Invoke(ctx);
                        break;
                    case InputSystemActionPhase.Performed | InputSystemActionPhase.Canceled:
                        if (ctx.performed || ctx.canceled)
                            _actions[SelectedIndex]._event.Invoke(ctx);
                        break;
                    case InputSystemActionPhase.Started | InputSystemActionPhase.Performed | InputSystemActionPhase.Canceled:
                        _actions[SelectedIndex]._event.Invoke(ctx);
                        break;
                }
            }
        }

        /// <summary>
        /// -1 means the SelectedIndex
        /// </summary>
        /// <param name="active"></param>
        /// <param name="index"></param>
        public virtual void SetInputActive(bool active, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;
            if (_actions.IsValidIndex(idx))
            {
                _actions[idx].enabled = active;
            }
        }

        public virtual void SetAllInputActive(bool active)
        {
            for (int i = 0; i < _actions.Length; i++)
            {
                _actions[i].enabled = active;
            }
        }

        public virtual void AddCallback(Action<InputAction.CallbackContext> callback, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;
            if (_actions.IsValidIndex(idx))
            {
                _actions[idx]._event.DynamicCalls += callback;
            }
        }

        public virtual void RemoveCallback(Action<InputAction.CallbackContext> callback, int index = -1)
        {
            int idx = index == -1 ? SelectedIndex : index;
            if (_actions.IsValidIndex(idx))
            {
                _actions[idx]._event.DynamicCalls -= callback;
            }
        }
    }

    [System.Serializable]
    public class InputSystemUltEvent : UltEvent<InputAction.CallbackContext>
    {
    }

    [System.Serializable]
    public struct InputSystemAction
    {
        public bool enabled;
        public InputSystemActionPhase eventPhase;
        public InputSystemUltEvent _event;
    }

    [System.Flags]
    public enum InputSystemActionPhase
    {
        Started = 1 << 1,
        Performed = 1 << 2,
        Canceled = 1 << 3
    }
}
using System.Collections;
using System.Collections.Generic;
using UltEvents;
using UnityEngine;

namespace d4160.Networking
{
    public abstract class NetworkingEntityBehaviour : MonoBehaviour
    {
        [SerializeField] protected UltEvent _onLocalEntity;
        [SerializeField] protected UltEvent _onRemoteEntity;

        public abstract bool IsLocal { get; }

        protected virtual void Awake()
        {
            if (IsLocal)
            {
                _onLocalEntity?.Invoke();
            }
            else
            {
                _onRemoteEntity?.Invoke();
            }
        }
    }
}
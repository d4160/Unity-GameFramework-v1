using System.Collections;
using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;
using UnityExtensions;

namespace d4160.GameFramework
{
    public abstract class GraphBehaviourBase<T> : MonoBehaviour where T : BaseGraphProcessor, new()
    {
        [SerializeField]
        [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
        protected GameFrameworkBaseGraph _graph;

        private T _graphProcessor;

        protected virtual void Start()
        {
            if (_graph && _graphProcessor == null)
            {
                _graphProcessor = new T();
                _graphProcessor.SetGraph(_graph);
            }
        }
    }
}
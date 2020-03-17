using d4160.Core;
using UnityEngine;

namespace d4160.GameFramework
{
    [RequireComponent(typeof(Canvas))]
    public abstract class CanvasBase : EntityBehaviourBase
    {
        protected Canvas m_canvas;

        protected virtual void Awake()
        {
            m_canvas = GetComponent<Canvas>();
        }

        protected virtual void Start()
        {
            CanvasPrefabsManagerBase.Instance.SetInstanced(this);
        }
    }
}
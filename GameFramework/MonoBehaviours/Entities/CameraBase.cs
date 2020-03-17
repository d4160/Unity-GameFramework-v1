using d4160.Core;
using UnityEngine;

namespace d4160.GameFramework
{
    [RequireComponent(typeof(Camera))]
    public abstract class CameraBase : EntityBehaviourBase
    {
        protected Camera m_camera;

        protected virtual void Awake()
        {
            m_camera = GetComponent<Camera>();
        }

        protected virtual void Start()
        {
            CameraPrefabsManagerBase.Instance.SetInstanced(this);
        }
    }
}
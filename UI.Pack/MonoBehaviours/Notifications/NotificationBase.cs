namespace d4160.UI.Notification
{
  using d4160.Core;
  using UnityEngine;

    public abstract class NotificationBase : EntityBehaviourBase, INotification
    {
        [SerializeField] protected TextComponentWrapper m_textComponent;

        protected virtual void Awake()
        {
            if (!m_textComponent)
                m_textComponent = GetComponent<TextComponentWrapper>();

            NotificationPrefabsManagerBase.Instance.SetInstanced(this);
        }

        public abstract void Notify(string msg, float duration);
    }
}

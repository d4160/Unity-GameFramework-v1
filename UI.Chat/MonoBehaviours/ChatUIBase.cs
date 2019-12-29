namespace d4160.UI
{
    using UnityEngine;

    public abstract class ChatUIBase : MonoBehaviour, IChatUI
    {
        protected virtual void Awake()
        {
            ChatPrefabsManagerBase.Instance.SetInstanced(this);
        }

        public virtual void OnEnterSend()
        {

        }

        public virtual void OnClickSend()
        {

        }

        public virtual void OnConnected(string[] friends = null)
        {

        }

        public virtual void OnDisconnected()
        {

        }

        public virtual void OnGetMessages(string channelName, string[] senders, object[] messages)
        {

        }

        public virtual void OnPrivateMessage(string sender, object message, string channelName)
        {

        }

        public virtual void OnChatStateChange(string state)
        {

        }

        public virtual void OnStatusUpdate(string user, int status, bool gotMessage, object message)
        {

        }

        public virtual void OnSubscribed(string[] channels)
        {

        }

        public virtual void OnUnsubscribed(string[] channels)
        {

        }

        public virtual void OnUserSubscribed(string channel, string user)
        {

        }

        public virtual void OnUserUnsubscribed(string channel, string user)
        {

        }

        public virtual void ShowChannel(string channel, string messages)
        {

        }

        public virtual void AppendToCurrentChannel(string text)
        {

        }

        public virtual void SubscribeOrShowChannel(string channel)
        {

        }

        public virtual void ClearMessagesOfCurrentChannel()
        {

        }
    }
}
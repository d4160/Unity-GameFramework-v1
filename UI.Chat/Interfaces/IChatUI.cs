namespace d4160.UI.Chat
{
    public interface IChatUI
    {
        void OnEnterSend();
        void OnClickSend();

        void OnConnected(string[] friends = null);
        void OnDisconnected();
        void OnChatStateChange(string state);
        void OnSubscribed(string[] channels);
        void OnUnsubscribed(string[] channels);
        void OnGetMessages(string channelName, string[] senders, object[] messages);
        void OnPrivateMessage(string sender, object message, string channelName);

        void OnStatusUpdate(string user, int status, bool gotMessage, object message);
        void OnUserSubscribed(string channel, string user);
        void OnUserUnsubscribed(string channel, string user);
        void ShowChannel(string channel, string messages);
        void AppendToCurrentChannel(string text);
        void SubscribeOrShowChannel(string channel);

        void ClearMessagesOfCurrentChannel();
    }
}
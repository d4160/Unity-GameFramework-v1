using System;

namespace d4160.Systems.Networking
{
    public interface IChatProvider
    {
        string Username { get; }

        bool UseCustomAuth { get; set; }

        Action<string[]> OnSubscribedAction { get; set; }

        Action<string[]> OnUnsubscribedAction { get; set; }

        Action<string, string[], object[]> OnGetMessagesAction { get; set; }

        Action<string, object, string> OnPrivateMessageAction { get; set; }

        Action<string, int, bool, object> OnStatusUpdateAction { get; set; }

        Action<string, string> OnUserSubscribedAction { get; set; }

        Action<string, string> OnUserUnsubscribedAction { get; set; }

        Action<string> OnChatStateChangedAction { get; set; }

        Action OnConnectedAction { get; set; }

        Action OnDisconnectedAction { get; set; }

        void Authenticate(string displayUsername, string customUsername = null);

        void Connect(string version);

        void Disconnect();

        void UpdateService();

        void SendPrivateMessage(string target, object message, bool forwardAsWebhook = false);

        void PublishMessage(string channel, object message, bool forwardAsWebhook = false);

        bool ContainsPrivateChannel(string channel);

        void RemovePrivateChannel(string channel);

        void ClearChannelMessages(string channelName, bool isPrivate);

        bool GetChannelMessages(string channelName, out string messages);

        bool AddMessageToChannel(string channelName, string message);

        void SetOnlineStatus(int newState, object messages = null);

        void Subscribe(string[] channels, int messagesFromHistory = 1);

        void Unsubscribe(string[] channels);

        void AddFriends(string[] friends);
    }
}
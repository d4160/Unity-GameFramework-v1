﻿namespace d4160.Networking
{
    public enum NetworkingType
    {
        PhotonUnityNetworking
    }

    public enum ChatProviderType
    {
        PhotonUnityNetworking
    }
    
    /// <summary>Contains commonly used status values for SetOnlineStatus. You can define your own.</summary>
    /// <remarks>
    /// While "online" (value 2 and up), the status message will be sent to anyone who has you on his friend list.
    ///
    /// Define custom online status values as you like with these rules:
    /// 0: Means "offline". It will be used when you are not connected. In this status, there is no status message.
    /// 1: Means "invisible" and is sent to friends as "offline". They see status 0, no message but you can chat.
    /// 2: And any higher value will be treated as "online". Status can be set.
    /// </remarks>
    public static class ChatUserStatus
    {
        /// <summary>(0) Offline.</summary>
        public const int Offline = 0;
        /// <summary>(1) Be invisible to everyone. Sends no message.</summary>
        public const int Invisible = 1;
        /// <summary>(2) Online and available.</summary>
        public const int Online = 2;
        /// <summary>(3) Online but not available.</summary>
        public const int Away = 3;
        /// <summary>(4) Do not disturb.</summary>
        public const int DND = 4;
        /// <summary>(5) Looking For Game/Group. Could be used when you want to be invited or do matchmaking.</summary>
        public const int LFG = 5;
        /// <summary>(6) Could be used when in a room, playing.</summary>
        public const int Playing = 6;
    }
}
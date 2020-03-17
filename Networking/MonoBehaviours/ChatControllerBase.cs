namespace d4160.Networking
{
	using System.Collections.Generic;
	using UnityEngine;
	using UnityEngine.Events;
	using UI.Chat;

	public abstract class ChatControllerBase : MonoBehaviour
	{
		[SerializeField] protected ChatProviderType m_chatProviderType = ChatProviderType.PhotonUnityNetworking;
		[SerializeField] protected bool m_authenticateAtStart;
        [SerializeField] protected bool m_connectAtStart;
		[SerializeField] protected string m_username;
		[Tooltip("Is allowed to use custom id or username from custom authentication. If is true, it will use a display name from an external source (like PhotonCustomAuth)")]
		[SerializeField] protected bool m_allowCustomAuthentication;
		[SerializeField] protected string m_clientVersion = "1.0";
		[SerializeField] protected string[] m_channelsToJoinOnConnect; // set in inspector. Demo channels to join automatically.
		[SerializeField] protected string[] m_friendsList;
		[SerializeField] protected int m_historyLengthToFetch; // set in inspector. Up to a certain degree, previously sent messages can be fetched for context
		[SerializeField, TextArea(7, 20)] protected string m_helpText = "\n    -- HELP --\n" +
                                              "To subscribe to channel(s) (channelnames are case sensitive) :  \n" +
                                              "\t<color=#E07B00>\\subscribe</color> <color=green><list of channelnames></color>\n" +
                                              "\tor\n" +
                                              "\t<color=#E07B00>\\s</color> <color=green><list of channelnames></color>\n" +
                                              "\n" +
                                              "To leave channel(s):\n" +
                                              "\t<color=#E07B00>\\unsubscribe</color> <color=green><list of channelnames></color>\n" +
                                              "\tor\n" +
                                              "\t<color=#E07B00>\\u</color> <color=green><list of channelnames></color>\n" +
                                              "\n" +
                                              "To switch the active channel\n" +
                                              "\t<color=#E07B00>\\join</color> <color=green><channelname></color>\n" +
                                              "\tor\n" +
                                              "\t<color=#E07B00>\\j</color> <color=green><channelname></color>\n" +
                                              "\n" +
                                              "To send a private message: (username are case sensitive)\n" +
                                              "\t\\<color=#E07B00>msg</color> <color=green><username></color> <color=green><message></color>\n" +
                                              "\n" +
                                              "To change status:\n" +
                                              "\t\\<color=#E07B00>state</color> <color=green><stateIndex></color> <color=green><message></color>\n" +
                                              "<color=green>0</color> = Offline " +
                                              "<color=green>1</color> = Invisible " +
                                              "<color=green>2</color> = Online " +
                                              "<color=green>3</color> = Away \n" +
                                              "<color=green>4</color> = Do not disturb " +
                                              "<color=green>5</color> = Looking For Group " +
                                              "<color=green>6</color> = Playing" +
                                              "\n\n" +
                                              "To clear the current chat tab (private chats get closed):\n" +
                                              "\t<color=#E07B00>\\clear</color>";

		protected IChatProvider m_chatProvider;
		protected string m_selectedChannelName;

		public IChatProvider ChatProvider => m_chatProvider;
		public string SelectedChannelName => m_selectedChannelName;
		public string Username { get =>  m_username; set => m_username = value; }

		protected virtual void Awake()
		{
			CreateChatProvider();

			m_chatProvider.UseCustomAuth = m_allowCustomAuthentication;
		}

		protected virtual void OnEnable()
		{
			m_chatProvider.OnConnectedAction += OnConnected;
			m_chatProvider.OnDisconnectedAction += OnDisconnected;
			m_chatProvider.OnSubscribedAction += OnSubscribed;
			m_chatProvider.OnUnsubscribedAction += OnUnsubscribed;
			m_chatProvider.OnGetMessagesAction += OnGetMessages;
			m_chatProvider.OnPrivateMessageAction += OnPrivateMessage;
		    m_chatProvider.OnStatusUpdateAction += OnStatusUpdate;
			m_chatProvider.OnUserSubscribedAction += OnUserSubscribed;
			m_chatProvider.OnUserUnsubscribedAction += OnUserUnsubscribed;
			m_chatProvider.OnChatStateChangedAction += OnChatStateChange;
		}

		protected virtual void OnDisable()
		{
			m_chatProvider.OnConnectedAction -= OnConnected;
			m_chatProvider.OnDisconnectedAction -= OnDisconnected;
			m_chatProvider.OnSubscribedAction -= OnSubscribed;
			m_chatProvider.OnUnsubscribedAction -= OnUnsubscribed;
			m_chatProvider.OnGetMessagesAction -= OnGetMessages;
			m_chatProvider.OnPrivateMessageAction -= OnPrivateMessage;
			m_chatProvider.OnStatusUpdateAction -= OnStatusUpdate;
			m_chatProvider.OnUserSubscribedAction -= OnUserSubscribed;
			m_chatProvider.OnUserUnsubscribedAction -= OnUserUnsubscribed;
			m_chatProvider.OnChatStateChangedAction -= OnChatStateChange;
		}

		protected virtual void Start()
		{
			if (m_authenticateAtStart)
				Authenticate();

			if (m_connectAtStart)
				Connect();
		}

		public virtual void Authenticate()
		{
			if (m_chatProvider != null)
			{
				m_chatProvider.Authenticate(m_username);
			}
		}

		public abstract void CreateChatProvider();

		protected virtual void OnConnected()
		{
			if (m_channelsToJoinOnConnect != null && this.m_channelsToJoinOnConnect.Length > 0)
			{
				m_chatProvider.Subscribe(m_channelsToJoinOnConnect, m_historyLengthToFetch);
			}

			if (ChatPrefabsManagerBase.Instanced)
			    ChatPrefabsManagerBase.Instance.InstancedMain?.OnConnected(m_friendsList);

			m_chatProvider.SetOnlineStatus(ChatUserStatus.Online); // You can set your online state (without a mesage).
		}

		protected virtual void OnDisconnected()
		{
			if (ChatPrefabsManagerBase.Instanced)
			    ChatPrefabsManagerBase.Instance.InstancedMain?.OnDisconnected();
		}

		protected void OnChatStateChange(string state)
		{
            if (ChatPrefabsManagerBase.Instanced)
				ChatPrefabsManagerBase.Instance.InstancedMain?.OnChatStateChange(state);
		}

		protected virtual void OnSubscribed(string[] channels)
		{
            if(ChatPrefabsManagerBase.Instanced)
			    ChatPrefabsManagerBase.Instance.InstancedMain?.OnSubscribed(channels);

			ShowChannel(channels[0]);
		}

		protected virtual void OnUnsubscribed(string[] channels)
		{
            if (ChatPrefabsManagerBase.Instanced)
				ChatPrefabsManagerBase.Instance.InstancedMain?.OnUnsubscribed(channels);
		}

		public void OnGetMessages(string channelName, string[] senders, object[] messages)
		{
			if (channelName.Equals(m_selectedChannelName))
			{
				// update text
				ShowChannel(m_selectedChannelName);
			}
		}

		// byte[] msgBytes = message as byte[]
		public void OnPrivateMessage(string sender, object message, string channelName)
		{
			ChatPrefabsManagerBase.Instance.InstancedMain.OnPrivateMessage(sender, message, channelName);

			if (m_selectedChannelName.Equals(channelName))
			{
					ShowChannel(channelName);
			}
		}

		/// <summary>
		/// New status of another user (you get updates for users set in your friends list).
		/// </summary>
		/// <param name="user">Name of the user.</param>
		/// <param name="status">New status of that user.</param>
		/// <param name="gotMessage">True if the status contains a message you should cache locally. False: This status update does not include a
		/// message (keep any you have).</param>
		/// <param name="message">Message that user set.</param>
		public void OnStatusUpdate(string user, int status, bool gotMessage, object message)
		{
			ChatPrefabsManagerBase.Instance.InstancedMain.OnStatusUpdate(user, status, gotMessage, message);
		}

		public virtual void OnUserSubscribed(string channel, string user)
		{
			ChatPrefabsManagerBase.Instance.InstancedMain.OnUserSubscribed(channel, user);
		}

		public virtual void OnUserUnsubscribed(string channel, string user)
		{
			ChatPrefabsManagerBase.Instance.InstancedMain.OnUserUnsubscribed(channel, user);
		}

		public void AddMessageToSelectedChannel(string msg)
		{
			bool found = m_chatProvider.AddMessageToChannel(m_selectedChannelName, msg);
			if (!found)
			{
				Debug.Log("AddMessageToSelectedChannel failed to find channel: " + m_selectedChannelName);
				return;
			}
		}

		public virtual void Connect()
		{
			if (m_chatProvider != null)
			{
				m_chatProvider.Connect(m_clientVersion);

                Debug.Log("Connecting to chat as: " + m_chatProvider.Username);
			}
		}

		public virtual void Disconnect()
		{
			if (m_chatProvider != null)
			{
				m_chatProvider.Disconnect();

				Debug.Log("Disconnecting from chat as: " + m_chatProvider.Username);
			}
		}

		#region MonoBehaviour callbacks

		protected virtual void Update()
		{
				if (m_chatProvider != null)
						m_chatProvider.UpdateService();
		}

		protected virtual void OnDestroy()
		{
				if (m_chatProvider != null)
				{
						m_chatProvider.Disconnect();
				}
		}

		protected virtual void OnApplicationQuit()
		{
			if (m_chatProvider != null)
			{
				m_chatProvider.Disconnect();
			}
		}

		#endregion

		public virtual void SendChatMessage(string message)
		{
			if (string.IsNullOrEmpty(message))
			{
				return;
			}

			bool doingPrivateChat = m_chatProvider.ContainsPrivateChannel(this.m_selectedChannelName);
			string privateChatTarget = string.Empty;
			if (doingPrivateChat)
			{
				// the channel name for a private conversation is (on the client!!) always composed of both user's IDs: "this:remote"
				// so the remote ID is simple to figure out

				string[] splitNames = this.m_selectedChannelName.Split(new char[] { ':' });
				privateChatTarget = splitNames[1];
			}

			if (message[0].Equals('\\'))
			{
				string[] tokens = message.Split(new char[] {' '}, 2);
				if (tokens[0].Equals("\\help"))
				{
					AppendHelpToCurrentChannel();
				}
				if (tokens[0].Equals("\\state"))
				{
					int newState = 0;

					List<string> messages = new List<string>();
					messages.Add ("i am state " + newState);
					string[] subtokens = tokens[1].Split(new char[] {' ', ','});

					if (subtokens.Length > 0)
					{
						newState = int.Parse(subtokens[0]);
					}

					if (subtokens.Length > 1)
					{
						messages.Add(subtokens[1]);
					}

					m_chatProvider.SetOnlineStatus(newState,messages.ToArray()); // this is how you set your own state and (any) message
				}
				else if ((tokens[0].Equals("\\subscribe") || tokens[0].Equals("\\s")) && !string.IsNullOrEmpty(tokens[1]))
				{
					m_chatProvider.Subscribe(tokens[1].Split(new char[] {' ', ','}));
				}
				else if ((tokens[0].Equals("\\unsubscribe") || tokens[0].Equals("\\u")) && !string.IsNullOrEmpty(tokens[1]))
				{
					m_chatProvider.Unsubscribe(tokens[1].Split(new char[] {' ', ','}));
				}
				else if (tokens[0].Equals("\\clear"))
				{
					ChatPrefabsManagerBase.Instance.InstancedMain.ClearMessagesOfCurrentChannel();

					if (doingPrivateChat)
					{
						m_chatProvider.RemovePrivateChannel(m_selectedChannelName);
					}
					else
					{
						m_chatProvider.ClearChannelMessages(m_selectedChannelName, doingPrivateChat);
					}
				}
				else if (tokens[0].Equals("\\msg") && !string.IsNullOrEmpty(tokens[1]))
				{
					string[] subtokens = tokens[1].Split(new char[] {' ', ','}, 2);
					if (subtokens.Length < 2) return;

					string targetUser = subtokens[0];
					string msg = subtokens[1];

					m_selectedChannelName = $"{m_chatProvider.Username}:{targetUser}";
					m_chatProvider.SendPrivateMessage(targetUser, msg);
					//ShowChannel()
				}
				else if ((tokens[0].Equals("\\join") || tokens[0].Equals("\\j")) && !string.IsNullOrEmpty(tokens[1]))
				{
					string[] subtokens = tokens[1].Split(new char[] { ' ', ',' }, 2);

					ChatPrefabsManagerBase.Instance.InstancedMain.SubscribeOrShowChannel(subtokens[0]);
				}
				else
				{
					Debug.Log("The command '" + tokens[0] + "' is invalid.");
				}
			}
			else
			{
				if (doingPrivateChat)
				{
					m_chatProvider.SendPrivateMessage(privateChatTarget, message);
				}
				else
				{
					m_chatProvider.PublishMessage(m_selectedChannelName, message);
				}
			}
		}

		public virtual void AppendHelpToCurrentChannel()
		{
			ChatPrefabsManagerBase.Instance.InstancedMain.AppendToCurrentChannel(m_helpText);
		}

		public virtual void ShowChannel(string channelName)
		{
			if (string.IsNullOrEmpty(channelName))
			{
				return;
			}

			string messages = null;
			bool found = m_chatProvider.GetChannelMessages(channelName, out messages);

			if (!found)
			{
				Debug.Log("ShowChannel failed to find channel: " + channelName);
				return;
			}

			m_selectedChannelName = channelName;

			Debug.Log("ShowChannel: " + m_selectedChannelName);

			if(ChatPrefabsManagerBase.Instanced)
			    ChatPrefabsManagerBase.Instance.InstancedMain?.ShowChannel(channelName, messages);
		}
	}

	[System.Serializable]
	public class StringArrayEvent : UnityEvent<string[]>
	{
	}

	[System.Serializable]
	public class StringStringEvent : UnityEvent<string, string>
	{
	}
}
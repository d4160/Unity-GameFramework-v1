namespace d4160.Networking
{
    using UnityEngine;
    using UnityEngine.UI;

    public class DefaultChatController : ChatControllerBase
    {
        public override void CreateChatProvider()
        {
            switch(m_chatProviderType)
            {
                case ChatProviderType.PhotonUnityNetworking:
                    //m_chatProvider = new PUNChatProvider();
                break;
            }
        }

        protected override void OnSubscribed(string[] channels)
		{
			// in this demo, we simply send a message into each channel. This is NOT a must have!
			//foreach (string channel in channels)
			//{
			//	m_chatProvider.PublishMessage(channel, "says 'hi'."); // you don't HAVE to send a msg on join but you could.
			//}

            Debug.Log("OnSubscribed: " + string.Join(", ", channels));

            base.OnSubscribed(channels);
		}
    }
}
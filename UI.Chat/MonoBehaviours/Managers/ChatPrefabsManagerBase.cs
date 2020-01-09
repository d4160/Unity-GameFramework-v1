namespace d4160.UI.Chat
{
    using Core;

    public class ChatPrefabsManagerBase : PrefabManagerBase<ChatPrefabsManagerBase, ChatUIBase>
    {
        public override void SetInstanced(ChatUIBase instanced)
        {
            if (m_instancedMain && m_instancedMain != instanced)
                Destroy(instanced.gameObject);
            else
                m_instancedMain = instanced;
        }
    }
}
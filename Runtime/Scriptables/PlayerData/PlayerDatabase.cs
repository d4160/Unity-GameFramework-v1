namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Systems.DataPersistence;
    using Malee;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "PlayerDatabase.asset", menuName = "Game Framework/Player Database")]
    public class PlayerDatabase : ScriptableObject, IDataSerializationActions
    {
        protected IDataSerializationAdapter m_dataAdapter;

        [SerializeField]
        [Reorderable(paginate = true, pageSize = 10)]
        protected PlayerDataReorderableArray m_playerData;

        public PlayerDataReorderableArray PlayerData => m_playerData;

        public IDataSerializationAdapter DataAdapter
        {
            get {
                if (m_dataAdapter == null)
                    m_dataAdapter = new DefaultPlayerDataSerializationAdapter();

                return m_dataAdapter;
            }
            set => m_dataAdapter = value;
        }

        public PlayerDataScriptableBase this[int index]
        {
            get
            {
                if (m_playerData.IsValidIndex(index))
                    return m_playerData[index];

                return null;
            }
        }

        public T GetData<T>(int index) where T : PlayerDataScriptableBase
        {
            var playerData = this[index];
            if (playerData)
                return playerData as T;

            return null;
        }

        public ISerializableData GetSerializableData()
        {
            return DataAdapter.GetSerializableData();
        }

        public void FillFromSerializableData(ISerializableData data)
        {
            DataAdapter.FillFromSerializableData(data);
        }

        public void InitializeData(ISerializableData data)
        {
            if(data != null)
                FillFromSerializableData(data);
        }

        public void Unintialize()
        {
            m_dataAdapter = null;
        }

        public bool IsInitialized => m_playerData != null && m_playerData.Length > 0;
    }

    [System.Serializable]
    public class PlayerDataReorderableArray : ReorderableArrayForUnityObject<PlayerDataScriptableBase>
    {
    }
}

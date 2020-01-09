namespace d4160.GameFramework
{
    using Core;
    using DataPersistence;
    using Malee;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "PlayerDatabase.asset", menuName = "Game Framework/Player Database")]
    public class PlayerDatabase : ScriptableObject, IDataSerializationActions
    {
        protected IDataSerializationAdapter m_dataAdapter;

        [SerializeField]
        [Reorderable(paginate = true, pageSize = 10)]
        protected ScriptableObjectReorderableArray m_playerData;

        public ScriptableObjectReorderableArray PlayerData => m_playerData;

        public IDataSerializationAdapter DataAdapter
        {
            get { return m_dataAdapter ?? (m_dataAdapter = new DefaultPlayerDataSerializationAdapter()); }
            set => m_dataAdapter = value;
        }

        public ScriptableObjectBase this[int index]
        {
            get
            {
                if (m_playerData.IsValidIndex(index))
                    return m_playerData[index];

                return null;
            }
        }

        public T GetData<T>(int index) where T : ScriptableObjectBase
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
}

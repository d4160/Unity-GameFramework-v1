namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.DataPersistence;
    using Malee;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "AppSettingsDatabase.asset", menuName = "Game Framework/App Database")]
    public class AppSettingsDatabase : ScriptableObject, IDataSerializationActions
    {
        protected IDataSerializationAdapter m_dataAdapter;

        [SerializeField]
        [Reorderable(paginate = true, pageSize = 10)]
        protected ScriptableObjectReorderableArray m_appSettingsData;

        public ScriptableObjectReorderableArray AppSettingsData => m_appSettingsData;

        public IDataSerializationAdapter DataAdapter
        {
            get {
                if (m_dataAdapter == null)
                    m_dataAdapter = new DefaultAppSettingsDataSerializationAdapter();

                return m_dataAdapter;
            }
            set => m_dataAdapter = value;
        }

        public ScriptableObjectBase this[int index]
        {
            get
            {
                if (m_appSettingsData.IsValidIndex(index))
                    return m_appSettingsData[index];

                return null;
            }
        }

        public T GetSettingsData<T>(int index) where T : ScriptableObjectBase
        {
            var settings = this[index];
            if (settings)
                return settings as T;

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

        public bool IsInitialized => m_appSettingsData != null && m_appSettingsData.Length > 0;
    }
}

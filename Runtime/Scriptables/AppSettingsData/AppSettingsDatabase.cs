namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Systems.DataPersistence;
    using Malee;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;
    using UnityExtensions;

    [CreateAssetMenu(fileName = "AppSettingsDatabase.asset", menuName = "Game Framework/App Database")]
    public class AppSettingsDatabase : ScriptableObject, IDataSerializationActions
    {
        protected IDataSerializationAdapter m_dataAdapter;

        [SerializeField]
        [Reorderable(paginate = true, pageSize = 10)]
        protected ScriptableObjectReorderableArray m_settings;

        public ScriptableObjectReorderableArray Settings => m_settings;

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
                if (m_settings.IsValidIndex(index))
                    return m_settings[index];

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

        public bool IsInitialized => m_settings != null && m_settings.Length > 0;
    }
}

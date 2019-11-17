namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using global::OdinSerializer;
    using UnityEngine.GameFoundation.DataPersistence;

    public enum DataSerializerType
    {
        JsonUtility,
        Odin
    }

    public enum DataPersistenceType
    {
        PlayerPrefs,
        Local,
        Remote
    }

    public class AppSettingsDataLoader : DataLoader
    {
        [SerializeField] protected DataPersistenceType m_persistenceType = DataPersistenceType.Local;
        [SerializeField] protected DataSerializerType m_serializerType = DataSerializerType.Odin;
        [SerializeField] protected DataSerializationAdapterType m_adapterType = DataSerializationAdapterType.Generic;
        // Odin only
        [SerializeField] protected DataFormat m_dataFormat = DataFormat.JSON;

        DefaultAppSettingsDataSerializationAdapter _dataSerializationAdapter;

        public override void Initialize()
        {
            _dataSerializationAdapter = new DefaultAppSettingsDataSerializationAdapter(m_adapterType);

            // choose where and how the data is stored
            switch (m_persistenceType)
            {
                case DataPersistenceType.PlayerPrefs:
                    m_adapterType = DataSerializationAdapterType.Concrete;
                    m_dataPersistence = new DefaultPlayerPrefsPersistence(_dataSerializationAdapter.GetSerializableData() as IPlayerPrefsActions, m_encrypted);
                break;
                case DataPersistenceType.Local:
                    var serializer = CreateDataSerializer();
                    m_dataPersistence = new DefaultLocalPersistence(serializer, m_encrypted, SaveToPlayerPrefs);
                break;
                case DataPersistenceType.Remote:
                default:
                break;
            }

            // use the adapter to initialize
            _dataSerializationAdapter.Initialize(SaveDataPathFull, m_dataPersistence);
        }

        protected IDataSerializer CreateDataSerializer()
        {
            // choose what serializer you want to use
            IDataSerializer dataSerializer = null;
            switch(m_serializerType)
            {
                case DataSerializerType.JsonUtility:
                    dataSerializer = new JsonDataSerializer();
                    m_adapterType = DataSerializationAdapterType.Concrete;
                break;
                case DataSerializerType.Odin:
                    dataSerializer = new OdinDataSerializer(m_dataFormat);
                break;
            }

            return dataSerializer;
        }

        public override void Load()
        {
            _dataSerializationAdapter.Load(m_dataPersistence,
            () => Debug.Log("Load Completed"),
            () => Debug.Log("Load Failed"));
        }

        public override void Save()
        {
            _dataSerializationAdapter.Save(m_dataPersistence,
            () => Debug.Log("Save Completed"),
            () => Debug.Log("Save Failed"));
        }
    }
}
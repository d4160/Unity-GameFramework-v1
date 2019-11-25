namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine.GameFoundation.DataPersistence;
    using DataSerializerType = d4160.Systems.DataPersistence.DataSerializerType;

    public class DefaultDataLoader : DataLoader
    {
        public override void CreateDataPersistence()
        {
            IDataPersistence dataPersistence = null;
            IStorageHelper storageHelper = null;
            IDataSerializer serializer = null;

            m_dataSerializationAdapter = CreateSerializationAdapter();

            storageHelper = CreateStorageHelper(m_dataSerializationAdapter);
            serializer = CreateDataSerializer();

            switch (m_persistenceType)
            {
                case DataPersistenceType.PlayerPrefs:
                    dataPersistence = new DefaultPlayerPrefsPersistence(storageHelper, m_encrypted);
                break;

                case DataPersistenceType.Local:
                    dataPersistence = new DefaultLocalPersistence(serializer, m_encrypted, SaveToPlayerPrefs);
                break;

                case DataPersistenceType.Remote:
                break;
            }

            m_dataPersistence = dataPersistence;
        }

        public override void Initialize()
        {
            m_dataSerializationAdapter.Initialize(Identifier, m_dataPersistence);
        }

        public override void Load()
        {
            m_dataSerializationAdapter.Load(m_dataPersistence);
        }

        public override void Save()
        {
            m_dataSerializationAdapter.Save(m_dataPersistence);
        }

        protected override IStorageHelper CreateStorageHelper(IDataSerializationActions serializationAdapter)
        {
            IStorageHelper storageHelper = null;

            switch (m_persistenceType)
            {
                case DataPersistenceType.PlayerPrefs:
                    storageHelper = serializationAdapter.GetSerializableData() as IStorageHelper;
                    if(storageHelper != null)
                        storageHelper.StorageHelperType = StorageHelperType.PlayerPrefs;
                break;

                case DataPersistenceType.Local:
                break;

                case DataPersistenceType.Remote:
                break;
            }

            return storageHelper;
        }

        protected override IDataSerializationAdapter CreateSerializationAdapter()
        {
            IDataSerializationAdapter serializationAdapter = null;

            switch (m_persistenceType)
            {
                case DataPersistenceType.PlayerPrefs:
                    m_adapterType = DataSerializationAdapterType.Concrete;
                break;

                case DataPersistenceType.Local:
                    if (m_serializerType == DataSerializerType.JsonUtility)
                        m_adapterType = DataSerializationAdapterType.Concrete;
                break;

                case DataPersistenceType.Remote:
                    if (m_serializerType == DataSerializerType.JsonUtility)
                            m_adapterType = DataSerializationAdapterType.Concrete;
                break;
            }

            switch(m_persistenceTarget)
            {
                case DataPersistenceTarget.Game:
                    serializationAdapter = new DefaultGameDataSerializationAdapter(m_adapterType);
                break;

                case DataPersistenceTarget.Player:
                    serializationAdapter = new DefaultPlayerDataSerializationAdapter(m_adapterType);
                break;

                case DataPersistenceTarget.AppSettings:
                    serializationAdapter = new DefaultAppSettingsDataSerializationAdapter(m_adapterType);
                break;
            }

            return serializationAdapter;
        }

        protected override IDataSerializer CreateDataSerializer()
        {
            // choose what serializer you want to use
            IDataSerializer dataSerializer = null;
            switch(m_serializerType)
            {
                case DataSerializerType.JsonUtility:
                    dataSerializer = new JsonDataSerializer();
                break;
                case DataSerializerType.Odin:
                break;
            }

            return dataSerializer;
        }
    }
}
namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine;
#if ODIN_SERIALIZER
    using OdinSerializer;
#endif
    using UnityEngine.GameFoundation.DataPersistence;
    using DataSerializerType = d4160.Systems.DataPersistence.DataSerializerType;
    using NaughtyAttributes;

    public class DefaultAppSettingsDataLoader : DataLoader
    {
#if ODIN_SERIALIZER
        // Odin only
        [ShowIf(ConditionOperator.And, "IsDataPersistenceLocal", "IsSerializerOdin")]
        [SerializeField] protected DataFormat m_dataFormat = DataFormat.JSON;

        #region Editor Only
#if UNITY_EDITOR
        private bool IsSerializerOdin => m_serializerType == DataSerializerType.Odin;
#endif
        #endregion
#endif

        DefaultAppSettingsDataSerializationAdapter _dataSerializationAdapter;

        public override void Initialize()
        {
            // choose where and how the data is stored
            switch (m_persistenceType)
            {
                case DataPersistenceType.PlayerPrefs:
                    m_adapterType = DataSerializationAdapterType.Concrete;
                    _dataSerializationAdapter = new DefaultAppSettingsDataSerializationAdapter(m_adapterType);

                    var storageHelper = _dataSerializationAdapter.GetSerializableData() as IStorageHelper;
                    storageHelper.StorageHelperType = StorageHelperType.PlayerPrefs;

                    m_dataPersistence = new DefaultPlayerPrefsPersistence(storageHelper, m_encrypted);

                    // use the adapter to initialize
                    _dataSerializationAdapter.Initialize(Identifier, m_dataPersistence);
                break;
                case DataPersistenceType.Local:
                    if (m_serializerType == DataSerializerType.JsonUtility)
                        m_adapterType = DataSerializationAdapterType.Concrete;
                    _dataSerializationAdapter = new DefaultAppSettingsDataSerializationAdapter(m_adapterType);

                    var serializer = CreateDataSerializer();
                    m_dataPersistence = new DefaultLocalPersistence(serializer, m_encrypted, SaveToPlayerPrefs);

                    // use the adapter to initialize
                    _dataSerializationAdapter.Initialize(Identifier, m_dataPersistence);
                break;
                case DataPersistenceType.Remote:
                #if PLAYFAB
                    if (m_serializerType == DataSerializerType.JsonUtility)
                            m_adapterType = DataSerializationAdapterType.Concrete;
                    _dataSerializationAdapter = new DefaultAppSettingsDataSerializationAdapter(m_adapterType);

                    storageHelper = null;
                    if (!m_storageInOneEntry)
                    {
                        storageHelper = _dataSerializationAdapter.GetSerializableData() as IStorageHelper;
                        storageHelper.StorageHelperType = StorageHelperType.PlayFab;
                    }

                    serializer = CreateDataSerializer();

                    var loginProvider = new CustomIdLoginProvider(
                        m_remoteId,
                        (result) =>
                        {
                            Debug.Log("Login with CustomId success!");

                            // use the adapter to initialize
                            _dataSerializationAdapter.Initialize(Identifier, m_dataPersistence);
                        },
                        (error) =>
                        {
                            //Debug.LogWarning("Something went wrong with your first API call.  :(");
                            //Debug.LogError("Here's some debug information:");
                            //Debug.LogError(error.GenerateErrorReport());
                        }
                    );

                    m_dataPersistence = new DefaultPlayFabPersistence(
                        serializer, m_encrypted,
                        loginProvider, storageHelper
                    );
                    #endif
                break;
                default:
                break;
            }
        }

        protected IDataSerializer CreateDataSerializer()
        {
            // choose what serializer you want to use
            IDataSerializer dataSerializer = null;
            switch(m_serializerType)
            {
                case DataSerializerType.JsonUtility:
                    dataSerializer = new JsonDataSerializer();
                break;
                case DataSerializerType.Odin:
#if ODIN_SERIALIZER
                    dataSerializer = new OdinDataSerializer(m_dataFormat);
#endif
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
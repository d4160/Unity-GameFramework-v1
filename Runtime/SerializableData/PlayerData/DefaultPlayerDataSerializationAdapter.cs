namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class DefaultPlayerDataSerializationAdapter : IDataSerializationAdapter
    {
        protected DataSerializationAdapterType m_dataType;

        public DefaultPlayerDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic)
        {
            m_dataType = dataType;
        }

        public virtual void InitializeData(ISerializableData data)
        {
            if (data != null)
            {
                FillFromSerializableData(data);
            }
        }

        public virtual ISerializableData GetSerializableData()
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    var serializableData = new DefaultPlayerSerializableData();
                    var data = PlayerSettings.Database.PlayerData;
                    var serializableDataArray = new BasePlayerSerializableData[data.Length];

                    for (int i = 0; i < data.Length; i++)
                    {
                        serializableDataArray[i] = data[i].GetSerializableData() as BasePlayerSerializableData;
                    }

                    serializableData.PlayerData = serializableDataArray;

                    return serializableData;
                case DataSerializationAdapterType.Concrete:
                    return GetConcreteSerializableData();
                default:
                    return null;
            }
        }

        protected virtual ISerializableData GetConcreteSerializableData()
        {
            var cData = new DefaultConcretePlayerSerializableData();
            var data = PlayerSettings.Database.PlayerData;

            for (int i = 0; i < data.Length; i++)
            {
                switch(i)
                {
                    default:
                    break;
                    //case 0: cData.___Data = (data[i].GetSerializableData() as Default___SerializableData); break;
                }
            }

            return cData;
        }

        public virtual void FillFromSerializableData(ISerializableData data)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    var gData = data as DefaultPlayerSerializableData;

                    if (gData == null || gData.PlayerData == null)
                    {
                        Debug.LogWarning($"PlayerSerializableData is null. ");
                        return;
                    }

                    var playerData = PlayerSettings.Database.PlayerData;
                    for (int i = 0; i < gData.PlayerData.Length; i++)
                    {
                        if (!playerData.IsValidIndex(i)) break;

                        playerData[i].FillFromSerializableData(gData.PlayerData[i]);
                    }
                break;
                case DataSerializationAdapterType.Concrete:
                    FillConcreteTypeFromSerializableData(data);
                break;
            }
        }

        protected virtual void FillConcreteTypeFromSerializableData(ISerializableData data)
        {
            var cData = data as DefaultConcretePlayerSerializableData;

            if (cData == null)
            {
                Debug.LogWarning($"ConcretePlayerSerializableData is null. ");
                return;
            }

            var playerData = PlayerSettings.Database.PlayerData;

            for (int i = 0; i < playerData.Length; i++)
            {
                switch(i)
                {
                    default:
                    break;
                    //case 0: playerData[i].FillFromSerializableData(cData.DefaultData); break;
                }
            }
        }

        public virtual void Initialize(
            string saveDataPathFull,
            IDataPersistence dataPersistence,
            System.Action onInitializeCompleted = null,
            System.Action onInitializeFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework<DefaultAppSettingsSerializableData>.SetAppSettingsDataPath(saveDataPathFull);
                    // tell Game Framework to initialize using this
                    // persistence system. Only call Initialize once per session.
                    GameFramework<DefaultAppSettingsSerializableData>.InitializeAppSettings(dataPersistence, this, onInitializeCompleted, onInitializeFailed);
                break;
                case DataSerializationAdapterType.Concrete:
                    InitializeConcreteType(saveDataPathFull, dataPersistence, onInitializeCompleted, onInitializeFailed);
                break;
            }
        }

        protected virtual void InitializeConcreteType(
            string saveDataPathFull,
            IDataPersistence dataPersistence,
            System.Action onInitializeCompleted = null,
            System.Action onInitializeFailed = null)
        {
            GameFramework<DefaultConcretePlayerSerializableData>.SetAppSettingsDataPath(saveDataPathFull);
            // tell Game Framework to initialize using this
            // persistence system. Only call Initialize once per session.
            GameFramework<DefaultConcretePlayerSerializableData>.InitializeAppSettings(dataPersistence, this, onInitializeCompleted, onInitializeFailed);
        }

        public virtual void Load(
            IDataPersistence dataPersistence,
            System.Action onLoadCompleted = null,
            System.Action onLoadFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework<DefaultPlayerSerializableData>.LoadAppSettings(dataPersistence, onLoadCompleted, onLoadFailed);
                break;
                case DataSerializationAdapterType.Concrete:
                    LoadConcreteType(dataPersistence, onLoadCompleted, onLoadFailed);
                break;
            }
        }

        protected virtual void LoadConcreteType(
            IDataPersistence dataPersistence,
            System.Action onLoadCompleted = null,
            System.Action onLoadFailed = null)
        {
            GameFramework<DefaultConcretePlayerSerializableData>.LoadAppSettings(dataPersistence, onLoadCompleted, onLoadFailed);
        }

        public virtual void Save(
            IDataPersistence dataPersistence,
            System.Action onSaveCompleted = null,
            System.Action onSaveFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework<DefaultPlayerSerializableData>.SaveAppSettings(dataPersistence, onSaveCompleted, onSaveFailed);
                break;
                case DataSerializationAdapterType.Concrete:
                    SaveConcreteType(dataPersistence, onSaveCompleted, onSaveFailed);
                break;
            }
        }

        protected virtual void SaveConcreteType(
            IDataPersistence dataPersistence,
            System.Action onSaveCompleted = null,
            System.Action onSaveFailed = null)
        {
            GameFramework<DefaultConcretePlayerSerializableData>.SaveAppSettings(dataPersistence, onSaveCompleted, onSaveFailed);
        }
    }
}
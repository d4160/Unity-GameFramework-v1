namespace d4160.GameFramework
{
    using d4160.Core;
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public class DefaultGameDataSerializationAdapter : IDataSerializationAdapter
    {
        protected DataSerializationAdapterType m_dataType;

        public DefaultGameDataSerializationAdapter(DataSerializationAdapterType dataType = DataSerializationAdapterType.Generic)
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
                    var serializableData = new DefaultGameSerializableData();
                    var data = GameFrameworkSettings.PlayerDatabase.PlayerData;
                    var serializableDataArray = new BaseGameSerializableData[data.Length];

                    for (int i = 0; i < data.Length; i++)
                    {
                        serializableDataArray[i] = data[i].GetSerializableData() as BaseGameSerializableData;
                    }

                    serializableData.GameData = serializableDataArray;

                    return serializableData;
                case DataSerializationAdapterType.Concrete:
                    return GetConcreteSerializableData();
                default:
                    return null;
            }
        }

        protected virtual ISerializableData GetConcreteSerializableData()
        {
            var cData = new DefaultConcreteGameSerializableData();
            var data = GameFrameworkSettings.GameDatabase.GameData;

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
                    var gData = data as DefaultGameSerializableData;

                    if (gData == null || gData.GameData == null)
                    {
                        Debug.LogWarning($"GameSerializableData is null. ");
                        return;
                    }

                    var GameData = GameFrameworkSettings.GameDatabase.GameData;
                    for (int i = 0; i < gData.GameData.Length; i++)
                    {
                        if (!GameData.IsValidIndex(i)) break;

                        GameData[i].FillFromSerializableData(gData.GameData[i]);
                    }
                break;
                case DataSerializationAdapterType.Concrete:
                    FillConcreteTypeFromSerializableData(data);
                break;
            }
        }

        protected virtual void FillConcreteTypeFromSerializableData(ISerializableData data)
        {
            var cData = data as DefaultConcreteGameSerializableData;

            if (cData == null)
            {
                Debug.LogWarning($"ConcreteGameSerializableData is null. ");
                return;
            }

            var GameData = GameFrameworkSettings.GameDatabase.GameData;

            for (int i = 0; i < GameData.Length; i++)
            {
                switch(i)
                {
                    default:
                    break;
                    //case 0: GameData[i].FillFromSerializableData(cData.DefaultData); break;
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
                    GameFramework.SetGameDataPath(saveDataPathFull);
                    // tell Game Framework to initialize using this
                    // persistence system. Only call Initialize once per session.
                    GameFramework.InitializeGameData<DefaultAppSettingsSerializableData>(dataPersistence, this, onInitializeCompleted, onInitializeFailed);
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
            GameFramework.SetGameDataPath(saveDataPathFull);
            // tell Game Framework to initialize using this
            // persistence system. Only call Initialize once per session.
            GameFramework.InitializeGameData<DefaultConcreteGameSerializableData>(dataPersistence, this, onInitializeCompleted, onInitializeFailed);
        }

        public virtual void Load(
            IDataPersistence dataPersistence,
            System.Action onLoadCompleted = null,
            System.Action onLoadFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework.LoadGameData<DefaultGameSerializableData>(dataPersistence, onLoadCompleted, onLoadFailed);
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
            GameFramework.LoadGameData<DefaultConcreteGameSerializableData>(dataPersistence, onLoadCompleted, onLoadFailed);
        }

        public virtual void Save(
            IDataPersistence dataPersistence,
            System.Action onSaveCompleted = null,
            System.Action onSaveFailed = null)
        {
            switch(m_dataType)
            {
                case DataSerializationAdapterType.Generic:
                    GameFramework.SaveGameData(dataPersistence, onSaveCompleted, onSaveFailed);
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
            GameFramework.SaveGameData(dataPersistence, onSaveCompleted, onSaveFailed);
        }
    }
}
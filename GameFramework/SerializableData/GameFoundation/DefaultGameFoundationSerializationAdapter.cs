namespace d4160.GameFramework
{
    using d4160.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation;
    using UnityEngine.GameFoundation.DataPersistence;

    public class GameFoundationSerializationAdapter : IDataSerializationAdapter
    {
        public GameFoundationSerializationAdapter()
        {
        }

        public virtual void FillFromSerializableData(ISerializableData data)
        {
        }

        public virtual ISerializableData GetSerializableData()
        {
            return null;
        }

        public virtual void InitializeData(ISerializableData data)
        {
        }

        public virtual void Initialize(
            string saveDataPathFull,
            IDataPersistence dataPersistence,
            System.Action onInitializeCompleted = null,
            System.Action onInitializeFailed = null)
        {
            GameFoundation.Initialize(dataPersistence, onInitializeCompleted, onInitializeFailed);
        }

        public virtual void Uninitialize()
        {
        }

        public virtual void Load(
            IDataPersistence dataPersistence,
            System.Action onLoadCompleted = null,
            System.Action onLoadFailed = null)
        {
            GameFoundation.Load(dataPersistence, onLoadCompleted, (e) => onLoadFailed.Invoke());
        }

        public virtual void Save(
            IDataPersistence dataPersistence,
            System.Action onSaveCompleted = null,
            System.Action onSaveFailed = null)
        {
            GameFoundation.Save(dataPersistence, onSaveCompleted, (e) => onSaveFailed.Invoke());
        }
    }
}
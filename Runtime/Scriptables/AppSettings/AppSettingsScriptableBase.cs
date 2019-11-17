namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public abstract class AppSettingsScriptableBase : ScriptableObject, IDataSerializationAdapter
    {
        public abstract ISerializableData GetSerializableData();

        public abstract void FillFromSerializableData(ISerializableData data);

        public abstract void Initialize(ISerializableData data);
    }
}
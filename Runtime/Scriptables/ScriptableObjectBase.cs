namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using Malee;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    public abstract class ScriptableObjectBase : ScriptableObject, IDataSerializationActions
    {
        public abstract ISerializableData GetSerializableData();

        public virtual void InitializeData(ISerializableData data)
        {
            if (data != null)
            {
                FillFromSerializableData(data);
            }
        }

        public abstract void FillFromSerializableData(ISerializableData data);
    }

    public abstract class ScriptableObjectBase<T> : ScriptableObjectBase where T : class, ISerializableData
    {
        public override ISerializableData GetSerializableData()
        {
            return GetSerializableDataGeneric();
        }

        protected abstract T GetSerializableDataGeneric();

        public override void FillFromSerializableData(ISerializableData data)
        {
            FillFromSerializableData(data as T);
        }

        protected abstract void FillFromSerializableData(T data);
    }

    [System.Serializable]
    public class ScriptableObjectReorderableArray : ReorderableArrayForUnityObject<ScriptableObjectBase>
    {
    }
}
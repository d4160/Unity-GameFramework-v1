namespace d4160.GameFramework
{
    using UnityEngine.GameFoundation.DataPersistence;

    public interface  IGenericSerialiableData : ISerializableData
    {
        BaseSerializableData[] SerializableData { get; set; }
    }
}
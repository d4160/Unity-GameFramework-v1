namespace d4160.Systems.DataPersistence
{
    using UnityEngine.GameFoundation.DataPersistence;

    [System.Serializable]
    public abstract class BaseSerializableData : ISerializableData
    {
        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public BaseSerializableData()
        {
        }
    }
}
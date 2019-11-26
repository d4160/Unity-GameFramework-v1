namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    /// <summary>
    /// Don't use JsonUtility to serialize this class since there is an array with abstract type,
    /// which JsonUtility can't support -> https://answers.unity.com/questions/1301570/doesnt-jsonutility-support-arrays-with-abstract-ty.html
    /// </summary>
    [System.Serializable]
    public class DefaultPlayerSerializableData : IGenericSerialiableData
    {
        [SerializeField] protected BaseSerializableData[] m_playerData;

        public BaseSerializableData[] SerializableData { get => m_playerData; set => m_playerData = value; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultPlayerSerializableData()
        {
        }
    }

    /// <summary>
    /// Inherited from this class to allow another lists
    /// </summary>
    [System.Serializable]
    public class DefaultConcretePlayerSerializableData : ISerializableData, IStorageHelper
    {
        protected StorageHelperType m_storageHelperType;
        protected int m_completedCount;

        public StorageHelperType StorageHelperType
        {
            get => m_storageHelperType;
            set
            {
                m_storageHelperType = value;
                /* Subtypes set */
            }
        }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultConcretePlayerSerializableData()
        {

        }

        public virtual void Save(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            m_completedCount = 0;
        }

        protected virtual void OnCompleted(System.Action onCompleted)
        {
            m_completedCount++;

            if (m_completedCount >= 5)
            {
                onCompleted?.Invoke();
            }
        }
    }
}
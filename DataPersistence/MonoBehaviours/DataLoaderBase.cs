namespace d4160.Systems.DataPersistence
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;
    using UnityExtensions;
    using NaughtyAttributes;

    public abstract class DataLoaderBase : MonoBehaviour
    {
        [InspectInline]
        [SerializeField] protected AuthenticatorControllerBase m_authenticator;
        [SerializeField] protected DataPersistenceTarget m_persistenceTarget = DataPersistenceTarget.AppSettings;
        [SerializeField] protected DataPersistenceType m_persistenceType = DataPersistenceType.Local;
        [SerializeField] protected bool m_encrypted;

        [ShowIf("IsDataPersistenceLocal")]
        [SerializeField] protected SaveDataPath m_saveDataFolder;

        [ShowIf(ConditionOperator.And, "IsDataPersistenceLocal", "IsNotSavePathPlayerPrefs")]
        [SerializeField] protected string m_fileName;

        [ShowIf(ConditionOperator.And, "IsDataPersistenceLocal", "IsNotSavePathPlayerPrefs")]
        [SerializeField] protected string m_fileExtension;

        [ShowIf("IsDataPersistenceRemote")]
        [SerializeField] protected RemotePersistenceType m_remotePersistenceType = RemotePersistenceType.PlayFab;

        [ShowIf("IsDataPersistenceRemote")]
        [Tooltip("Otherwise split in many entries like playerprefs")]
        [SerializeField] protected bool m_remoteStorageInOneEntry;

        [ShowIf("AvailableToUseKey")]
        [SerializeField] protected string m_key;

        [ShowIf("AvailableToUseSerializer")]
        [SerializeField] protected DataSerializerType m_serializerType = DataSerializerType.Odin;

        [ShowIf("AvailableToChoiceAdapter")]
        [SerializeField] protected DataSerializationAdapterType m_adapterType = DataSerializationAdapterType.Generic;

        protected IDataPersistence m_dataPersistence;
        protected IDataSerializationAdapter m_dataSerializationAdapter;

        #region Editor Only
#if UNITY_EDITOR
        protected bool IsNotDataPersistenceTargetGameFoundation => m_persistenceTarget != DataPersistenceTarget.GameFoundation;
        protected bool IsDataPersistenceLocaOrRemote => IsDataPersistenceLocal || IsDataPersistenceRemote;
        protected bool AvailableToUseSerializer => (IsDataPersistenceRemote && m_remoteStorageInOneEntry)
                                            || (IsDataPersistenceLocal);

        protected bool AvailableToChoiceAdapter => IsNotDataPersistenceTargetGameFoundation &&
                                            IsNotSerializerJsonUtility &&
                                            (IsDataPersistenceLocal ||
                                            (IsDataPersistenceRemote && m_remoteStorageInOneEntry));
#endif
        #endregion

        protected bool IsDataPersistenceLocal => m_persistenceType == DataPersistenceType.Local;
        protected bool IsDataPersistenceRemote => m_persistenceType == DataPersistenceType.Remote;
        protected bool IsNotSerializerJsonUtility => m_serializerType != DataSerializerType.JsonUtility;
        protected bool IsNotSavePathPlayerPrefs => m_saveDataFolder != SaveDataPath.PlayerPrefs;
        protected bool AvailableToUseKey => (!IsNotSavePathPlayerPrefs && IsDataPersistenceLocal)
                                            || (IsDataPersistenceRemote && m_remoteStorageInOneEntry);

        protected string Identifier
        {
            get
            {
                if (AvailableToUseKey)
                    return m_key;
                else
                    return DataPath.GetPath(m_saveDataFolder, m_fileName, m_fileExtension);
            }
        }

        protected bool SaveToPlayerPrefs => m_saveDataFolder == SaveDataPath.PlayerPrefs;

        protected string DataPersistenceString => $"for {m_persistenceTarget}, as {m_persistenceType}, encrypted({m_encrypted})";

        public AuthenticatorControllerBase Authenticator => m_authenticator;

        public void CreateDataPersistenceAndInitialize()
        {
            CreateDataPersistence();
            Initialize();
        }

        public abstract void CreateDataPersistence();

        public abstract void Initialize();

        public abstract void Uninitialize();

        public abstract void Save();

        public abstract void Load();

        protected virtual IStorageHelper CreateStorageHelper(IDataSerializationAdapter serializationAdapter)
        {
            return null;
        }

        protected virtual IDataSerializationAdapter CreateSerializationAdapter()
        {
            return null;
        }

        protected virtual IDataSerializer CreateDataSerializer()
        {
            return null;
        }
    }
}
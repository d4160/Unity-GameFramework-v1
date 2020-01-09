namespace d4160.DataPersistence
{
    public enum DataPersistenceTarget
    {
      Game,
      Player,
      AppSettings,
      GameFoundation
    }

    public enum DataPersistenceType
    {
      PlayerPrefs,
      Local,
      Remote
    }

    public enum AuthenticationType
    {
      Local,
      Remote
    }

    public enum DataSerializerType
    {
      JsonUtility,
      Odin
    }

    public enum DataSerializationAdapterType
    {
      Generic,
      Concrete
    }

    public enum StorageHelperType
    {
      PlayerPrefs = 0,
      PlayFab
    }

    public enum RemotePersistenceType
    {
      PlayFab = 1
    }
}
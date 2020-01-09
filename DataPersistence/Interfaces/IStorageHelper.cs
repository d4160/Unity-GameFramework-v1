namespace d4160.DataPersistence
{
    public interface IStorageHelper
    {
        StorageHelperType StorageHelperType { get; set; }

        void Save(bool encrypted = false, System.Action onCompleted = null);

        void Load(bool encrypted = false, System.Action onCompleted = null);
    }
}
﻿namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;

    [System.Serializable]
    public class DefaultLevelCategoriesSerializableData : BaseSerializableData, IStorageHelper
    {
        public DefaultSerializableLevelCategory[] levelCategories;

        public StorageHelperType StorageHelperType { get; set; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultLevelCategoriesSerializableData() : base()
        {
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
                    if (encrypted)
                    {
                    }
                    else
                    {
                    }

                    onCompleted?.Invoke();
                break;

                case StorageHelperType.PlayFab:
                    LoadForPlayFab(encrypted, onCompleted);
                break;
            }
        }

        protected virtual void LoadForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {
        }

        public virtual void Save(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
                    if (encrypted)
                    {
                    }
                    else
                    {
                    }

                    onCompleted?.Invoke();
                break;

                case StorageHelperType.PlayFab:
                #if PLAYFAB
                    SaveForPlayFab(encrypted, onCompleted);
                    #endif
                break;
            }
        }

        protected virtual void SaveForPlayFab(bool encrypted = false, System.Action onCompleted = null)
        {

        }
    }
}
  namespace d4160.GameFramework
{
  using System.Collections.Generic;
  using d4160.DataPersistence;

    [System.Serializable]
    public class DefaultPlayTrialsSerializableData : BaseSerializableData, IStorageHelper
    {
        public DefaultPlayTrial[] playTrials;

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultPlayTrialsSerializableData() : base()
        {
        }

        public DefaultPlayTrialsSerializableData(DefaultPlayTrial[] elements) : base()
        {
            playTrials = elements;
        }

        public StorageHelperType StorageHelperType { get; set; }

        public void Add(DefaultPlayTrial element)
        {
            var list = new List<DefaultPlayTrial>(playTrials);
            list.Add(element);

            playTrials = list.ToArray();
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
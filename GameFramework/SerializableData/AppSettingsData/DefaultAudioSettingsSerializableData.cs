namespace d4160.GameFramework
{
    using UnityEngine;
    using d4160.DataPersistence;
    using System.Collections.Generic;

    [System.Serializable]
    public class DefaultAudioSettingsSerializableData : BaseSerializableData, IStorageHelper
    {
        public bool music;
        public float musicVolume;
        public bool sfxs;
        public float sfxsVolume;

        public StorageHelperType StorageHelperType { get; set; }

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultAudioSettingsSerializableData() : base()
        {
        }

        public virtual void Load(bool encrypted = false, System.Action onCompleted = null)
        {
            switch (StorageHelperType)
            {
                case StorageHelperType.PlayerPrefs:
                    if (encrypted)
                    {
                        music = PlayerPrefsUtility.GetEncryptedBool(nameof(music), music);
                        musicVolume = PlayerPrefsUtility.GetEncryptedFloat(nameof(musicVolume), musicVolume);
                        sfxs = PlayerPrefsUtility.GetEncryptedBool(nameof(sfxs), sfxs);
                        sfxsVolume = PlayerPrefsUtility.GetEncryptedFloat(nameof(sfxsVolume), sfxsVolume);
                    }
                    else
                    {
                        music = PlayerPrefsUtility.GetBool(nameof(music), music);
                        musicVolume = PlayerPrefs.GetFloat(nameof(musicVolume), musicVolume);
                        sfxs = PlayerPrefsUtility.GetBool(nameof(sfxs), sfxs);
                        sfxsVolume = PlayerPrefs.GetFloat(nameof(sfxsVolume), sfxsVolume);
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
                        PlayerPrefsUtility.SetEncryptedBool(nameof(music), music);
                        PlayerPrefsUtility.SetEncryptedFloat(nameof(musicVolume), musicVolume);
                        PlayerPrefsUtility.SetEncryptedBool(nameof(sfxs), sfxs);
                        PlayerPrefsUtility.SetEncryptedFloat(nameof(sfxsVolume), sfxsVolume);
                    }
                    else
                    {
                        PlayerPrefsUtility.SetBool(nameof(music), music);
                        PlayerPrefs.SetFloat(nameof(musicVolume), musicVolume);
                        PlayerPrefsUtility.SetBool(nameof(sfxs), sfxs);
                        PlayerPrefs.SetFloat(nameof(sfxsVolume), sfxsVolume);
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
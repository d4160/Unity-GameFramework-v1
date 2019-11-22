namespace d4160.GameFramework
{
    using UnityEngine;
    using d4160.Systems.DataPersistence;
    #if PLAYFAB
  using PlayFab;
  using PlayFab.ClientModels;
  #endif
  using System.Collections.Generic;

  [System.Serializable]
    public class DefaultAudioSettingsSerializableData : BaseSettingsSerializableData, IStorageHelper
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

        public void Load(bool encrypted = false, System.Action onCompleted = null)
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
                #if PLAYFAB
                    PlayFabClientAPI.GetUserData(new GetUserDataRequest() {
                        Keys = null
                    }, result => {
                        if (result.Data != null && result.Data.ContainsKey(nameof(music)))
                        {
                            music = bool.Parse(result.Data[nameof(music)].Value);
                            musicVolume = float.Parse(result.Data[nameof(musicVolume)].Value);
                            sfxs = bool.Parse(result.Data[nameof(sfxs)].Value);
                            sfxsVolume = float.Parse(result.Data[nameof(sfxsVolume)].Value);
                        }

                        onCompleted?.Invoke();
                    }, null);
                    #endif
                break;
            }
        }

        public void Save(bool encrypted = false, System.Action onCompleted = null)
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
                    PlayFabClientAPI.UpdateUserData(new UpdateUserDataRequest() {
                        Data = new Dictionary<string, string>() {
                            { nameof(music), music.ToString() },
                            { nameof(musicVolume), musicVolume.ToString() },
                            { nameof(sfxs), sfxs.ToString() },
                            { nameof(sfxsVolume), sfxsVolume.ToString() }
                        }
                    }, (result) => onCompleted?.Invoke(), null);
                    #endif
                break;
            }
        }
    }
}
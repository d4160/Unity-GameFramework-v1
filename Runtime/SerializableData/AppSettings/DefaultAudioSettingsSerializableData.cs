namespace d4160.GameFramework
{
    using UnityEngine;
    using d4160.Systems.DataPersistence;

    [System.Serializable]
    public class DefaultAudioSettingsSerializableData : SettingsSerializableDataBase, IPlayerPrefsActions
    {
        public bool music;
        public float musicVolume;
        public bool sfxs;
        public float sfxsVolume;

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultAudioSettingsSerializableData() : base()
        {
        }

        public void Load(bool encrypted = false)
        {
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
        }

        public void Save(bool encrypted = false)
        {
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
        }
    }
}
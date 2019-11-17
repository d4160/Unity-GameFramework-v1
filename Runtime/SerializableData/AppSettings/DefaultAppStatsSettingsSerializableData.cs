namespace d4160.GameFramework
{
    using d4160.Systems.DataPersistence;

    [System.Serializable]
    public class DefaultAppStatsSettingsSerializableData : SettingsSerializableDataBase, IPlayerPrefsActions
    {
        public bool fps;
        public bool ram;
        public bool audio;
        public bool advancedInfo;

        /// <summary>
        /// Default constructor for serialization purpose.
        /// </summary>
        public DefaultAppStatsSettingsSerializableData() : base()
        {
        }

        public void Load(bool encrypted = false)
        {
            if (encrypted)
            {
                fps = PlayerPrefsUtility.GetEncryptedBool(nameof(fps), fps);
                ram = PlayerPrefsUtility.GetEncryptedBool(nameof(ram), ram);
                audio = PlayerPrefsUtility.GetEncryptedBool(nameof(audio), audio);
                advancedInfo = PlayerPrefsUtility.GetEncryptedBool(nameof(advancedInfo), advancedInfo);
            }
            else
            {
                fps = PlayerPrefsUtility.GetBool(nameof(fps), fps);
                ram = PlayerPrefsUtility.GetBool(nameof(ram), ram);
                audio = PlayerPrefsUtility.GetBool(nameof(audio), audio);
                advancedInfo = PlayerPrefsUtility.GetBool(nameof(advancedInfo), advancedInfo);
            }
        }

        public void Save(bool encrypted = false)
        {
            if (encrypted)
            {
                PlayerPrefsUtility.SetEncryptedBool(nameof(fps), fps);
                PlayerPrefsUtility.SetEncryptedBool(nameof(ram), ram);
                PlayerPrefsUtility.SetEncryptedBool(nameof(audio), audio);
                PlayerPrefsUtility.SetEncryptedBool(nameof(advancedInfo), advancedInfo);
            }
            else
            {
                PlayerPrefsUtility.SetBool(nameof(fps), fps);
                PlayerPrefsUtility.SetBool(nameof(ram), ram);
                PlayerPrefsUtility.SetBool(nameof(audio), audio);
                PlayerPrefsUtility.SetBool(nameof(advancedInfo), advancedInfo);
            }
        }
    }
}
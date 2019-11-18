namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultAudio Settings_SO.asset", menuName = "Game Framework/App Settings/Default Audio")]
    public class DefaultAudioSettings : AppSettingsScriptableBase
    {
        [SerializeField] protected bool m_music;
        [Range(0, 1f)]
        [SerializeField] protected float m_musicVolume;
        [SerializeField] protected bool m_sfxs;
        [Range(0, 1f)]
        [SerializeField] protected float m_sfxsVolume;

        public override ISerializableData GetSerializableData()
        {
            var data = new DefaultAudioSettingsSerializableData();
            data.music = m_music;
            data.musicVolume = m_musicVolume;
            data.sfxs = m_sfxs;
            data.sfxsVolume = m_sfxsVolume;

            return data;
        }

        public override void Initialize(ISerializableData data)
        {
            if (data != null)
            {
                FillFromSerializableData(data);
            }
        }

        public override void FillFromSerializableData(ISerializableData data)
        {
            var appStatsData = data as DefaultAudioSettingsSerializableData;
            m_music = appStatsData.music;
            m_musicVolume = appStatsData.musicVolume;
            m_sfxs = appStatsData.sfxs;
            m_sfxsVolume = appStatsData.sfxsVolume;
        }
    }
}
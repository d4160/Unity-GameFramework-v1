namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultAudio Settings_SO.asset", menuName = "Game Framework/App Settings/Default Audio")]
    public class DefaultAudioSettings : ScriptableObjectBase<DefaultAudioSettingsSerializableData>
    {
        [SerializeField] protected bool m_music;
        [Range(0, 1f)]
        [SerializeField] protected float m_musicVolume;
        [SerializeField] protected bool m_sfxs;
        [Range(0, 1f)]
        [SerializeField] protected float m_sfxsVolume;

        protected override DefaultAudioSettingsSerializableData GetSerializableDataGeneric()
        {
            var data = new DefaultAudioSettingsSerializableData();
            data.music = m_music;
            data.musicVolume = m_musicVolume;
            data.sfxs = m_sfxs;
            data.sfxsVolume = m_sfxsVolume;

            return data;
        }

        protected override void FillFromSerializableData(DefaultAudioSettingsSerializableData data)
        {
            m_music = data.music;
            m_musicVolume = data.musicVolume;
            m_sfxs = data.sfxs;
            m_sfxsVolume = data.sfxsVolume;
        }
    }
}
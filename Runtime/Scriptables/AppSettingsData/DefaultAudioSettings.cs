namespace d4160.GameFramework
{
    using UnityEngine;
    using UnityEngine.GameFoundation.DataPersistence;

    //[CreateAssetMenu(fileName = "New DefaultAudio Settings_SO.asset", menuName = "Game Framework/App Settings/Default Audio")]
    public abstract class DefaultAudioSettings<T> : ScriptableObjectBase<T> where T :BaseSerializableData
    {
        [SerializeField] protected bool m_music;
        [Range(0, 1f)]
        [SerializeField] protected float m_musicVolume;
        [SerializeField] protected bool m_sfxs;
        [Range(0, 1f)]
        [SerializeField] protected float m_sfxsVolume;
    }
}
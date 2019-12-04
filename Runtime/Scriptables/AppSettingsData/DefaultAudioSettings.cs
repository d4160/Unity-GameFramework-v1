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

        public virtual bool Music 
        {
            get => m_music;
            set 
            {
                m_music = value;
                ApplyMusic();
            }
        }

        public virtual float MusicVolume 
        {
            get => m_musicVolume;
            set 
            {
                m_musicVolume = value;
                ApplyMusicVolume();
            }
        }

        public virtual bool Sfxs 
        {
            get => m_sfxs;
            set 
            {
                m_sfxs = value;
                ApplySfxs();
            }
        }

        public virtual float SfxsVolume 
        {
            get => m_sfxsVolume;
            set 
            {
                m_sfxsVolume = value;
                ApplySfxsVolume();
            }
        }

        public virtual void ApplyMusic(){}
        public virtual void ApplyMusicVolume(){}
        public virtual void ApplySfxs(){}
        public virtual void ApplySfxsVolume(){}
    }
}
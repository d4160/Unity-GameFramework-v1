namespace d4160.GameFramework
{
    using UnityEngine;
    using d4160.DataPersistence;

    //[CreateAssetMenu(fileName = "New DefaultAppStats Settings_SO.asset", menuName = "Game Framework/App Settings/Default AppStats")]
    public abstract class DefaultAppStatsSettings<T> : ScriptableObjectBase<T> where T : BaseSerializableData
    {
        [SerializeField] protected bool m_fps;
        [SerializeField] protected bool m_ram;
        [SerializeField] protected bool m_audio;
        [SerializeField] protected bool m_advancedInfo;

        public virtual bool Fps
        {
            get => m_fps;
            set
            {
                m_fps = value;
                ApplyFps();
            }
        }

        public virtual bool Ram
        {
            get => m_ram;
            set
            {
                m_ram = value;
                ApplyRam();
            }
        }

        public virtual bool Audio
        {
            get => m_audio;
            set
            {
                m_audio = value;
                ApplyAudio();
            }
        }

        public virtual bool AdvancedInfo
        {
            get => m_advancedInfo;
            set
            {
                m_advancedInfo = value;
                ApplyAdvancedInfo();
            }
        }

        public virtual void ApplyFps(){}
        public virtual void ApplyRam(){}
        public virtual void ApplyAudio(){}
        public virtual void ApplyAdvancedInfo(){}
    }
}
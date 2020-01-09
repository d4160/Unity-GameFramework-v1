namespace d4160.GameFramework
{
    using UnityEngine;
    using DataPersistence;

    //[CreateAssetMenu(fileName = "New DefaultPostProcessing Settings_SO.asset", menuName = "Game Framework/App Settings/Default PostProcessing")]
    public abstract class DefaultAccesibilitySettings<T> : ScriptableObjectBase<T> where T : BaseSerializableData
    {
        [Range(0.5f, 1.5f)]
        [SerializeField] protected float m_uiScale = .5f;
        [SerializeField] protected bool m_subtitles;

        public virtual float UIScale
        {
            get => m_uiScale;
            set
            {
                m_uiScale = value;
                ApplyUIScale();
            }
        }

        public virtual bool Subtitles
        {
            get => m_subtitles;
            set
            {
                m_subtitles = value;
                ApplySubtitles();
            }
        }

        public virtual void ApplyUIScale(){}
        public virtual void ApplySubtitles(){}
    }
}
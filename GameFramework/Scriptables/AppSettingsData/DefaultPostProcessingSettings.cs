namespace d4160.GameFramework
{
    using UnityEngine;
    using d4160.Systems.DataPersistence;

    //[CreateAssetMenu(fileName = "New DefaultPostProcessing Settings_SO.asset", menuName = "Game Framework/App Settings/Default PostProcessing")]
    public abstract class DefaultPostProcessingSettings<T> : ScriptableObjectBase<T> where T : BaseSerializableData
    {
        [SerializeField] protected bool m_bloom;
        [SerializeField] protected bool m_colorGrading;
        [SerializeField] protected bool m_vignette;

        public virtual bool Bloom
        {
            get => m_bloom;
            set
            {
                m_bloom = value;
                ApplyBloom();
            }
        }

        public virtual bool ColorGrading
        {
            get => m_colorGrading;
            set
            {
                m_colorGrading = value;
                ApplyColorGrading();
            }
        }

        public virtual bool Vignette
        {
            get => m_vignette;
            set
            {
                m_vignette = value;
                ApplyVignette();
            }
        }

        public virtual void ApplyBloom(){}
        public virtual void ApplyColorGrading(){}
        public virtual void ApplyVignette(){}
    }
}
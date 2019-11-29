namespace d4160.GameFramework
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "New DefaultPostProcessing Settings_SO.asset", menuName = "Game Framework/App Settings/Default PostProcessing")]
    public abstract class DefaultPostProcessingSettings<T> : ScriptableObjectBase<T> where T : BaseSerializableData
    {
        [SerializeField] protected bool m_bloom;
        [SerializeField] protected bool m_colorGrading;
        [SerializeField] protected bool m_vignette;
    }
}
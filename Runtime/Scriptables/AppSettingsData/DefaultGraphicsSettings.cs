namespace d4160.GameFramework
{
    using UnityEngine;
    using d4160.Core;
    using d4160.Core.Attributes;
    using DG.DeInspektor.Attributes;
    using UnityEngine.GameFoundation.DataPersistence;

    [CreateAssetMenu(fileName = "New DefaultGraphics Settings_SO.asset", menuName = "Game Framework/App Settings/Default Graphics")]
    public abstract class DefaultGraphicsSettings<T> : ScriptableObjectBase<T> where T : BaseSerializableData
    {
        [Dropdown(ValuesProperty = "Resolutions")]
        [SerializeField] protected int m_resolution;
        [SerializeField] protected FullScreenMode m_fullScreenMode;
        [Dropdown(ValuesProperty = "Qualities")]
        [SerializeField] protected int m_qualityLevel;
        [Popup("Don't Sync","Every V Blank","Every Second V Blank", IsIndexProperty = true)]
        [SerializeField] protected int m_vSyncCount;

        #region Editor Use
#if UNITY_EDITOR
        protected string[] Resolutions
        {
            get
            {
                var resolutions = Screen.resolutions;
                var values = new string[resolutions.Length];
                for (int i = 0; i < resolutions.Length; i++)
                {
                    values[i] = $"{resolutions[i].width}x{resolutions[i].height}";
                }

                return values;
            }
        }

        protected string[] Qualities => QualitySettings.names;
#endif
        #endregion

        public int ResolutionIndex
        {
            get => m_resolution;
            set
            {
                if(Screen.resolutions.IsValidIndex(value))
                    m_resolution = value;
                else
                    m_resolution = Screen.resolutions.Length - 1;
            }
        }
    }
}
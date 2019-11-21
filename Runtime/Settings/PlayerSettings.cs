namespace d4160.GameFramework
{
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityExtensions;

    public class PlayerSettings : ScriptableObject
    {
        /// <summary>
        /// The directory name where Unity project assets will be created/stored.
        /// </summary>
        public static readonly string kAssetsFolder = "GameFramework";

        private static PlayerSettings s_Instance;
        internal static PlayerSettings Instance
        {
            get
            {
                bool assetUpdate = false;

                if (s_Instance == null)
                {
                    s_Instance = Resources.Load<PlayerSettings>("PlayerSettings");

#if UNITY_EDITOR
                    if (s_Instance == null && !Application.isPlaying)
                    {
                        s_Instance = ScriptableObject.CreateInstance<PlayerSettings>();

                        if (!AssetDatabase.IsValidFolder(string.Format("Assets/{0}", kAssetsFolder)))
                        {
                            AssetDatabase.CreateFolder("Assets", kAssetsFolder);
                        }

                        if (!AssetDatabase.IsValidFolder(string.Format("Assets/{0}/Resources", kAssetsFolder)))
                        {
                            AssetDatabase.CreateFolder(string.Format("Assets/{0}", kAssetsFolder), "Resources");
                        }

                        AssetDatabase.CreateAsset(s_Instance, string.Format("Assets/{0}/Resources/PlayerSettings.asset", kAssetsFolder));
                        assetUpdate = true;

                        s_Instance = Resources.Load<PlayerSettings>("PlayerSettings");
                    }
#endif

                    if (s_Instance == null)
                    {
                        throw new System.InvalidOperationException("Unable to find or create a PlayerSettings resource!");
                    }
                }

#if UNITY_EDITOR
                if (s_Instance.m_Database == null)
                {
                    string databaseAssetPath = $"Assets/{kAssetsFolder}/PlayerSettingsDatabase.asset";

                    // try to load a database asset by hardcoded path
                    s_Instance.m_Database = AssetDatabase.LoadAssetAtPath<PlayerDatabase>(databaseAssetPath);

                    // if that doesn't work, then create one
                    if (s_Instance.m_Database == null)
                    {
                        s_Instance.m_Database = ScriptableObject.CreateInstance<PlayerDatabase>();

                        if (!AssetDatabase.IsValidFolder(string.Format("Assets/{0}", kAssetsFolder)))
                        {
                            AssetDatabase.CreateFolder("Assets", kAssetsFolder);
                        }

                        AssetDatabase.CreateAsset(s_Instance.m_Database, databaseAssetPath);
                        EditorUtility.SetDirty(s_Instance);
                        assetUpdate = true;
                    }
                }

                if (assetUpdate)
                {
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                }
#else
                if (s_Instance.m_Database == null)
                {
                    throw new System.Exception("Player database reference cannot be null."
                        + "Open one of the Game Framework windows in the Unity Editor while not in Play Mode to have a database asset created for you automatically.");
                }
#endif

                return s_Instance;
            }
        }

        [SerializeField]
        [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
        private PlayerDatabase m_Database;

        public static PlayerDatabase Database
        {
            get { return Instance.m_Database; }
            set { Instance.m_Database = value; }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Set GameFoundationSettings asset file.
        /// </summary>
        [MenuItem("Window/Game Framework/PlayerSettings", false, 2030)]
        public static void SelectGameFoundationSettingsAssetFile()
        {
            Selection.SetActiveObjectWithContext(Instance, null);
        }
#endif
    }
}
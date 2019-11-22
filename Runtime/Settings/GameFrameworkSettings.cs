namespace d4160.GameFramework
{
    using UnityEngine;
#if UNITY_EDITOR
    using UnityEditor;
#endif
    using UnityExtensions;

    public class GameFrameworkSettings : ScriptableObject
    {
        /// <summary>
        /// The directory name where Unity project assets will be created/stored.
        /// </summary>
        public static readonly string kAssetsFolder = "GameFramework";

        private static GameFrameworkSettings s_Instance;
        internal static GameFrameworkSettings Instance
        {
            get
            {
                bool assetUpdate = false;

                if (s_Instance == null)
                {
                    s_Instance = Resources.Load<GameFrameworkSettings>("GameFrameworkSettings");

#if UNITY_EDITOR
                    if (s_Instance == null && !Application.isPlaying)
                    {
                        s_Instance = ScriptableObject.CreateInstance<GameFrameworkSettings>();

                        if (!AssetDatabase.IsValidFolder(string.Format("Assets/{0}", kAssetsFolder)))
                        {
                            AssetDatabase.CreateFolder("Assets", kAssetsFolder);
                        }

                        if (!AssetDatabase.IsValidFolder(string.Format("Assets/{0}/Resources", kAssetsFolder)))
                        {
                            AssetDatabase.CreateFolder(string.Format("Assets/{0}", kAssetsFolder), "Resources");
                        }

                        AssetDatabase.CreateAsset(s_Instance, string.Format("Assets/{0}/Resources/GameFrameworkSettings.asset", kAssetsFolder));
                        assetUpdate = true;

                        s_Instance = Resources.Load<GameFrameworkSettings>("GameFrameworkSettings");
                    }
#endif

                    if (s_Instance == null)
                    {
                        throw new System.InvalidOperationException("Unable to find or create a GameFrameworkSettings resource!");
                    }
                }

#if UNITY_EDITOR
                if (s_Instance.m_GameDatabase == null)
                {
                    string databaseAssetPath = $"Assets/{kAssetsFolder}/GameDatabase.asset";

                    // try to load a database asset by hardcoded path
                    s_Instance.m_GameDatabase = AssetDatabase.LoadAssetAtPath<GameDatabase>(databaseAssetPath);

                    // if that doesn't work, then create one
                    if (s_Instance.m_GameDatabase == null)
                    {
                        s_Instance.m_GameDatabase = ScriptableObject.CreateInstance<GameDatabase>();

                        if (!AssetDatabase.IsValidFolder(string.Format("Assets/{0}", kAssetsFolder)))
                        {
                            AssetDatabase.CreateFolder("Assets", kAssetsFolder);
                        }

                        AssetDatabase.CreateAsset(s_Instance.m_GameDatabase, databaseAssetPath);
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
                if (s_Instance.m_GameDatabase == null)
                {
                    throw new System.Exception("Game Database reference cannot be null."
                        + "Open one of the Game Framework windows in the Unity Editor while not in Play Mode to have a database asset created for you automatically.");
                }
#endif

                #if UNITY_EDITOR
                if (s_Instance.m_PlayerDatabase == null)
                {
                    string databaseAssetPath = $"Assets/{kAssetsFolder}/PlayerDatabase.asset";

                    // try to load a database asset by hardcoded path
                    s_Instance.m_PlayerDatabase = AssetDatabase.LoadAssetAtPath<PlayerDatabase>(databaseAssetPath);

                    // if that doesn't work, then create one
                    if (s_Instance.m_PlayerDatabase == null)
                    {
                        s_Instance.m_PlayerDatabase = ScriptableObject.CreateInstance<PlayerDatabase>();

                        if (!AssetDatabase.IsValidFolder(string.Format("Assets/{0}", kAssetsFolder)))
                        {
                            AssetDatabase.CreateFolder("Assets", kAssetsFolder);
                        }

                        AssetDatabase.CreateAsset(s_Instance.m_PlayerDatabase, databaseAssetPath);
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
                if (s_Instance.m_PlayerDatabase == null)
                {
                    throw new System.Exception("Player Database reference cannot be null."
                        + "Open one of the Game Framework windows in the Unity Editor while not in Play Mode to have a database asset created for you automatically.");
                }
#endif

                #if UNITY_EDITOR
                if (s_Instance.m_AppSettingsDatabase == null)
                {
                    string databaseAssetPath = $"Assets/{kAssetsFolder}/AppSettingsDatabase.asset";

                    // try to load a database asset by hardcoded path
                    s_Instance.m_AppSettingsDatabase = AssetDatabase.LoadAssetAtPath<AppSettingsDatabase>(databaseAssetPath);

                    // if that doesn't work, then create one
                    if (s_Instance.m_AppSettingsDatabase == null)
                    {
                        s_Instance.m_AppSettingsDatabase = ScriptableObject.CreateInstance<AppSettingsDatabase>();

                        if (!AssetDatabase.IsValidFolder(string.Format("Assets/{0}", kAssetsFolder)))
                        {
                            AssetDatabase.CreateFolder("Assets", kAssetsFolder);
                        }

                        AssetDatabase.CreateAsset(s_Instance.m_AppSettingsDatabase, databaseAssetPath);
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
                if (s_Instance.m_AppSettingsDatabase == null)
                {
                    throw new System.Exception("AppSettings Database reference cannot be null."
                        + "Open one of the Game Framework windows in the Unity Editor while not in Play Mode to have a database asset created for you automatically.");
                }
#endif

                return s_Instance;
            }
        }

        [SerializeField]
        [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
        private GameDatabase m_GameDatabase;

        public static GameDatabase GameDatabase
        {
            get { return Instance.m_GameDatabase; }
            set { Instance.m_GameDatabase = value; }
        }

        [SerializeField]
        [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
        private PlayerDatabase m_PlayerDatabase;

        public static PlayerDatabase PlayerDatabase
        {
            get { return Instance.m_PlayerDatabase; }
            set { Instance.m_PlayerDatabase = value; }
        }

        [SerializeField]
        [InspectInline(canCreateSubasset = true, canEditRemoteTarget = true)]
        private AppSettingsDatabase m_AppSettingsDatabase;

        public static AppSettingsDatabase AppSettingsDatabase
        {
            get { return Instance.m_AppSettingsDatabase; }
            set { Instance.m_AppSettingsDatabase = value; }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Set GameFoundationSettings asset file.
        /// </summary>
        [MenuItem("Window/Game Framework/Settings", false, 2031)]
        public static void SelectGameFoundationSettingsAssetFile()
        {
            Selection.SetActiveObjectWithContext(Instance, null);
        }
#endif
    }
}
/*
namespace d4160.Systems.SceneManagement.Tests
{
    using d4160.Game;
    using d4160.Levels;
    using d4160.Worlds;
    using UniRx.Async;
    using UnityEngine;

    public class UniRxAsyncLoaderTest : LevelLauncher, IWorldSceneNames, ILevelSceneNames
    {
        [SerializeField] protected WorldScene m_scene1;
        [SerializeField] protected LevelScene m_scene2;

        private UniRxAsyncEmptySceneLoader _sceneLoader;

#if UNITY_EDITOR
        #region IWorldScene Implementation
        public string[] WorldNames => GameFrameworkSettings.Database.WorldsSO.ArchetypeNames;
        public string[] WorldSceneNames => GameFrameworkSettings.Database.WorldsSO.GetSceneNames(m_scene1.world);
        #endregion

        #region ILevelScene Implementation
        public string[] LevelCategoryNames => GameFrameworkSettings.Database.LevelCategoriesSO.ArchetypeNames;
        public string[] LevelSceneNames => GameFrameworkSettings.Database.LevelCategoriesSO.GetSceneNames(m_scene2.levelCategory);
        #endregion
#endif

        private void Awake()
        {
            _sceneLoader = GetComponent<UniRxAsyncEmptySceneLoader>();
        }

        private void Update()
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;

            if (keyboard.aKey.wasPressedThisFrame)
            {
                if (_asyncOp != null)
                {
                    _asyncOp.allowSceneActivation = true;
                }
            }
        }

        public override async void Load(System.Action onCompleted = null)
        {
            await LoadWorldScene();

            var buildIndex = GameFrameworkSettings.Database.LevelCategoriesSO.GetSceneBuildIndex(m_scene2);

            await _sceneLoader.LoadSceneAsync(
                buildIndex, false,
                null, null, true, (p) => {
                var sceneName = SceneManagementSingleton.GetSceneName(buildIndex);
                Debug.Log($"Loading \"{sceneName}\" scene: {(int)(p * 100)}%", gameObject);
            });
        }

        AsyncOperation _asyncOp;

        private async UniTask LoadWorldScene()
        {
            var buildIndex = GameFrameworkSettings.Database.WorldsSO.GetSceneBuildIndex(m_scene1);

            //Debug.Log($"WorldSceneName null?: {worldSceneName}");
            await _sceneLoader.LoadSceneAsync(
                buildIndex,
                true,
                (ao) => _asyncOp = ao,
                () => Debug.Log($"Scene with \"{buildIndex}\" index load complete.", gameObject),
                false,
                (p) => {
                    var sceneName = SceneManagementSingleton.GetSceneName(buildIndex);
                    Debug.Log($"Loading \"{sceneName}\" scene: {(int)(p * 100)}%", gameObject);
                }
            );
        }

        public override async void Unload(System.Action onCompleted = null)
        {
            await _sceneLoader.UnloadAllLoadedScenes(onCompleted);
        }
    }
}
*/
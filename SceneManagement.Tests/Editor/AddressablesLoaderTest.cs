/*
namespace d4160.Systems.SceneManagement.Tests
{
    using System.Collections;
    using d4160.Game;
    using d4160.Levels;
    using d4160.Worlds;
    using UniRx.Async;
    using UnityEngine;
    using UnityEngine.ResourceManagement.ResourceProviders;

    public class AddressablesLoaderTest : LevelLauncher, IWorldSceneNames, ILevelSceneNames
    {
        [SerializeField] protected WorldScene m_scene1;
        [SerializeField] protected LevelScene m_scene2;

        private AddressablesEmptySceneLoader _sceneLoader;

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
            _sceneLoader = GetComponent<AddressablesEmptySceneLoader>();
        }

        private void Update()
        {
            var keyboard = UnityEngine.InputSystem.Keyboard.current;

            if (keyboard.aKey.wasPressedThisFrame)
            {
                if (_scene.Scene.IsValid())
                {
                    _scene.Activate();

                    UniRx.MainThreadDispatcher.StartUpdateMicroCoroutine(Worker());
                }
            }
        }

        public override async void Load(System.Action onCompleted = null)
        {
            await LoadWorldScene();

            var sceneName = GameFrameworkSettings.Database.LevelCategoriesSO.GetSceneName(m_scene2);

            await _sceneLoader.LoadSceneAsync(
                sceneName, true,
                false, (p) => {
                Debug.Log($"Loading \"{sceneName}\" scene: {(int)(p * 100)}%", gameObject);
            });
        }

        SceneInstance _scene;
        private IEnumerator Worker()
        {
            yield return null;

            SceneManagementSingleton.SetActiveScene(_scene.Scene);
        }

        private async UniTask LoadWorldScene()
        {
            var sceneName = GameFrameworkSettings.Database.WorldsSO.GetSceneName(m_scene1);

            //Debug.Log($"WorldSceneName null?: {worldSceneName}");
            var result = await _sceneLoader.LoadSceneAsync(
                sceneName,
                false,
                true,
                (p) => {
                    Debug.Log($"Loading \"{sceneName}\" scene: {(int)(p * 100)}%", gameObject);
                }
            );

            if (result.Scene.IsValid())
            {
                _scene = result;
            }
        }

        public override async void Unload(System.Action onCompleted = null)
        {
            await _sceneLoader.UnloadAllLoadedScenes(onCompleted);
        }
    }
}
*/
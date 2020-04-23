namespace d4160.GameFramework
{
    using d4160.Core;
    using UnityEngine;
    using UnityExtensions;
    using d4160.Core.Attributes;
    using d4160.UI.Loading;

    public abstract class DefaultPlayLauncher : PlayLevelLauncher
    {
        #region Serialized Fields
        [InspectInline]
        [SerializeField] protected GameModeGraph m_gameModeGraph;
        [Dropdown(ValuesProperty = "ChapterNames")]
        [SerializeField] protected int m_chapterToStart;
        #endregion

        #region Protected Fields and Properties
        protected DefaultGameModeGraphProcessor m_processor;
        protected AsyncOperation m_worldLoadingAsyncOp, m_playLoadingAsyncOp;
        protected bool m_playSceneLoading, m_loadingCompleted;

        public ChapterNode CurrentChapter
        {
            get
            {
                //Debug.Log($"Processor null? {m_processor == null}");
                if (m_processor != null)
                    return m_processor.Current as ChapterNode;

                return null;
            }
        }
        #endregion

        #region Editor Use Members
#if UNITY_EDITOR
        public string[] ChapterNames
        {
            get
            {
                if (!m_gameModeGraph) return new string[0];

                m_processor = new DefaultGameModeGraphProcessor(m_gameModeGraph);

                var nodes = m_processor.ProcessArray;
                var names = new string[nodes.Length];
                for (int i = 0; i < names.Length; i++)
                {
                    if (nodes[i] is ChapterNode)
                    {
                        var chapter = (nodes[i] as ChapterNode);
                        names[i] = $"{chapter.Title} ({chapter.ChapterType})";
                    }
                    else
                    {
                        names[i] = null;
                    }
                }

                return names;
            }
        }
#endif
        #endregion

        #region Unity Callbacks
        protected virtual void Start()
        {
            Initialize();
        }

        protected virtual void Initialize()
        {
            // Only once in the app life, this configuration don't allow runtime graph nodes
            if (m_gameModeGraph && m_processor == null)
            {
                m_processor = new DefaultGameModeGraphProcessor(m_gameModeGraph);
                m_processor.MoveTo(m_chapterToStart);
                m_processor.Run();
            }
            else if (m_processor != null && m_processor.Current == null)
            {
                m_processor.MoveTo(m_chapterToStart);
                m_processor.Run();
            }
        }
        #endregion

        #region ILevel Implementation
        public override void ActivateScenes()
        {
            if (m_worldLoadingAsyncOp != null)
            {
                m_worldLoadingAsyncOp.allowSceneActivation = true;
            }

            if (m_playLoadingAsyncOp != null)
            {
                m_playLoadingAsyncOp.allowSceneActivation = true;
            }
        }
        #endregion

        #region IPlayLevel Implementation
        public override void SetReadyToPlay()
        {
            /** Instantiates **/
            /** Camera/Entities Setup **/
            /** Inputs Enable/Disable **/

            m_playState = PlayState.Waiting;
        }

        /// <summary>
        /// Set as Playing without check PlayState
        /// </summary>
        public override void Play()
        {
            /** Instantiates **/
            /** Camera/Entities Setup **/
            /** Inputs Enable/Disable **/

            Time.timeScale = 1;

            m_playState = PlayState.Playing;
        }

        /// <summary>
        /// Set as Paused without check PlayState
        /// </summary>
        public override void Pause()
        {
            /** TimeSystem calls (timeScale tracer) **/

            Time.timeScale = 0;

            m_playState = PlayState.Paused;
        }

        public override void SwitchPause()
        {
            if (m_playState == PlayState.Paused)
                Play();
            else if (m_playState == PlayState.Playing)
                Pause();
        }

        public override void Restart(bool useLoadingScreen = false)
        {
            Unload(() => {
                if (!useLoadingScreen)
                {
                    Load();
                }
                else
                {
                    LoadWithLoadingScreen();
                }
            });

            m_playState = PlayState.Waiting;
        }

        public override void SetGameOver(PlayResult result)
        {
            /** Destroys/Disables  **/
            /** Camera/Entities Setup **/

            m_playResult = result;
            m_playState = PlayState.GameOver;
        }
        #endregion

        #region IGameModeFlowControl Implementation
        public override void MoveRestart(bool useLoadingScreen = false)
        {
            MoveTo(useLoadingScreen, m_chapterToStart);
        }

        /// <summary>
        /// Move to the next node, passing the index of the inquire output edge
        /// </summary>
        /// <param name="index"></param>
        public override void MoveNext(bool useLoadingScreen = false, int index = -1)
        {
            Unload(() => {
                if (index == -1)
                {
                    m_processor.MoveNext();
                    m_processor.Run();
                }
                else
                {
                    var nodes = CurrentChapter.NextNodes;
                    if (nodes.IsValidIndex(index))
                    {
                        m_processor.MoveNext(nodes[index].GUID);
                        m_processor.Run();
                    }
                }

                if (!useLoadingScreen)
                {
                    Load();
                }
                else
                {
                    LoadWithLoadingScreen();
                }
            });
        }

        /// <summary>
        /// Move to the previous  node, passing the index of the inquire input edge
        /// </summary>
        /// <param name="index"></param>
        public override void MovePrevious(bool useLoadingScreen = false, int index = -1)
        {
            Unload(() => {
                if (index == -1)
                {
                    m_processor.MovePrevious();
                    m_processor.Run();
                }
                else
                {
                    var nodes = CurrentChapter.PreviousNodes;
                    if (nodes.IsValidIndex(index))
                    {
                        m_processor.MovePrevious(nodes[index].GUID);
                        m_processor.Run();
                    }
                }

                if (!useLoadingScreen)
                {
                    Load();
                }
                else
                {
                    LoadWithLoadingScreen();
                }
            });
        }

        /// <summary>
        /// Move to the node, passing the index of the node
        /// </summary>
        /// <param name="index"></param>
        public override void MoveTo(bool useLoadingScreen = false, int index = -1)
        {
            Unload(() => {
                m_processor.MoveTo(index);
                m_processor.Run();

                if (!useLoadingScreen)
                {
                    Load();
                }
                else
                {
                    LoadWithLoadingScreen();
                }
            });
        }

        protected virtual void LoadWithLoadingScreen()
        {
            // The LoadingScreen level is the first by default (0)
            var loadingLauncher = GameManager.Instance.GetLevelLauncher<DefaultLoadingScreenLauncher>(0);

            loadingLauncher.SetLevelToLoad(LevelType.GameMode, LauncherIndex);

            GameManager.Instance.LoadLevel(LevelType.General, 0);
        }
        #endregion

        #region ILevel Implementation
        public override void Load(System.Action onCompleted = null)
        {
            m_playState = PlayState.Loading;

            /* If the GameManager is faster than this */
            Initialize();

            LoadWorldScene(onCompleted);
        }

        protected virtual void LoadWorldScene(System.Action onCompleted = null)
        {
            var chapter = CurrentChapter;

            if (chapter == null) return;

            int? buildIndex = (GameFrameworkSettings.GameDatabase[2] as IWorldSceneGetter)?.GetSceneBuildIndex(chapter.WorldScene);

            Debug.Log($"(Play Launcher) Loading World Scene: {buildIndex} index");

            if (!buildIndex.HasValue || buildIndex == -1)
            {
                LoadPlayScene(true, onCompleted);
                return;
            }

            m_sceneLoader.LoadSceneAsync(
                buildIndex.Value,
                true,
                (ao) => m_worldLoadingAsyncOp = ao,
                null,
                false,
                (p) => {
                    if (p >= 0.9f)
                    {
                        if (!m_playSceneLoading)
                        {
                            LoadPlayScene(false, onCompleted);
                            m_playSceneLoading = true;
                        }
                    }
                    else
                    {
                        if (LoadingScreenBase.Instanced)
                            LoadingScreenBase.Instance.SetLoadingProgress(p);
                    }
                }
            );
        }

        protected virtual void LoadPlayScene(bool setActiveAsMainScene = false, System.Action onCompleted = null)
        {
            var chapter = CurrentChapter;

            if (chapter == null) return;

            int? buildIndex = (GameFrameworkSettings.GameDatabase[3] as ILevelSceneGetter)?.GetSceneBuildIndex(chapter.LevelScene);

            Debug.Log($"(Play Launcher) Loading Play Scene: {buildIndex} index");

            if (!buildIndex.HasValue || buildIndex == -1)
            {
                onCompleted?.Invoke();

                return;
            }

            m_sceneLoader.LoadSceneAsync(
                buildIndex.Value,
                setActiveAsMainScene,
                (ao) => m_playLoadingAsyncOp = ao,
                () => {
                    onCompleted?.Invoke();
                    SetReadyToPlay();
                },
                false,
                (p) => {
                    if (p < 0.9f)
                    {
                        Debug.Log($"{p}%");
                        if (LoadingScreenBase.Instanced)
                            LoadingScreenBase.Instance.SetLoadingProgress(p);
                    }
                    else
                    {
                        if (!m_loadingCompleted)
                        {
                            if (LoadingScreenBase.Instanced)
                                LoadingScreenBase.Instance.SetAsLoadCompleted();
                            else
                                ActivateScenes();

                            m_loadingCompleted = true;
                        }
                    }
                }
            );
        }

        public override void Unload(System.Action onCompleted = null)
        {
            m_loadingCompleted = false;
            m_playSceneLoading = false;

            m_worldLoadingAsyncOp = null;
            m_playLoadingAsyncOp = null;

            m_sceneLoader.UnloadAllLoadedScenes(() => {

                Time.timeScale = 1;

                m_playState = PlayState.None;

                onCompleted.Invoke();
            });
        }
        #endregion
    }
}
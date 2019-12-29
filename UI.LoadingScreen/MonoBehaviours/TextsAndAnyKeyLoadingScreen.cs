namespace d4160.UI
{
    using d4160.Core;
    using NaughtyAttributes;
    using UnityEngine;
    using UnityEngine.UI;
#if UNITY_INPUT_SYSTEM
    using UnityEngine.InputSystem;
#endif

    public class TextsAndAnyKeyLoadingScreen : NonInteractiveLoadingScreen
    {
        [Header("TITLES")]
        public bool enableTitle;
        [ShowIf("enableTitle")]
        public Text title;
        [ShowIf("enableTitle")]
        public string titleText;
        public bool enableTitleDesc = true;
        [ShowIf("enableTitleDesc")]
        public Text titleDescription;
        [ShowIf("enableTitleDesc")]
        public string titleDescText;

        [Header("PROGRESS")]
        public bool enableProgress;
        [ShowIf("enableProgress")]
        public Text progress;
        public bool enableProgressBar;
        [ShowIf("enableProgressBar")]
        public GameObject progressBar;

        [Header("HINTS")]
        public bool enableHints;
        [ShowIf("enableHints")]
        public Text hintsText;
        [ShowIf("enableHints")]
        public string[] hintList;
        [ShowIf("enableHints")]
        public bool useRandomHint = true;
        [ShowIf("enableHints")]
        public bool changeHintWithTimer;
        [ShowIf(ConditionOperator.And, "enableHints", "changeHintWithTimer")]
        public float hintTimerValue = 5;

        [Header("PRESS ANY KEY")]
        public bool enablePressAnyKey = true;
        [ShowIf("enablePressAnyKey")]
        public Animator objectAnimator;

        protected bool m_isAnimPlayed = false;
        protected int m_hintIndex;
        protected float m_hintTimer;
        protected Slider m_progressSlider;
        protected Image m_progressImage;

        protected override void Start()
        {
            base.Start();

            if (title)
            {
                title.enabled = enableTitle;
                if (enableTitle)
                {
                    title.text = titleText;
                }
            }

            if (titleDescription)
            {
                titleDescription.enabled = enableTitleDesc;
                if (enableTitleDesc)
                {
                    titleDescription.text = titleDescText;
                }
            }

            if (progress)
            {
                progress.enabled = enableProgress;
            }

            if (hintsText)
            {
                hintsText.enabled = enableHints;
                if (enableHints && hintList.Length > 0)
                {
                    m_hintIndex = 0;

                    // If this is enabled, generate random hints
                    if (useRandomHint == true)
                    {
                        string hintChose = hintList[Random.Range(0, hintList.Length)];
                        hintsText.text = hintChose;
                    }
                    else
                    {
                        hintsText.text = hintList[m_hintIndex];
                    }
                }
            }

            if (progressBar)
            {
                progressBar.gameObject.SetActive(enableProgressBar);
                if (enableProgressBar)
                {
                    m_progressSlider = progressBar.GetComponent<Slider>();

                    if (!m_progressSlider)
                    {
                        m_progressImage = progressBar.GetComponent<Image>();
                    }
                }
            }
        }

        protected override void UpdateCallback()
        {
            UpdateElapsedLoadingTime();

            ProcessProgress();

            ProcessImages();

            ProcessHints();

            if (ReadyToContinue)
            {
                if (!enablePressAnyKey)
                {
                    // Fade out
                    canvasAlpha.alpha -= fadingAnimationSpeed * Time.deltaTime;

                    // If fade out is complete, then disable the object
                    if (canvasAlpha.alpha <= 0)
                    {
                        FinishAndContinue();
                    }
                }
                else
                {
                    // If loading is completed and PAK feature enabled, show Press Any Key panel
                    if (m_isAnimPlayed == false)
                    {
                        objectAnimator.enabled = true;
                        objectAnimator.Play ("PAK Fade-in");
                        m_isAnimPlayed = true;
                    }

                    if (ProcessAnyKey())
                    {
                        objectAnimator.Play ("PAK Fade-out");
                        FinishAndContinue();
                    }
                }
            }
        }

        protected virtual bool ProcessAnyKey()
        {
#if UNITY_INPUT_SYSTEM
            var keyboard = Keyboard.current;
            if (keyboard.anyKey.wasPressedThisFrame)
            {
                return true;
            }
#endif
            return false;
        }

        protected virtual void ProcessProgress()
        {
            if (enableProgressBar || enableProgress)
            {
                var p = GetProgress(minLoadingDuration);

                if (enableProgressBar)
                {
                    if (m_progressImage)
                    {
                        m_progressImage.fillAmount = p;
                    }
                    else if (m_progressSlider)
                    {
                        m_progressSlider.value = p;
                    }
                }

                if (enableProgress)
                {
                    progress.text = $"{(int)(p * 100)}%";
                }
            }
        }

        protected virtual void ProcessHints()
        {
            // Check if random hints are enabled
            if (enableHints)
            {
                m_hintTimer += Time.deltaTime;

                if (m_hintTimer >= hintTimerValue)
                {
                    string hintChose = useRandomHint ?
                        hintList[Random.Range(0, hintList.Length)] : hintList[m_hintIndex];
                    hintsText.text = hintChose;

                    if (!useRandomHint)
                    {
                        m_hintIndex++;
                        if (!hintList.IsValidIndex(m_hintIndex))
                        {
                            m_hintIndex = 0;
                        }
                    }

                    m_hintTimer = 0;
                }
            }
        }
    }
}



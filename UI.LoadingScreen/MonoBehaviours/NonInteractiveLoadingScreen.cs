namespace d4160.UI
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using NaughtyAttributes;
    using d4160.Core;

    public class NonInteractiveLoadingScreen : LoadingScreenBase
    {
        [Header("RESOURCES")]
        public CanvasGroup canvasAlpha;

        [Header("IMAGES")]
        public Image imageObject;
        [OnValueChanged("CalculateMinLoadingDuration")]
        public Sprite[] imageList;
        [Tooltip("If not use images in sequence")]
        public bool useRandomImage;
        public bool changeImageWithTimer;
        [ShowIf("changeImageWithTimer")]
        [OnValueChanged("CalculateMinLoadingDuration")]
        public float imageTimerValue = 2f;
        [ShowIf("changeImageWithTimer")]
        public Animator fadingAnimator;
        [ShowIf("changeImageWithTimer")]
        [OnValueChanged("CalculateMinLoadingDuration")]
        [Range(0.1f, 5)] public float imageFadingSpeed = 1f;
        [Tooltip("For the complete fade out and fade in. For 60 frames = 1f")]
        [ShowIf("changeImageWithTimer")]
        [OnValueChanged("CalculateMinLoadingDuration")]
        public float fadingAnimationDuration = 1.1f;

        [Header("SETTINGS")]
        public float fadingAnimationSpeed = 2.0f;
        [ShowIf("changeImageWithTimer")]
        public bool calculateMinLoadingDuration;
        [ShowIf(ConditionOperator.And, "changeImageWithTimer", "calculateMinLoadingDuration")]
        [ReadOnly]
        public float calculatedMinLoadingDuration;
        [HideIf(ConditionOperator.And, "calculateMinLoadingDuration", "changeImageWithTimer")]
        public float minLoadingDuration = 2f;

        protected int m_imageCount;
        protected bool m_fading;

        protected void CalculateMinLoadingDuration()
        {
            calculatedMinLoadingDuration = (imageTimerValue + fadingAnimationDuration * 1f / imageFadingSpeed) * imageList.Length;
        }

        protected override bool ReadyToContinue => m_elapsedLoadingTime >= minLoadingDuration && m_sceneAsyncLoadCompleted;

        protected virtual void Start()
        {
            m_imageCount = 1;

            if (calculateMinLoadingDuration)
                minLoadingDuration = calculatedMinLoadingDuration;

            if (imageObject && imageList.Length > 0)
            {
                // If this is enabled, generate random images
                if (useRandomImage == true)
                {
                    Sprite imageChose = imageList[Random.Range(0, imageList.Length)];
                    imageObject.sprite = imageChose;
                }
                else
                {
                    imageObject.sprite = imageList[m_imageCount - 1];
                }
            }

            // Set up Image fading anim speed
            if (changeImageWithTimer && fadingAnimator)
                fadingAnimator.speed = imageFadingSpeed;
            // fadingAnimator.SetFloat("Fade Out", imageFadingSpeed);
        }

        protected override void UpdateCallback()
        {
            base.UpdateCallback();

            ProcessImages();

            if (ReadyToContinue)
            {
                // Fade out
                canvasAlpha.alpha -= fadingAnimationSpeed * Time.deltaTime;

                // If fade out is complete, then disable the object
                if (canvasAlpha.alpha <= 0)
                {
                    FinishAndContinue();
                }
            }
        }

        protected virtual void ProcessImages()
        {
            if (changeImageWithTimer && imageList.Length > 1)
            {
                if (m_elapsedLoadingTime >= imageTimerValue * m_imageCount + ((m_imageCount - 1) * fadingAnimationDuration) && fadingAnimator.GetCurrentAnimatorStateInfo(0).IsName("Start"))
                {
                    fadingAnimator.Play("Fade In");
                    m_fading = true;
                }
                else if (m_fading && fadingAnimator.GetCurrentAnimatorStateInfo(0).IsName("Fade Out"))
                {
                    if (imageList.IsValidIndex(m_imageCount))
                    {
                        Sprite imageChose = useRandomImage == true ?
                            imageList[Random.Range(0, imageList.Length)] :
                            imageList[m_imageCount];
                        imageObject.sprite = imageChose;

                        m_imageCount++;
                        m_fading = false;

                        if (m_imageCount >= imageList.Length)
                        {
                            changeImageWithTimer = false;
                        }
                    }
                }
            }
        }
    }
}
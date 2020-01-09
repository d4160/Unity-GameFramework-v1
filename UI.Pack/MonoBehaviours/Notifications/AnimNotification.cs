namespace d4160.UI.Notification
{
    using UnityEngine;
    using UnityEngine.UI;

    public class AnimNotification : NotificationBase
    {
        [Header("OBJECT")]
        public GameObject notificationObject;
        public Animator notificationAnimator;

        [Header("OBJECT")]
        public Text titleObject;
        public Text descriptionObject;

        [Header("VARIABLES")]
        public string titleText;
        public string animationNameIn;
        public string animationNameOut;

        void Start()
        {
            gameObject.SetActive(false);
        }

        public override void Notify(string msg, float duration)
        {
            if (notificationObject) 
                notificationObject.SetActive(true);

            if (titleObject)
                titleObject.text = titleText;

            if (descriptionObject)
                descriptionObject.text = msg;

            if (notificationAnimator)
                notificationAnimator.Play(animationNameIn);
            
            Invoke("HideNotification", duration);
        }

        private void HideNotification()
        { 
            if (notificationAnimator)
                notificationAnimator.Play(animationNameOut);
        }
    }
}

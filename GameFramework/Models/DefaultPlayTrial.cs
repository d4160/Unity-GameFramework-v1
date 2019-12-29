namespace d4160.GameFramework
{
    using UnityEngine;

    [System.Serializable]
    public class DefaultPlayTrial
    {
        [SerializeField] protected int score;
        [SerializeField] protected float time;
        [SerializeField] protected PlayResult result;
        [SerializeField] protected System.DateTime startedTimeStamp;

        public DefaultPlayTrial()
        {

        }
    }
}
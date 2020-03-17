using d4160.Core;
using DG.DeAudio;
using UnityEngine;

namespace d4160.GameFramework
{
    public class AudioBehaviour : MonoBehaviour
    {
        [SerializeField] protected DeAudioClipData[] _clips;

        public virtual void PlayAudioClip(int clipIndex)
        {
            if (_clips.IsValidIndex(clipIndex))
            {
                _clips[clipIndex].Play();
            }
        }

        public virtual void StopAudioClip(int clipIndex)
        {
            if (_clips.IsValidIndex(clipIndex))
            {
                _clips[clipIndex].Stop();
            }
        }

        public virtual void PauseAudioClip(int clipIndex)
        {
            if (_clips.IsValidIndex(clipIndex))
            {
                _clips[clipIndex].Pause();
            }
        }
    }
}
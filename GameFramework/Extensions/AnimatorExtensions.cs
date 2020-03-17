using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace d4160.GameFramework
{
    public static class AnimatorExtensions
    {
        public static void SetTrigger(this Animator anim, ref AnimatorString @string)
        {
            anim.SetTrigger(@string.HashOrId);
        }

        public static void SetBool(this Animator anim, ref AnimatorString @string, bool value)
        {
            anim.SetBool(@string.HashOrId, value);
        }

        public static void SetInteger(this Animator anim, ref AnimatorString @string, int value)
        {
            anim.SetInteger(@string.HashOrId, value);
        }

        public static void SetFloat(this Animator anim, ref AnimatorString @string, float value)
        {
            anim.SetFloat(@string.HashOrId, value);
        }

        public static void Play(this Animator anim, ref AnimatorString @string, int layer = -1, float normalizedTime = float.NegativeInfinity)
        {
            anim.Play(@string.HashOrId, layer, normalizedTime);
        }

        public static void ChangeCurrentStateNormalizedTime(this Animator anim, int layer = 0, float normalizedTime = 0.0f)
        {
            anim.Play(0, layer, normalizedTime);
        }
    }
}
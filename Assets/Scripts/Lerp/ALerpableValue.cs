using System;
using System.Collections;
using UnityEngine;

namespace Lerp
{
    public abstract class ALerpableValue : MonoBehaviour
    {
        /// <summary>
        /// Lerp value between startValue and endValue. startValue and endValue should both be 0.0 to 1.0
        /// </summary>
        /// <param name="startValue"></param>
        /// <param name="endValue"></param>
        /// <param name="duration"></param>
        public void StartLerp(float startValue, float endValue, float duration)
        {
            if (duration <= 0.0f)
            {
                throw new Exception("Cannot have negative/zero duration");
            }

            StartCoroutine(LerpValue(startValue, endValue, duration));
        }

        private IEnumerator LerpValue(float startValue, float endValue, float duration)
        {
            float timeElapsed = 0f;
            while (timeElapsed < duration)
            {
                float progress = Mathf.Min(timeElapsed / duration, 1f);
                ApplyValue(progress);
                timeElapsed += Time.deltaTime;
                yield return null;
            }
        }

        /// <summary>
        /// Applies value of the lerp. Lerp is between two values. 0 means fully value A, 1 means fully value B. 
        /// </summary>
        /// <param name="value"></param>
        protected abstract void ApplyValue(float value);
    }
}
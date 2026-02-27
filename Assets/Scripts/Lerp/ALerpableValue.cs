using System;
using System.Collections;
using UnityEngine;

namespace Lerp
{
    public abstract class ALerpableValue : MonoBehaviour
    {
        /// <summary>
        /// Lerp value from value A to value B for duration
        /// </summary>
        /// <param name="duration"></param> Min is 0 seconds
        public void StartLerp(float duration)
        {
            if (duration < 0.0f)
            {
                throw new Exception("Cannot have negative duration");
            }

            if (duration > 0.0f)
            {
                StartCoroutine(LerpValue(duration));
            }
            else
            {
                // Instantly apply change if duration 0
                ApplyValue(1f);
            }
        }

        private IEnumerator LerpValue(float duration)
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
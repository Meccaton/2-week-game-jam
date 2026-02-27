using UnityEngine;


namespace Lerp
{
    /// <summary>
    /// Example of how to access Landscape manager and use one of the public methods to dynamically change
    /// landscape during runtime
    /// </summary>
    public class ChangeCheckerColorOnKey : MonoBehaviour
    {
        void Update()
        {
            if (Input.anyKeyDown)
            {
                LandscapeManager.Instance.ChangeCheckerFirstColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), 2f);
            }
        }
    }
}

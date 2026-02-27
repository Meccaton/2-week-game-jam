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
            if (Input.GetKeyDown("1"))
            {
                LandscapeManager.Instance.ChangeCheckerSquareSize(Random.Range(0.2f, 2f), 0.5f);
            }

            if (Input.GetKeyDown("2"))
            {
                LandscapeManager.Instance.ChangeCheckerFirstColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), 2f);
            }
            if (Input.GetKeyDown("3"))
            {
                LandscapeManager.Instance.ChangeCheckerSecondColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), 2f);
            }
            if (Input.GetKeyDown("4"))
            {
                LandscapeManager.Instance.ChangeSkyboxTopColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), 0.5f);
            }
            if (Input.GetKeyDown("5"))
            {
                LandscapeManager.Instance.ChangeSkyboxBottomColor(new Color(Random.Range(0f, 1f), Random.Range(0f, 1f), Random.Range(0f, 1f)), 0.5f);
            }
        }
    }
}

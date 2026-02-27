using Lerp;
using UnityEngine;

/// <summary>
/// One stop shop for dynamically managing landscape
/// From anywhere in your script can use Instance field to access public methods, i.e LandscapeManager.Instance.ChangeCheckerFirstColor(...);
/// </summary>
public class LandscapeManager : MonoBehaviour
{
    [SerializeField] private LerpableMaterialPropertyColor checkerColorLerper1;
    [SerializeField] private LerpableMaterialPropertyColor checkerColorLerper2;

    public static LandscapeManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// Change one of the checker colors
    /// </summary>
    /// <param name="color"></param> Color to change to
    /// <param name="duration"></param> Duration it should spend fading into new color
    public void ChangeCheckerFirstColor(Color color, float duration) => ChangeCheckerColor(checkerColorLerper1, color, duration);

    /// <summary>
    /// Change one of the checker colors
    /// </summary>
    /// <param name="color"></param> Color to change to
    /// <param name="duration"></param> Duration it should spend fading into new color
    public void ChangeCheckerSecondColor(Color color, float duration) => ChangeCheckerColor(checkerColorLerper2, color, duration);

    private void ChangeCheckerColor(LerpableMaterialPropertyColor checkerColorLerper, Color c, float duration)
    {
        checkerColorLerper.SetStartColorToCurrentColor();
        checkerColorLerper.SetEndColor(c);
        checkerColorLerper.StartLerp(duration);
    }
}

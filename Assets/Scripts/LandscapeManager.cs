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
    [SerializeField] private LerpableMaterialPropertyFloat squareSizeLerper;
    [SerializeField] private LerpableMaterialPropertyColorNoRenderer skyboxTopLerper;
    [SerializeField] private LerpableMaterialPropertyColorNoRenderer skyboxBottomLerper;

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
        checkerColorLerper.SetStartLerpValueToCurrentValue();
        checkerColorLerper.SetEndColor(c);
        checkerColorLerper.StartLerp(duration);
    }

    public void ChangeCheckerSquareSize(float size, float duration)
    {
        squareSizeLerper.SetStartLerpValueToCurrentValue();
        squareSizeLerper.setLerpEndValue(size);
        squareSizeLerper.StartLerp(duration);
    }

    // TODO: If it wasn't a game jam and had more time I'd just have one ChangeColor method that checker and skybox would both call,
    // as they both should inherit from the same ALerpableColor class (that doesn't exist) 
    public void ChangeSkyboxTopColor(Color c, float duration) => ChangeSkyboxColor(skyboxTopLerper, c, duration);
    public void ChangeSkyboxBottomColor(Color c, float duration) => ChangeSkyboxColor(skyboxBottomLerper, c, duration);
    private void ChangeSkyboxColor(LerpableMaterialPropertyColorNoRenderer skyboxLerper, Color c, float duration)
    {
        skyboxLerper.SetStartLerpValueToCurrentValue();
        skyboxLerper.SetEndColor(c);
        skyboxLerper.StartLerp(duration);
    }
}

using UnityEngine;
namespace Lerp
{
    /// <summary>
    /// Lerps a color from a material instance
    /// </summary>
    public class LerpableMaterialPropertyColor : ALerpableValue
    {
        [SerializeField] private Renderer renderer;
        [SerializeField] private string colorPropertyName;
        [SerializeField] private Color colorA;
        [SerializeField] private bool useStartingColorForColorA;
        [SerializeField] private Color colorB;

        private void Start()
        {
            if (useStartingColorForColorA)
            {
                colorA = renderer.material.GetColor(colorPropertyName);
            }
        }

        protected override void ApplyValue(float value)
        {
            Color color = Color.Lerp(colorA, colorB, value);
            renderer.material.SetColor(colorPropertyName, color);
        }
    }
}

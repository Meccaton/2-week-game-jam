using UnityEngine;
using UnityEngine.Serialization;

namespace Lerp
{
    /// <summary>
    /// Lerps a color from a material instance
    /// </summary>
    public class LerpableMaterialPropertyColor : ALerpableValue
    {
        [SerializeField] private Renderer renderComponent;
        [SerializeField] private string colorPropertyName;
        [FormerlySerializedAs("colorA")] [SerializeField] private Color startColor;
        [FormerlySerializedAs("colorB")] [SerializeField] private Color endColor;
        
        public void SetStartColorToCurrentColor()
        {
            startColor = renderComponent.material.GetColor(colorPropertyName);
        }

        protected override void ApplyValue(float value)
        {
            Color color = Color.Lerp(startColor, endColor, value);
            renderComponent.material.SetColor(colorPropertyName, color);
        }

        public void SetStartColor(Color c) => startColor = c;

        public void SetEndColor(Color c) => endColor = c;
        
    }
}

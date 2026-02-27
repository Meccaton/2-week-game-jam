using UnityEngine;

namespace Lerp
{
    // TODO: Can consolidate this with the Renderer version, DRY principles
    public class LerpableMaterialPropertyColorNoRenderer : ALerpableValue
    {
        [SerializeField] private Material material;
        [SerializeField] private string colorPropertyName;
        [SerializeField] private Color startColor;
        [SerializeField] private Color endColor;
        
        public override void SetStartLerpValueToCurrentValue() => startColor = material.GetColor(colorPropertyName);

        protected override void ApplyValue(float value)
        {
            Color color = Color.Lerp(startColor, endColor, value);
            material.SetColor(colorPropertyName, color);
        }

        public void SetStartColor(Color c) => startColor = c;

        public void SetEndColor(Color c) => endColor = c;
    }
}

using UnityEngine;

namespace Lerp
{
    public class LerpableMaterialPropertyFloat : ALerpableValue
    {
        [SerializeField] private Renderer renderComponent;
        [SerializeField] private string floatPropertyName;
        [SerializeField] private float startLerpValue;
        [SerializeField] private float endLerpValue;

        protected override void ApplyValue(float value)
        {
            renderComponent.material.SetFloat(floatPropertyName, Mathf.Lerp(startLerpValue, endLerpValue, value));
        }

        public override void SetStartLerpValueToCurrentValue() => startLerpValue = renderComponent.material.GetFloat(floatPropertyName);
        
        public void setLerpEndValue(float endLerpValue) => this.endLerpValue = endLerpValue;
    }
}

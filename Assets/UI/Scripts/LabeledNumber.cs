using System;
using TMPro;
using UnityEngine;

namespace UI.Scripts
{
    public class LabeledNumber : MonoBehaviour
    {
        private TMP_Text _textMeshPro;
        private string _label = "Score: {0:0000.0}";
        private float _value = 0f;
        private void Awake()
        {
            _textMeshPro = GetComponent<TMP_Text>();
            UpdateText();
        }

        private void UpdateText()
        {
            _textMeshPro.SetText(string.Format(_label, _value));
        }
        
        public void IncreaseValue(float amount)
        {
            _value += amount;
            UpdateText();
        }
        
        public void SetLabel(string label)
        {
            _label = label;
            UpdateText();
        }

        public void FixedUpdate()
        {
            // Temporary: increase value by 10/second
            // In the future IncreaseValue will be called by other classes, or by triggers
            IncreaseValue(10 * Time.deltaTime);
        }
    }
}
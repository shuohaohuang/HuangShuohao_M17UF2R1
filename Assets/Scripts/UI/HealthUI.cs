using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    FloatVariables healthValues;

    [SerializeField]
    Slider healthBar;

    [SerializeField]
    TextMeshProUGUI text;

    private void Awake()
    {
        healthValues.OnValuesUpdate += UpdateValues;
        healthBar.minValue = 0;
        UpdateValues();
    }

    private void UpdateValues()
    {
        healthBar.maxValue = healthValues.Max;
        healthBar.value = healthValues.Current;
        text.text = $"{healthValues.Current} / {healthValues.Max}";
    }
}

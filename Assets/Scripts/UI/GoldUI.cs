using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GoldUI : MonoBehaviour
{
    [SerializeField]
    FloatVariables goldvalues;

    [SerializeField]
    TextMeshProUGUI text;

    private void Awake()
    {
        goldvalues.OnValuesUpdate += UpdateValues;
        UpdateValues();
    }

    private void UpdateValues()
    {
        text.text = $"{goldvalues.Current}";
    }
}

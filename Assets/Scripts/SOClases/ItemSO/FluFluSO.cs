using UnityEngine;

[CreateAssetMenu(fileName = "FluFluSO", menuName = "Scriptable Objects/FluFluSO")]
public class FluFluSo : ItemsSO
{
    public float stunTime;
    public float angryRate;
    public float range;
    public float satiety;

    public float capacity;
    public float consumption;

    public float refillTime;
    Material material;
}

using System;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "FloatVariables", menuName = "Scriptable Objects/FloatVariables")]
public class FloatVariables : ScriptableObject
{
    [SerializeField]
    private float max;

    [SerializeField]
    private float current;

    [SerializeField]
    private bool beenInit;

    public UnityAction OnValuesUpdate;

    public float Max
    {
        get => max;
        set
        {
            if (value == -1 || max == -1)
                max = -1;
            else
                max = Mathf.Max(0, value);
            OnValuesUpdate?.Invoke();
        }
    }

    public float Current
    {
        get => current;
        set
        {
            if (max != -1)
                current = Mathf.Clamp(value, 0, max);
            else
                current = value;
            OnValuesUpdate?.Invoke();
            BeenInit = true;
        }
    }
    public bool BeenInit
    {
        get => beenInit;
        set => beenInit = value;
    }
}

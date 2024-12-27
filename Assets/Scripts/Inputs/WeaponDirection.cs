using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponDirection : MonoBehaviour, InputSystem_Actions.IDirectionActions
{
    private InputSystem_Actions inputs;

    private PlaceHolder placeHolder;

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
    }

    void Awake()
    {
        inputs = new();
        inputs.Direction.AddCallbacks(this);
    }

    private void Start()
    {
        placeHolder = GetComponent<PlaceHolder>();
    }
}

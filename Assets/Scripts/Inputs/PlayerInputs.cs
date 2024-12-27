using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputs;
    private Movement movement;
    private MainCharaterAnimation _animation;
    private Inventory inventory;

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log("attack");
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
            inventory.GetItem(PlaceHolder.FirstHolder());
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        // Debug.Log(context.ReadValue<Vector2>());
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        movement.MoveTo(context.ReadValue<Vector2>());
        if (context.performed)
        {
            _animation.Moving(context.ReadValue<Vector2>());
        }

        if (context.canceled)
        {
            _animation.StopMoving();
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputs = new();
        inputs.Player.AddCallbacks(this);
    }

    private void Start()
    {
        movement = GetComponent<Movement>();
        _animation = GetComponent<MainCharaterAnimation>();
        inventory = GetComponent<Inventory>();
    }

    private void OnEnable()
    {
        inputs.Enable();
    }

    private void OnDisable()
    {
        inputs.Disable();
    }

    public void OnSwitchLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
            inventory.Switch(-1);
    }

    public void OnSwitchRight(InputAction.CallbackContext context)
    {
        if (context.performed)
            inventory.Switch(1);
    }
}

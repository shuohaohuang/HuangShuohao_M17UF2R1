using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputs;
    private Movement movement;
    private MainCharaterAnimation _animation;

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        Debug.Log(context.ReadValue<Vector2>());
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
    }

    private void OnEnable()
    {
        inputs.Enable();
    }
    private void OnDisable()
    {
        inputs.Disable();
    }
}

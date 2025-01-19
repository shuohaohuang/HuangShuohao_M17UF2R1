using System;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    private InputSystem_Actions inputs;
    private Movement movement;
    private MainCharaterAnimation _animation;
    private Inventory inventory;

    [SerializeField]
    ItemInspectorSO itemInspectorSO;

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inventory.GetCurrentItem() is AItem item)
            {
                item.StartUse();
            }
        }
        if (context.canceled)
        {
            if (inventory.GetCurrentItem() is AItem item)
            {
                item.StopUse();
            }
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            if (AItem.GetFirst() is AItem item)
            {
                inventory.GetItem(item);
                return;
            }
            if (AInteratacable.GetFirst() is AInteratacable interactable)
            {
                interactable.Act();
            }
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inventory.GetCurrentItem() is ITrackable trackable)
            {
                trackable.Track(context.ReadValue<Vector2>());
            }
        }
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

    void Awake()
    {
        inputs = new();
        inputs.Player.AddCallbacks(this);
        inventory = GetComponent<Inventory>();
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

    public void OnSwitchLeft(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inventory.Switch(-1);
        }
    }

    public void OnSwitchRight(InputAction.CallbackContext context)
    {
        if (context.performed)
            inventory.Switch(1);
    }

    public void OnInspect(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (inventory.GetCurrentItem() is AItem item)
            {
                itemInspectorSO.InpectItem(item.itemsSO);
            }
        }
        if (context.canceled)
        {
            itemInspectorSO.EndInspect();
        }
    }
}

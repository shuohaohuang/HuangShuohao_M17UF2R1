using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ainteratacable : MonoBehaviour
{
    public static List<Ainteratacable> interatacables = new();

    public static Ainteratacable GetFirst()
    {
        if (interatacables[0] is Ainteratacable interactable)
        {
            interatacables.Remove(interactable);
            return interactable;
        }
        return null;
    }

    public abstract void Act();

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLAYER"))
        {
            interatacables.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLAYER"))
        {
            interatacables.Remove(this);
        }
    }
}

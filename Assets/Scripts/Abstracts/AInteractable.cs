using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AInteratacable : MonoBehaviour
{
    public static List<AInteratacable> interatacables = new();

    public static AInteratacable GetFirst()
    {
        if (interatacables.Count == 0)
            return null;
        AInteratacable firstItem = interatacables[0];
        interatacables.RemoveAt(0);

        return firstItem;
    }

    public abstract void Act();

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLAYER"))
        {
            interatacables.Add(this);
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLAYER"))
        {
            interatacables.Remove(this);
        }
    }
}

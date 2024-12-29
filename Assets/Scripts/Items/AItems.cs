using System.Collections.Generic;
using UnityEngine;

public abstract class AItems : MonoBehaviour, IHoldable
{
    [SerializeField]
    protected int price;

    [SerializeField]
    protected string alias;

    protected Transform parentTransform;

    public static List<AItems> pickAvaible = new();

    public static AItems GetFirst()
    {
        if (pickAvaible.Count == 0)
            return null;

        AItems firstItem = pickAvaible[0];
        pickAvaible.RemoveAt(0);

        return firstItem;
    }

    public void HoldNew(Transform parent)
    {
        transform.SetParent(parent);
        parentTransform = parent;
        transform.localPosition = Vector3.zero;
    }

    public abstract void Use();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLAYER"))
        {
            pickAvaible.Add(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLAYER"))
            pickAvaible.Remove(this);
    }
}

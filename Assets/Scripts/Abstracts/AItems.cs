using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AItems : MonoBehaviour, IHoldable
{
    [SerializeField]
    protected bool useAvaible = true;

    [SerializeField]
    protected int price;

    [SerializeField]
    protected float cd;

    [SerializeField]
    protected string alias;

    [SerializeField]
    protected AudioClip shotAudio;

    protected Transform parentTransform;

    public static List<AItems> pickAvaible = new();

    protected abstract void InitializeStats();

    protected virtual IEnumerator Cooldown()
    {
        useAvaible = false;
        yield return new WaitForSeconds(cd);
        useAvaible = true;
    }

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
        InitializeStats();
    }

    public abstract void Use();

    protected void OnTriggerEnter2D(Collider2D other)
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

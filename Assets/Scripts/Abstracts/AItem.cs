using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AItem : MonoBehaviour, IHoldable
{
    protected bool isUsing = false;
    protected int price;

    [SerializeField]
    protected float currentCd = -0.1f;

    [SerializeField]
    protected float cd;

    protected string alias;

    public Sprite sprite;

    protected AudioClip onUseAudio;

    protected AudioSource audioSource;

    protected Transform parentTransform;

    public static List<AItem> pickAvaible = new();

    public ItemsSO itemsSO;

    protected Coroutine useCoroutine;

    protected Coroutine cooldownCoroutine;

    protected abstract void InitializeStats();

    protected virtual IEnumerator Cooldown()
    {
        currentCd = cd;
        while (currentCd > 0)
        {
            currentCd -= Time.deltaTime;

            yield return null; // Esperar hasta el siguiente frame
        }
    }

    public static AItem GetFirst()
    {
        if (pickAvaible.Count == 0)
            return null;

        AItem firstItem = pickAvaible[0];
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

    public virtual void StopUse()
    {
        isUsing = false;
        StopCoroutine(useCoroutine);
    }

    public virtual void StartUse()
    {
        isUsing = true;
        useCoroutine = StartCoroutine(Use());
    }

    protected abstract IEnumerator Use();

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

    public abstract void SwapIn();
    public abstract void SwapOut();
}

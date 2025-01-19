using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AMeleeItem : AItem, ITrackable
{
    protected Vector2 direction;

    protected float range;

    protected float stunTime;

    protected float angryRate;

    protected float knockbackRate;

    protected float minDistance;

    protected Transform ItemOrigin;

    [SerializeField]
    protected Animator animator;

    protected override void InitializeStats()
    {
        MeleeSO meleeSO = itemsSO as MeleeSO;
        ItemOrigin = transform.GetChild(0).GetComponentInChildren<Transform>();
        animator = transform.GetChild(0).GetComponentInChildren<Animator>();

        range = meleeSO.range;
        stunTime = meleeSO.stunTime;
        angryRate = meleeSO.angryRate;
        knockbackRate = meleeSO.knockbackRate;
        minDistance = meleeSO.minDistance;
        price = meleeSO.price;
        cd = meleeSO.cd;
        alias = meleeSO.alias;
        sprite = meleeSO.sprite;
        onUseAudio = meleeSO.onUseAudio;

        audioSource = GetComponent<AudioSource>();
        audioSource.clip = onUseAudio;
    }

    public void Track(Vector2 endPoint)
    {
        direction = (
            endPoint - (Vector2)Camera.main.WorldToScreenPoint(transform.parent.position)
        ).normalized;
        transform.localPosition = direction * minDistance;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = ItemOrigin == null ? Vector3.zero : ItemOrigin.position;
        Gizmos.DrawWireSphere(position, range);
    }

    public void DetectColliders()
    {
        audioSource.Play();
        foreach (Collider2D item in Physics2D.OverlapCircleAll(ItemOrigin.position, range))
        {
            if (item.GetComponent<AEnemy>() is AEnemy enemy)
            {
                enemy.GetStun(stunTime, angryRate);
                enemy.GetKnockBack(knockbackRate, (Vector2)ItemOrigin.position);
            }
        }
    }

    public override void SwapIn()
    {
        ItemOrigin.GetComponent<SpriteRenderer>().enabled = true;
        animator.enabled = true;
    }

    public override void SwapOut()
    {
        ItemOrigin.GetComponent<SpriteRenderer>().enabled = false;
        animator.enabled = false;
    }
}

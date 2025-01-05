using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public abstract class AMeleeItem : AItems, ITrackable
{
    [SerializeField]
    protected Vector2 direction;

    [SerializeField]
    protected float range;

    [SerializeField]
    protected float stunTime;

    [SerializeField]
    protected float angryRate;

    [SerializeField]
    protected float knockbackRate;

    [SerializeField]
    protected float minDistance;

    [SerializeField]
    protected Collider2D itemCollider;

    [SerializeField]
    protected Transform ItemOrigin;

    [SerializeField]
    protected float DetectionRadius;

    [SerializeField]
    protected Animator animator;

    public override void Use()
    {
        if (!useAvaible)
            return;
        StartCoroutine(Cooldown());
        animator.SetTrigger("ATTACK");
        DetectColliders();
    }

    protected override void InitializeStats()
    {
        ItemOrigin = transform.GetChild(0).GetComponentInChildren<Transform>();
        animator = transform.GetChild(0).GetComponentInChildren<Animator>();
        // ItemOrigin.transform.localPosition = new Vector3(0, 0, 0);
        Debug.Log("not implemented");
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
        Gizmos.DrawWireSphere(position, DetectionRadius);
    }

    public void DetectColliders()
    {
        foreach (
            Collider2D item in Physics2D.OverlapCircleAll(ItemOrigin.position, DetectionRadius)
        )
        {
            if (item.GetComponent<AEnemy>() is AEnemy enemy)
            {
                enemy.GetStun(stunTime, angryRate);
                enemy.GetKnockBack(knockbackRate, (Vector2)transform.position);
            }
        }
    }
}

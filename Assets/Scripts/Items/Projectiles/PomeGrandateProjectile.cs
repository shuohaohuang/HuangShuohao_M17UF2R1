using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PomeGranateProjectile : AProjectile
{
    [SerializeField]
    float speed = 1f;

    [SerializeField]
    float time;

    [SerializeField]
    float range = 3;

    public void GoTo(Vector2 objectivePoint)
    {
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(objectivePoint);

        Vector2 direction = worldPosition - (Vector2)transform.position;

        _rb.linearVelocity = direction.normalized * speed;

        time = Vector2.Distance(worldPosition, transform.position) / speed;
        StartCoroutine(Explode());
    }

    public override void InitProjectile(
        float projectileSpeed,
        Vector2 direction,
        Vector3 position,
        Quaternion rotation,
        ARangedItem parent,
        int satiety
    )
    {
        transform.SetPositionAndRotation(position, rotation);
        speed = projectileSpeed;

        foodValue = satiety;

        GoTo(direction);
        this.parent = parent;
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(time);
        DetectColliders();
        parent.ReturnToPool(this);
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AEnemy>() != null)
        {
            other.GetComponent<AEnemy>().GetFeed(foodValue);
            DetectColliders();
            parent.ReturnToPool(this);
        }

        if (other.CompareTag("OBSTACLES"))
        {
            DetectColliders();
            parent.ReturnToPool(this);
        }
    }

    public void DetectColliders()
    {
        foreach (Collider2D item in Physics2D.OverlapCircleAll(transform.position, range))
        {
            if (item.GetComponent<AEnemy>() is AEnemy enemy)
            {
                enemy.GetKnockBack(2, (Vector2)transform.position);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 position = transform == null ? Vector3.zero : transform.position;
        Gizmos.DrawWireSphere(position, range);
    }

    private void OnEnable()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.angularVelocity = Mathf.Deg2Rad * 50;
    }
}

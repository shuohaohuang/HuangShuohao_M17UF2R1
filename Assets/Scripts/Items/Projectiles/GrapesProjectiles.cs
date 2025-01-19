using System.Collections.Generic;
using UnityEngine;

public class GrapesProjectiles : AProjectile
{
    public override void InitProjectile(
        float projectileSpeed,
        Vector2 direction,
        Vector3 position,
        Quaternion rotation,
        ARangedItem parent,
        int satiety
    )
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;
        transform.SetPositionAndRotation(position, rotation);
        foodValue = satiety;

        this.parent = parent;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<AEnemy>() != null)
        {
            other.GetComponent<AEnemy>().GetFeed(foodValue);
            parent.ReturnToPool(this);
        }

        if (other.CompareTag("OBSTACLES"))
        {
            parent.ReturnToPool(this);
        }
    }
}

using UnityEngine;

public class BasicProyectile : AEnemyProjectile
{
    public override void InitProjectile(
        float projectileSpeed,
        Vector2 direction,
        Vector3 position,
        Quaternion rotation,
        RangeEnemy rangedEnemy,
        int damage
    )
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = direction * projectileSpeed;
        transform.SetPositionAndRotation(position, rotation);
        speed = projectileSpeed;

        this.damage = damage;
        parent = rangedEnemy;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<IDamageable>() is IDamageable damageable)
        {
            damageable.TakeDamage(damage);
            parent.ReturnToPool(this);
        }

        if (other.CompareTag("OBSTACLES"))
        {
            parent.ReturnToPool(this);
        }
    }
}

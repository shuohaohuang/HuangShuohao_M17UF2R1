using System.Collections.Generic;
using UnityEngine;

public abstract class AEnemyProjectile : MonoBehaviour
{
    [SerializeField]
    protected int damage;

    [SerializeField]
    protected float speed;

    protected Collider2D _collider2D;
    protected Rigidbody2D _rb;
    public RangeEnemy parent;

    public abstract void InitProjectile(
        float projectileSpeed,
        Vector2 direction,
        Vector3 position,
        Quaternion rotation,
        RangeEnemy rangedEnemy,
        int damage
    );
}

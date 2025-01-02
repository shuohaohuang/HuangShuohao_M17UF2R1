using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected int satiety;
    protected Collider2D _collider2D;

    protected Rigidbody2D _rb;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        _collider2D.isTrigger = false;
    }
}

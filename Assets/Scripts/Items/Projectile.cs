using UnityEngine;

public class Projectile : MonoBehaviour
{
    int satiety;
    Collider2D _collider2D;

    Rigidbody2D _rb;

    private void OnTriggerEnter2D(Collider2D other)
    {
        _collider2D.isTrigger = false;
    }
}

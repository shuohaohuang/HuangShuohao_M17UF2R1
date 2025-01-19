using UnityEngine;

public abstract class AProjectile : MonoBehaviour
{
    public int foodValue;
    protected Collider2D _collider2D;
    protected Rigidbody2D _rb;

    protected ARangedItem parent;

    public virtual void InitProjectile(
        float projectileSpeed,
        Vector2 direction,
        Vector3 position,
        Quaternion rotation,
        ARangedItem item,
        int satiety
    ) { }

    private void OnDestroy()
    {
        parent.DestroyPool();
    }
}

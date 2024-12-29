using Unity.Mathematics;
using UnityEngine;

public class ARangedItem : AItems
{
    [SerializeField]
    protected GameObject projectileSprite;

    [SerializeField]
    protected int projectileSpeed;

    [SerializeField]
    protected Vector2 direction;

    [SerializeField]
    protected float minDistance;

    public override void Use()
    {
        GameObject newProjectile = Instantiate(
            projectileSprite,
            transform.position,
            quaternion.identity
        );
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
    }

    public void SetDirection(Vector2 newDirection)
    {
        direction = (
            newDirection - (Vector2)Camera.main.WorldToScreenPoint(parentTransform.position)
        ).normalized;
        updatePosition(direction);
    }

    void updatePosition(Vector2 direction)
    {
        transform.localPosition = direction * minDistance;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}

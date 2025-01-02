using System.Collections;
using Unity.Mathematics;
using UnityEngine;

public abstract class ARangedItem : AItems, ITrackable
{
    [SerializeField]
    protected float projectileSpeed;

    [SerializeField]
    protected int satiety;

    [SerializeField]
    protected float minDistance;

    [SerializeField]
    protected Vector2 direction;

    [SerializeField]
    protected Vector2 objectivePoint;

    [SerializeField]
    protected ARangedSO aRangedSO;

    [SerializeField]
    protected GameObject projectile;

    public override void Use()
    {
        if (!useAvaible)
            return;
        StartCoroutine(Cooldown());
        GameObject newProjectile = Instantiate(projectile, transform.position, quaternion.identity);
        newProjectile.GetComponent<Rigidbody2D>().linearVelocity = direction * projectileSpeed;
    }

    public virtual void Track(Vector2 endPoint)
    {
        Vector2 thisScreePosition = (Vector2)
            Camera.main.WorldToScreenPoint(transform.parent.position);

        direction = (endPoint - thisScreePosition).normalized;
        transform.localPosition = direction * minDistance;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    protected override void InitializeStats()
    {
        alias = aRangedSO.alias;
        price = aRangedSO.price;
        cd = aRangedSO.cd;
        shotAudio = aRangedSO.shotAudio;
        projectileSpeed = aRangedSO.projectileSpeed;
        satiety = aRangedSO.satiety;
        minDistance = aRangedSO.minDistance;
        projectile = aRangedSO.projectile;
    }
}
